namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._3_Inheritance;

public class _2_AbstractAndVirtualMethods
{
    private class Template1
    {
        public class Animal
        {
            // Virtual method
            public virtual void Speak()
            {
                Console.WriteLine("Animal makes a sound");
            }
        }

        public class Dog : Animal
        {
            // Override the virtual method
            public override void Speak()
            {
                Console.WriteLine("Dog barks");
            }
        }

        public class Cat : Animal
        {
            // Override the virtual method
            public override void Speak()
            {
                Console.WriteLine("Cat meows");
            }
        }

        private class Program
        {
            private static void Main()
            {
                var animal = new Animal();
                Animal dog = new Dog();
                Animal cat = new Cat();

                animal.Speak(); // Output: Animal makes a sound
                dog.Speak(); // Output: Dog barks
                cat.Speak(); // Output: Cat meows
            }
        }
    }

    private class Template2
    {
        public abstract class Shape
        {
            // Abstract method
            public abstract double GetArea();

            // Non-abstract method
            public void Display()
            {
                Console.WriteLine("This is a shape");
            }
        }

        public class Circle : Shape
        {
            public Circle(double radius)
            {
                Radius = radius;
            }

            public double Radius { get; }

            // Implement the abstract method
            public override double GetArea()
            {
                return Math.PI * Radius * Radius;
            }
        }

        public class Rectangle : Shape
        {
            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double Width { get; }
            public double Height { get; }

            // Implement the abstract method
            public override double GetArea()
            {
                return Width * Height;
            }
        }

        private class Program
        {
            private static void Main()
            {
                Shape circle = new Circle(5);
                Shape rectangle = new Rectangle(4, 6);

                Console.WriteLine($"Circle Area: {circle.GetArea()}"); // Output: Circle Area: 78.53981633974483
                Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}"); // Output: Rectangle Area: 24
            }
        }
    }

    private class Template3
    {
        // Define an interface
        public interface IMovable
        {
            void Move();
        }

        public interface IStoppable
        {
            void Stop();
        }

        // Implement multiple interfaces
        public class Car : IMovable, IStoppable
        {
            public void Move()
            {
                Console.WriteLine("Car is moving");
            }

            public void Stop()
            {
                Console.WriteLine("Car has stopped");
            }
        }

        private class Program
        {
            private static void Main()
            {
                var myCar = new Car();
                myCar.Move(); // Output: Car is moving
                myCar.Stop(); // Output: Car has stopped
            }
        }
    }

    private class Template4
    {
        public sealed class FinalClass
        {
            public void Display()
            {
                Console.WriteLine("This class cannot be inherited");
            }
        }

        // The following would produce a compile error
        // public class DerivedClass : FinalClass { }

        public class BaseClass
        {
            public virtual void Display()
            {
                Console.WriteLine("Base class display");
            }
        }

        public class DerivedClass : BaseClass
        {
            public sealed override void Display()
            {
                Console.WriteLine("Derived class display");
            }
        }

        // The following would produce a compile error
        // public class AnotherDerivedClass : DerivedClass
        // {
        //     public override void Display() { }
        // }
    }

    private class Template5
    {
        public class BaseClass
        {
            public void Display()
            {
                Console.WriteLine("Base class display");
            }
        }

        public class DerivedClass : BaseClass
        {
            public new void Display()
            {
                Console.WriteLine("Derived class display");
            }
        }

        private class Program
        {
            private static void Main()
            {
                var baseObj = new BaseClass();
                var derivedObj = new DerivedClass();
                BaseClass baseRef = new DerivedClass();

                baseObj.Display(); // Output: Base class display
                derivedObj.Display(); // Output: Derived class display
                baseRef.Display(); // Output: Base class display
            }
        }
    }
}