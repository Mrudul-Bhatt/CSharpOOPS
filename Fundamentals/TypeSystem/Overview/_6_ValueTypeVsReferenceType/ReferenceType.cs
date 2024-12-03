namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._6_ValueTypeVsReferenceType;

public class ReferenceType
{
    //Reference Type Example
    private class Person
    {
        public string Name { get; set; }
    }

    internal class Program1
    {
        private static void Main()
        {
            var person1 = new Person { Name = "Alice" };
            var person2 = person1; // References the same object

            person2.Name = "Bob"; // Modifies the shared instance

            Console.WriteLine($"Person1: {person1.Name}"); // Output: Person1: Bob
            Console.WriteLine($"Person2: {person2.Name}"); // Output: Person2: Bob
        }
    }

    //Arrays as Reference Type
    private class Program2
    {
        private static void Main()
        {
            int[] nums = { 1, 2, 3 }; // Array is a reference type

            var numsCopy = nums; // Both refer to the same memory
            numsCopy[0] = 42;

            Console.WriteLine($"nums[0]: {nums[0]}"); // Output: nums[0]: 42
        }
    }
}