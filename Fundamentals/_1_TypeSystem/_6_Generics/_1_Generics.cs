namespace CSharpOOPS.Fundamentals.TypeSystem._6_Generics;

public class _1_Generics
{
    private class Template1
    {
        // A simple generic class.
        private class GenericList<T>
        {
            private readonly List<T> _items = new();

            public void Add(T item)
            {
                _items.Add(item);
            }

            public void PrintAll()
            {
                foreach (var item in _items) Console.WriteLine(item);
            }
        }

        private class ExampleClass
        {
        }

        public class Program
        {
            private static void Main()
            {
                // Generic list of integers
                GenericList<int> intList = new();
                intList.Add(10);
                intList.Add(20);
                intList.PrintAll();

                // Generic list of strings
                GenericList<string> stringList = new();
                stringList.Add("Hello");
                stringList.Add("World");
                stringList.PrintAll();

                // Generic list of custom objects
                GenericList<ExampleClass> customList = new();
                customList.Add(new ExampleClass());
                Console.WriteLine("Custom object added to the list.");
            }
        }
    }

    // Generic method
    private class Template2
    {
        private class Utility
        {
            public static void PrintTwice<T>(T value)
            {
                Console.WriteLine(value);
                Console.WriteLine(value);
            }
        }

        public class Program
        {
            public static void Main()
            {
                // Call the generic method with different types
                Utility.PrintTwice(42);
                Utility.PrintTwice<string>("Generics in C#");
                Utility.PrintTwice(DateTime.Now);
            }
        }
    }

    // Generic constraints
    private class Template3
    {
        private class GenericUtility<T> where T : new()
        {
            public T CreateInstance()
            {
                return new T();
            }
        }

        private class MyClass
        {
        }

        public class Program
        {
            public static void Main()
            {
                GenericUtility<MyClass> utility = new();
                var instance = utility.CreateInstance();
                Console.WriteLine("Instance of MyClass created.");
            }
        }
    }
}