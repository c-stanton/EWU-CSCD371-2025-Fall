using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculate;

public static class Calculator<T> where T : struct, IComparable, IConvertible
{
    private static T ConvertToT(double value) => (T)Convert.ChangeType(value, typeof(T));
    private static double ConvertToDouble(T value) => (double)Convert.ChangeType(value, typeof(double));

    public static T Add(T a, T b)
    {
        dynamic dynA = a;
        dynamic dynB = b;
        return dynA + dynB;
    }

    public static T Subtract(T a, T b) {
        dynamic dynA = a;
        dynamic dynB = b;
        return dynA - dynB;
    }

    public static T Multiply(T a, T b) {
        dynamic dynA = a;
        dynamic dynB = b;
        return dynA * b;
    }

    public static T Divide(T a, T b)
    {
        if (ConvertToDouble(b) == 0)
        {
            if (typeof(T) == typeof(double))
            {
                return ConvertToT(double.NaN);
            }
            return default;
        }

        dynamic dynA = a;
        dynamic dynB = b;
        return dynA / dynB;
    }


    public static IReadOnlyDictionary<char, Func<T, T, T>> MathematicalOperations { get; } =
        new Dictionary<char, Func<T, T, T>>
        {
            { '+', Add },
            { '-', Subtract },
            { '*', Multiply },
            { '/', Divide }
        };

    public static bool TryCalculate(string expression, out T result)
    {
        result = default;
        string[] parts = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3)
        {
            return false;
        }

        if (parts[1].Length != 1)
        {
            return false;
        }

        char op = parts[1][0];

        if (!int.TryParse(parts[0], out int number1) || !int.TryParse(parts[2], out int number2))
        {
            return false;
        }

        T operand1 = ConvertToT(number1);
        T operand2 = ConvertToT(number2);

        if (MathematicalOperations.TryGetValue(op, out var operation))
        {
            if (op == '/' && ConvertToDouble(operand2) == 0)
            {
                return false;
            }
            result = operation(operand1, operand2);
            return true;
        }

        return false;
    }
}
