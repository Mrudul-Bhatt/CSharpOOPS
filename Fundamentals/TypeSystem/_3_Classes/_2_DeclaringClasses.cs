using System;

namespace CSharpOOPS.Fundamentals.TypeSystem._3_Classes;

public class _2_DeclaringClasses
{
    //Example 1: Declaring a Simple Class
    public class Template1
    {
        public class Customer
        {
            // Field
            private string name;

            // Property
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            // Method
            public void DisplayInfo()
            {
                Console.WriteLine($"Customer Name: {name}");
            }
        }
    }

    // Example 2: Internal Class (Default Modifier)
    public class Template2
    {
        class Order
        {
            public int OrderID { get; set; }
            public string ProductName { get; set; }
        }
    }

    //Example 3: Class with Constructor
    public class Template3
    {
        public class Product
        {
            // Fields
            private string name;
            private double price;

            // Constructor
            public Product(string name, double price)
            {
                this.name = name;
                this.price = price;
            }

            // Method
            public void ShowDetails()
            {
                Console.WriteLine($"Product: {name}, Price: ${price}");
            }
        }

        static void Main()
        {
            Product product = new Product("Laptop", 999.99); // Constructor is called
            product.ShowDetails();
        }

    }

    // Example 4: Nested Classes
    public class Template4
    {
        public class OuterClass
        {
            public class InnerClass
            {
                public void Display()
                {
                    Console.WriteLine("I am a nested class.");
                }
            }
        }

        static void Main()
        {
            OuterClass.InnerClass nested = new OuterClass.InnerClass();
            nested.Display();
        }

    }

}
