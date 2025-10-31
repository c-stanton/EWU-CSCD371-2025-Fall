using System;

namespace Calculate;

public class Program
{
    public required Action<string> WriteLine { get; init; }

    public required Func<string?> ReadLine { get; init; }

    public Program()
    {

    }

    public static void Main(string[] args)
    {
        var program = new Program
        {
            WriteLine = Console.WriteLine,
            ReadLine = Console.ReadLine
        };
        program.Run();
    }

    
    public void Run()
    {
       
    }

}