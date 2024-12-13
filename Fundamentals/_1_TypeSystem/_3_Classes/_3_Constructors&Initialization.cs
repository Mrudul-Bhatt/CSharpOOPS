using System;

namespace CSharpOOPS.Fundamentals.TypeSystem._3_Classes;

public class _3_Constructors_Initialization
{
    // Default values of Value Types and Reference Types
    // Field Initializers
    class Template1
    {
        public class Sample
        {
            public int Number;       // Default: 0
            public string Name;      // Default: null
            public bool IsActive;    // Default: false
            public int _capacity = 10;     // Field initializer for the `_capacity` field
        }

        static void Main()
        {
            var obj = new Sample();
            Console.WriteLine(obj.Number);   // Output: 0
            Console.WriteLine(obj.Name);     // Output: (null)
            Console.WriteLine(obj.IsActive); // Output: False
        }
    }

    // Primary Constructors (C# 12)
    class Template2
    {
        public class Container(int capacity)
        {
            private int _capacity = capacity;

            public void ShowCapacity()
            {
                Console.WriteLine($"Capacity: {_capacity}");
            }
        }

        static void Main()
        {
            var container = new Container(30);
            container.ShowCapacity(); // Output: Capacity: 30
        }
    }

    // Required Properties 
    class Template3
    {
        public class Person
        {
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
        }

        static void Main()
        {
            // Usage
            // var p1 = new Person(); // Error: Required properties not set

            var p2 = new Person() { FirstName = "Grace", LastName = "Hopper" };
            Console.WriteLine($"{p2.FirstName} {p2.LastName}"); // Output: Grace Hopper
        }
    }

    // Object Initializers 
    class Template4
    {
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        static void Main()
        {
            var person = new Person
            {
                FirstName = "Alan",
                LastName = "Turing"
            };

            Console.WriteLine($"{person.FirstName} {person.LastName}"); // Output: Alan Turing
        }

    }
}
