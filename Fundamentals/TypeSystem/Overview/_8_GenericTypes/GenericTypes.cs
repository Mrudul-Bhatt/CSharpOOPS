namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._8_GenericTypes;

public class GenericTypes
{
    /**
     * Example: Generic List
     */
    private class Program1
    {
        private static void Main()
        {
            // Create a generic list of strings
            var stringList = new List<string>();

            // Add strings to the list
            stringList.Add("Hello");
            stringList.Add("World");

            // Attempting to add a non-string value results in a compile-time error
            // stringList.Add(42); // Uncommenting this line will cause a compile-time error

            // Iterate through the list
            foreach (var str in stringList) Console.WriteLine(str);

            // Output:
            // Hello
            // World
        }
    }

    /**
     * * Custom Generic Class Example
     * You can create your own generic types using type parameters.
     */
    private class GenericBox<T>
    {
        private T value;

        public void SetValue(T val)
        {
            value = val;
        }

        public T GetValue()
        {
            return value;
        }
    }

    private class Program2
    {
        private static void Main()
        {
            // Create a GenericBox for integers
            var intBox = new GenericBox<int>();
            intBox.SetValue(123);
            Console.WriteLine($"Integer Value: {intBox.GetValue()}"); // Output: Integer Value: 123

            // Create a GenericBox for strings
            var stringBox = new GenericBox<string>();
            stringBox.SetValue("Hello, Generics!");
            Console.WriteLine($"String Value: {stringBox.GetValue()}"); // Output: String Value: Hello, Generics!
        }
    }

    /**
     * * Generic Constraints
     * You can restrict the types that can be used as type parameters using constraints.
     */
    private class DataProcessor<T> where T : IComparable<T>
    {
        public T FindMax(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }

    private class Program
    {
        private static void Main()
        {
            var processor = new DataProcessor<int>();
            var max = processor.FindMax(5, 10);
            Console.WriteLine($"Max: {max}"); // Output: Max: 10
        }
    }
}