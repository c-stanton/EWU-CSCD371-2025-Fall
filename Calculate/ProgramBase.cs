using System;

namespace Calculate;

public abstract class ProgramBase
{
    public required Action<string> WriteLine { get; init; }
    public required Func<string?> ReadLine { get; init; }
    protected ProgramBase()
    {
        WriteLine = Console.WriteLine;
        ReadLine = Console.ReadLine;
    }
}