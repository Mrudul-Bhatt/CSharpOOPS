namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._4_Polymorphism;

public class _1_Polymorphism
{
    private class Template1
    {
        // Base class
        public class Shape
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }

            // Virtual method
            public virtual void Draw()
            {
                Console.WriteLine("Performing base class drawing tasks");
            }
        }

        // Derived classes
        public class Circle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Drawing a circle");
                base.Draw(); // Calls base class implementation
            }
        }

        public class Rectangle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Drawing a rectangle");
                base.Draw();
            }
        }

        public class Triangle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Drawing a triangle");
                base.Draw();
            }
        }

        private class Program
        {
            private static void Main()
            {
                // Polymorphism in action: a collection of Shapes
                var shapes = new List<Shape>
                {
                    new Rectangle(), // Runtime type: Rectangle
                    new Triangle(), // Runtime type: Triangle
                    new Circle() // Runtime type: Circle
                };

                // The runtime type determines the method implementation
                foreach (var shape in shapes) shape.Draw();
            }
        }
    }
}