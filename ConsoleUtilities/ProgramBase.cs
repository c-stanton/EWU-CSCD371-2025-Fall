using System;

namespace ConsoleUtilities;

public abstract class ProgramBase
{
    public required Action<string> WriteLine { get; init; }
    public required Func<string?> ReadLine { get; init; }
    protected ProgramBase()
    {
    }
}