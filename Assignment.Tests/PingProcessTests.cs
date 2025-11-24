using IntelliTect.TestTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Tests;

[TestClass]
public class PingProcessTests
{
    PingProcess Sut { get; set; } = new();

    [TestInitialize]
    public void TestInitialize()
    {
        Sut = new();
    }

    [TestMethod]
    public void Start_PingProcess_Success()
    {
        Process process = Process.Start("ping", "localhost");
        process.WaitForExit();
        Assert.AreEqual<int>(0, process.ExitCode);
    }

    [TestMethod]
    public void Run_GoogleDotCom_Success()
    {
        int exitCode = Sut.Run("google.com").ExitCode;
        Assert.AreEqual<int>(0, exitCode);
    }

    [TestMethod]
    public void Run_InvalidAddressOutput_Success()
    {
        (int exitCode, string? stdOutput) = Sut.Run("badaddress");
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());

        Assert.AreEqual<string?>(
            "Ping request could not find host badaddress. Please check the name and try again.".Trim(),
            stdOutput,
            $"Output is unexpected: {stdOutput}");

        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_CaptureStdOutput_Success()
    {
        PingResult result = Sut.Run("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunTaskAsync_Success()
    {
        // Do NOT use async/await in this test.
        Task<PingResult> task = Sut.RunTaskAsync("localhost");
        PingResult result = task.Result;
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTaskReturn_Success()
    {
        // Do NOT use async/await in this test.
        Task<PingResult> task = Sut.RunAsync("localhost");
        PingResult result = task.Result;
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public async Task RunAsync_UsingTpl_Success()
    {
        PingResult result = await Sut.RunAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping()
    {
        CancellationTokenSource cts = new();
        Task<PingResult> task = Sut.RunAsync("localhost", cts.Token);

        cts.Cancel();

        Assert.Throws<AggregateException>(() => _ = task.Result);
    }

    [TestMethod]
    public void RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException()
    {
        CancellationTokenSource cts = new();
        Task<PingResult> task = Sut.RunAsync("localhost", cts.Token);

        cts.Cancel();

        Assert.Throws<TaskCanceledException>(() =>
        {
            try
            {
                _ = task.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten().InnerException!;
            }
        });
    }

    [TestMethod]
    public async Task RunAsync_MultipleHostAddresses_True()
    {
        string[] hostNames = new string[] { "localhost", "localhost", "localhost", "localhost" };

        PingResult result = await Sut.RunAsync(hostNames, pingCountPerHost: 1);

        var lines = result.StdOutput?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                ?? Array.Empty<string>();

        int replyLinesCount = lines.Count(l => l.TrimStart().StartsWith("Reply from", StringComparison.OrdinalIgnoreCase));

        int expectedReplies = hostNames.Length;

        Assert.AreEqual(expectedReplies, replyLinesCount,
            $"Expected {expectedReplies} 'Reply from' lines, but got {replyLinesCount}");
    }

    [TestMethod]
    public async Task RunLongRunningAsync_UsingTpl_Success()
    {
        PingResult result = await Sut.RunLongRunningAsync("localhost");
        AssertValidPingOutput(result);
    }

    [TestMethod]
    public void StringBuilderAppendLine_InParallel_IsNotThreadSafe()
    {
        IEnumerable<int> numbers = Enumerable.Range(0, short.MaxValue);
        System.Text.StringBuilder sb = new();

        try
        {
            numbers.AsParallel().ForAll(_ => sb.AppendLine(""));
        }
        catch
        {
            return;
        }

        int lineCount = sb.ToString().Split(Environment.NewLine).Length;

        Assert.AreNotEqual(numbers.Count() + 1, lineCount);
    }

    readonly string PingOutputLikeExpression = @"
Pinging * with 32 bytes of data:
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*
Reply from ::1: time<*

Ping statistics for ::1:
    Packets: Sent = *, Received = *, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = *, Maximum = *, Average = *".Trim();

    private void AssertValidPingOutput(int exitCode, string? stdOutput)
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(stdOutput));
        stdOutput = WildcardPattern.NormalizeLineEndings(stdOutput!.Trim());

        Assert.IsTrue(
            stdOutput!.IsLike(PingOutputLikeExpression),
            $"Output is unexpected: {stdOutput}");

        Assert.AreEqual<int>(0, exitCode);
    }

    private void AssertValidPingOutput(PingResult result) =>
        AssertValidPingOutput(result.ExitCode, result.StdOutput);
}
