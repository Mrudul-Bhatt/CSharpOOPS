using System;

namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _1_NullChecks
{
    class Template1
    {
        static void Main()
        {
            int? maybe = 12;

            if (maybe is int number)
            {
                Console.WriteLine($"The nullable int 'maybe' has the value {number}");
            }
            else
            {
                Console.WriteLine("The nullable int 'maybe' doesn't hold a value");
            }
        }
    }

    class Template2
    {
        static void Main()
        {
            string? message = "Hello, World!";

            if (message is not null)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Message is null.");
            }
        }
    }

    class Template3
    {
        public class Circle
        {
            public double Radius { get; set; }
        }

        public class Rectangle
        {
            public double Width { get; set; }
            public double Height { get; set; }
        }

        static void Main()
        {
            object shape = new Circle { Radius = 5 };

            string description = shape switch
            {
                Circle c => $"Circle with radius {c.Radius}",
                Rectangle r => $"Rectangle with width {r.Width} and height {r.Height}",
                null => "Shape is null",
                _ => "Unknown shape"
            };

            Console.WriteLine(description);
        }
    }

    class Template4
    {
        static void Main()
        {
            int age = 25;

            string category = age switch
            {
                < 13 => "Child",
                >= 13 and < 20 => "Teenager",
                >= 20 and < 60 => "Adult",
                >= 60 => "Senior"
            };

            Console.WriteLine($"Age category: {category}");
        }
    }

    class Template5
    {
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main()
        {
            object location = new Point { X = 3, Y = 4 };

            string description = location switch
            {
                Point { X: 0, Y: 0 } => "Origin",
                Point { X: var x, Y: var y } => $"Point at ({x}, {y})",
                _ => "Unknown location"
            };

            Console.WriteLine(description);
        }
    }

    class Template6
    {
        static void Main()
        {
            object value = 42;

            string result = value switch
            {
                int i and > 0 => "Positive integer",
                int i and < 0 => "Negative integer",
                string s and not "" => "Non-empty string",
                _ => "Unknown"
            };

            Console.WriteLine(result);
        }
    }
}
