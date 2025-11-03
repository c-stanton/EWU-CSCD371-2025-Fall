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
        WriteLine("Calculator Program! Press Enter to exit.");

        while (true)
        {
            WriteLine("Enter a expression");
            string? input = ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                WriteLine("Exiting program.");
                break;
            }

            if (Calculator.TryCalculate(input, out double result))
            {
                WriteLine($"Result: {result}");
            }
            else
            {
                WriteLine("Invalid expression. Please try again.");
            }
        }
        WriteLine("Thanks 4 playin!");
    }

    

}