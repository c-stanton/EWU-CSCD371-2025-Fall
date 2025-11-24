using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment;

public record struct PingResult(int ExitCode, string? StdOutput);

public class PingProcess
{
    private ProcessStartInfo StartInfo { get; } = new("ping");

    public PingResult Run(string hostNameOrAddress)
    {
        StartInfo.Arguments = BuildPingArguments(hostNameOrAddress);
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder ??= new StringBuilder()).AppendLine(line);
        Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
        return new PingResult(process.ExitCode, stringBuilder?.ToString());
    }

    public Task<PingResult> RunTaskAsync(string hostNameOrAddress)
    {
        return Task.Run(() =>
        {
            StartInfo.Arguments = BuildPingArguments(hostNameOrAddress);

            StringBuilder? sb = null;
            void output(string? line) =>
                (sb ??= new StringBuilder()).AppendLine(line);

            Process p = RunProcessInternal(StartInfo, output, null, default);
            return new PingResult(p.ExitCode, sb?.ToString());
        });
    }

    public async Task<PingResult> RunAsync(
        string hostNameOrAddress, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            StartInfo.Arguments = BuildPingArguments(hostNameOrAddress);
            StringBuilder? sb = null;
            void output(string? line) =>
                (sb ??= new StringBuilder()).AppendLine(line);

            Process p = RunProcessInternal(StartInfo, output, null, cancellationToken);
            return new PingResult(p.ExitCode, sb?.ToString());
        }, cancellationToken);
    }

    public async Task<PingResult> RunAsync(
    IEnumerable<string> hostNameOrAddresses,
    int pingCountPerHost = 1,
    CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new();
        object lockObj = new();

        var tasks = hostNameOrAddresses
            .Select(host => Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                ProcessStartInfo psi = new("ping")
                {
                    Arguments = BuildPingArguments(host, pingCountPerHost),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                void output(string? line)
                {
                    lock (lockObj)
                    {
                        sb.AppendLine(line);
                    }
                }

                Process p = RunProcessInternal(psi, output, null, cancellationToken);
                return p.ExitCode;

            }, cancellationToken))
            .ToArray();

        int[] exitCodes = await Task.WhenAll(tasks);
        int totalExitCodes = exitCodes.Sum();

        return new PingResult(totalExitCodes, sb.ToString());
    }

    public Task<PingResult> RunLongRunningAsync(
        string hostNameOrAddress,
        CancellationToken cancellationToken = default)
    {
        return Task.Factory.StartNew(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            ProcessStartInfo psi = new("ping")
            {
                Arguments = BuildPingArguments(hostNameOrAddress),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            StringBuilder? sb = null;
            void output(string? line) =>
                (sb ??= new StringBuilder()).AppendLine(line);

            Process p = RunProcessInternal(
                psi,
                output,
                null,
                cancellationToken);

            return new PingResult(p.ExitCode, sb?.ToString());

        }, cancellationToken,
           TaskCreationOptions.LongRunning,
           TaskScheduler.Current);
    }

    private Process RunProcessInternal(
        ProcessStartInfo startInfo,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        var process = new Process
        {
            StartInfo = UpdateProcessStartInfo(startInfo)
        };
        return RunProcessInternal(process, progressOutput, progressError, token);
    }

    private Process RunProcessInternal(
        Process process,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += OutputHandler;
        process.ErrorDataReceived += ErrorHandler;

        try
        {
            if (!process.Start())
            {
                return process;
            }

            token.Register(obj =>
            {
                if (obj is Process p && !p.HasExited)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Win32Exception ex)
                    {
                        throw new InvalidOperationException($"Error cancelling process{Environment.NewLine}{ex}");
                    }
                }
            }, process);


            if (process.StartInfo.RedirectStandardOutput)
            {
                process.BeginOutputReadLine();
            }
            if (process.StartInfo.RedirectStandardError)
            {
                process.BeginErrorReadLine();
            }

            if (process.HasExited)
            {
                return process;
            }
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error running '{process.StartInfo.FileName} {process.StartInfo.Arguments}'{Environment.NewLine}{e}");
        }
        finally
        {
            if (process.StartInfo.RedirectStandardError)
            {
                process.CancelErrorRead();
            }
            if (process.StartInfo.RedirectStandardOutput)
            {
                process.CancelOutputRead();
            }
            process.OutputDataReceived -= OutputHandler;
            process.ErrorDataReceived -= ErrorHandler;

            if (!process.HasExited)
            {
                process.Kill();
            }

        }
        return process;

        void OutputHandler(object s, DataReceivedEventArgs e)
        {
            progressOutput?.Invoke(e.Data);
        }

        void ErrorHandler(object s, DataReceivedEventArgs e)
        {
            progressError?.Invoke(e.Data);
        }
    }

    private static ProcessStartInfo UpdateProcessStartInfo(ProcessStartInfo startInfo)
    {
        startInfo.CreateNoWindow = true;
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        return startInfo;
    }

    private static string BuildPingArguments(string host, int count = 4)
    {
        string countArg = OperatingSystem.IsWindows()
            ? $"-n {count}"
            : $"-c {count}";

        return $"{countArg} {host}";
    }
}