using System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _2_TypeTests
{
    // MidPoint function without using pattern matching.
    class Template1
    {
        public static class Program
        {
            public static T MidPoint<T>(IEnumerable<T> sequence)
            {
                if (sequence is IList<T> list)
                {
                    // If the sequence is a list, calculate the midpoint using Count property
                    return list[list.Count / 2];
                }
                else if (sequence is null)
                {
                    // If the sequence is null, throw an exception
                    throw new ArgumentNullException(nameof(sequence), "Sequence can't be null.");
                }
                else
                {
                    // If the sequence is another type of IEnumerable, calculate midpoint differently
                    int halfLength = sequence.Count() / 2 - 1;
                    if (halfLength < 0) halfLength = 0;
                    return sequence.Skip(halfLength).First();
                }
            }

            public static void Main()
            {
                // Example 1: Using a list
                IList<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                Console.WriteLine($"Midpoint of list: {MidPoint(numbers)}"); // Output: 3

                // Example 2: Using an array
                int[] array = { 10, 20, 30, 40, 50 };
                Console.WriteLine($"Midpoint of array: {MidPoint(array)}"); // Output: 30

                // Example 3: Using another IEnumerable
                IEnumerable<int> enumerable = new[] { 100, 200, 300 };
                Console.WriteLine($"Midpoint of IEnumerable: {MidPoint(enumerable)}"); // Output: 200

                // Example 4: Null sequence
                try
                {
                    IEnumerable<int>? nullSequence = null;
                    Console.WriteLine($"Midpoint of null: {MidPoint(nullSequence)}");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message); // Output: Sequence can't be null.
                }
            }
        }
    }

    // MidPoint enhanced function using pattern matching
    class Template2
    {
        public static T MidPoint<T>(IEnumerable<T> sequence) => sequence switch
        {
            IList<T> list => list[list.Count / 2], // If sequence is a list
            null => throw new ArgumentNullException(nameof(sequence), "Sequence can't be null."), // If sequence is null
            _ => sequence.Skip(sequence.Count() / 2 - 1).First() // Default case
        };
    }
}
