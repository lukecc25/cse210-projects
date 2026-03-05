using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create some individual shapes
        Square square = new Square("Red", 4);
        Rectangle rectangle = new Rectangle("Blue", 5, 6);
        Circle circle = new Circle("Green", 3);

        Console.WriteLine($"{square.GetColor()} square area: {square.GetArea()}");
        Console.WriteLine($"{rectangle.GetColor()} rectangle area: {rectangle.GetArea()}");
        Console.WriteLine($"{circle.GetColor()} circle area: {circle.GetArea()}");
        Console.WriteLine();

        // Build a polymorphic list of shapes
        List<Shape> shapes = new List<Shape>
        {
            square,
            rectangle,
            circle,
            new Square("Yellow", 2.5),
            new Rectangle("Purple", 2, 10),
            new Circle("Orange", 1.5)
        };

        Console.WriteLine("All shapes in list:");
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"{shape.GetColor()} shape area: {shape.GetArea()}");
        }
    }
}