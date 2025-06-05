namespace CSharpOOPS._2_LINQ._2_GettingStartedWithLINQ._1_IntroToLINQ;

public class _3_Execution
{
    private class Template1
    {
        private void Main()
        {
            // Data source
            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            // Query to filter even numbers
            var evenNumQuery =
                from num in numbers
                where num % 2 == 0
                select num;

            // Immediate execution using Count()
            var evenNumCount = evenNumQuery.Count();
            Console.WriteLine($"Count of even numbers: {evenNumCount}"); // Output: 3

            // Force immediate execution and cache results
            var evenNumsList = evenNumQuery.ToList(); // Results are cached in a List
            Console.WriteLine(string.Join(", ", evenNumsList)); // Output: 2, 4, 6
        }
    }

    private class Template2
    {
        private void Main()
        {
            // Data source
            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            // Query creation (deferred execution)
            var evenNumQuery =
                from num in numbers
                where num % 2 == 0
                select num;

            // Execute the query by enumerating it
            Console.WriteLine("Even numbers:");
            foreach (var num in evenNumQuery) Console.Write(num + " "); // Output: 2 4 6

            // Modify the data source
            numbers[1] = 10;

            // Query execution reflects updated data
            Console.WriteLine("\nUpdated even numbers:");
            foreach (var num in evenNumQuery) Console.Write(num + " "); // Output: 10 4 6
        }
    }

    private class Template3
    {
        private void Main()
        {
            // Data source
            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            // Streaming execution: processes each element lazily
            var filteredNumbers = numbers.Where(num => num > 3);

            // Query execution
            Console.WriteLine("Filtered numbers:");
            foreach (var num in filteredNumbers) Console.Write(num + " "); // Output: 4 5 6
        }
    }

    private class Template4
    {
        private void Main()
        {
            // Data source
            int[] numbers = { 5, 3, 6, 2, 4, 1 };

            // Non-streaming execution: sorts the entire collection
            var sortedNumbers = numbers.OrderBy(num => num);

            // Query execution
            Console.WriteLine("Sorted numbers:");
            foreach (var num in sortedNumbers) Console.Write(num + " "); // Output: 1 2 3 4 5 6
        }
    }
}