namespace CSharpOOPS._2_LINQ;

public class _1_Overview
{
    //LINQ to Objects
    private class Template1
    {
        private class Program
        {
            private static void Main()
            {
                // Data source
                int[] scores = { 97, 92, 81, 60 };

                // QUERY SYNTAX
                // LINQ query: Query syntax
                var scoreQuery =
                    from score in scores
                    where score > 80
                    select score;

                // Execute the query
                Console.WriteLine("Scores above 80:");
                foreach (var score in scoreQuery) Console.WriteLine(score);

                // METHOD SYNTAX
                // LINQ query: Method syntax (alternative)
                var scoreQueryMethod = scores.Where(score => score > 80);
                Console.WriteLine("\nScores above 80 (method syntax):");
                foreach (var score in scoreQueryMethod) Console.WriteLine(score);
            }
        }
    }

    private class Template2
    {
        // LINQ with Collections
        private void Main()
        {
            var names = new List<string> { "Alice", "Bob", "Charlie", "Dave" };

            // Find names with length > 3
            var longNames = from name in names
                where name.Length > 3
                select name;

            Console.WriteLine("Names with more than 3 characters:");
            foreach (var name in longNames) Console.WriteLine(name);
        }

        // LINQ with Sorting & Grouping
        private void Main2()
        {
            var employees = new[]
            {
                new { Name = "Alice", Department = "HR" },
                new { Name = "Bob", Department = "IT" },
                new { Name = "Charlie", Department = "HR" },
                new { Name = "Dave", Department = "IT" }
            };

            // Group employees by department
            var groupedEmployees = from emp in employees
                group emp by emp.Department;

            foreach (var group in groupedEmployees)
            {
                Console.WriteLine($"Department: {group.Key}");
                foreach (var employee in group) Console.WriteLine($"  {employee.Name}");
            }
        }

        // LINQ with Aggregation
        private void Main3()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            // Sum and Average using LINQ
            var sum = numbers.Sum();
            var average = numbers.Average();

            Console.WriteLine($"Sum: {sum}, Average: {average}");
        }
    }
}