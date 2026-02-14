using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Verify constructors ===");
        Fraction f1 = new Fraction();
        Fraction f2 = new Fraction(6);
        Fraction f3 = new Fraction(6, 7);
        Console.WriteLine($"1/1: {f1.GetFractionString()} = {f1.GetDecimalValue()}");
        Console.WriteLine($"6/1: {f2.GetFractionString()} = {f2.GetDecimalValue()}");
        Console.WriteLine($"6/7: {f3.GetFractionString()} = {f3.GetDecimalValue()}");

        Console.WriteLine("\n=== Verify getters and setters ===");
        Fraction f = new Fraction(3, 4);
        Console.WriteLine($"Before: {f.GetFractionString()} (top={f.GetTop()}, bottom={f.GetBottom()})");
        f.SetTop(5);
        f.SetBottom(8);
        Console.WriteLine($"After SetTop(5), SetBottom(8): {f.GetFractionString()} = {f.GetDecimalValue()}");

        Console.WriteLine("\n=== Different representations ===");
        DisplayFraction(new Fraction());
        DisplayFraction(new Fraction(5));
        DisplayFraction(new Fraction(3, 4));
        DisplayFraction(new Fraction(1, 3));

        Console.WriteLine("\n=== Practice: 20 random fractions ===");
        Fraction frac = new Fraction();
        Random random = new Random();
        for (int i = 1; i <= 20; i++)
        {
            int top = random.Next(1, 100);
            int bottom = random.Next(1, 100);
            frac.SetTop(top);
            frac.SetBottom(bottom);
            Console.WriteLine($"Fraction {i}: string: {frac.GetFractionString()} Number: {frac.GetDecimalValue()}");
        }
    }

    static void DisplayFraction(Fraction fraction)
    {
        Console.WriteLine(fraction.GetFractionString());
        Console.WriteLine(fraction.GetDecimalValue());
    }
}
