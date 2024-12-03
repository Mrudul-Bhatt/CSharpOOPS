namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._6_ValueTypeVsReferenceType;

public class ValueType
{
    //Custom Value Type : Struct
    private struct Coords
    {
        public int X;
        public readonly int Y;

        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Program1
    {
        private static void Main()
        {
            var point1 = new Coords(10, 20);
            var point2 = point1; // Creates a copy (value semantics)

            point2.X = 30; // Modifies only the copy

            Console.WriteLine($"Point1: ({point1.X}, {point1.Y})"); // Output: Point1: (10, 20)
            Console.WriteLine($"Point2: ({point2.X}, {point2.Y})"); // Output: Point2: (30, 20)
        }
    }

    // Enum Example 

    private enum FileMode
    {
        CreateNew = 1,
        Create = 2,
        Open = 3,
        OpenOrCreate = 4,
        Truncate = 5,
        Append = 6
    }

    internal class Program2
    {
        private static void Main()
        {
            var mode = FileMode.OpenOrCreate;
            Console.WriteLine($"File mode: {mode}"); // Output: File mode: OpenOrCreate
        }
    }
}