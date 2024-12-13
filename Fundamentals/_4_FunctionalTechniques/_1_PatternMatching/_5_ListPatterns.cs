namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _5_ListPatterns
{
    private class Template1
    {
        private void Main()
        {
            var balance = 0m;

            // Simulated input records
            List<string[]> transactions = new()
            {
                new[] { "04-01-2020", "DEPOSIT", "Initial deposit", "2250.00" },
                new[] { "04-22-2020", "WITHDRAWAL", "Debit", "Groceries", "255.73" },
                new[] { "05-02-2020", "INTEREST", "0.65" },
                new[] { "04-15-2020", "FEE", "5.55" },
                new[] { "INVALID", "UNKNOWN" } // Invalid format
            };

            // Process transactions
            foreach (var transaction in transactions)
            {
                balance += transaction switch
                {
                    [_, "DEPOSIT", .., var amount] => decimal.Parse(amount), // Match deposits
                    [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount), // Match withdrawals
                    [_, "INTEREST", var interest] => decimal.Parse(interest), // Match interest
                    [_, "FEE", var fee] => -decimal.Parse(fee), // Match fees
                    _ => throw new InvalidOperationException(
                        $"Record {string.Join(", ", transaction)} is not in the expected format!")
                };

                Console.WriteLine($"Processed: {string.Join(", ", transaction)}, New Balance: {balance:C}");
            }
        }
    }

    private class Template2
    {
        private string AnalyzeArray(int[] numbers)
        {
            return numbers switch
            {
                [1, 2, 3] => "Exact match: [1, 2, 3]",
                [_, _, _] => "Any three-element array",
                [1, ..] => "Starts with 1",
                [.., 9] => "Ends with 9",
                [_, _, _, _] => "Four-element array",
                [] => "Empty array",
                _ => "Unknown pattern"
            };
        }

        private void Main()
        {
            // Usage
            Console.WriteLine(AnalyzeArray(new[] { 1, 2, 3 })); // Output: Exact match: [1, 2, 3]
            Console.WriteLine(AnalyzeArray(new[] { 1, 5, 9 })); // Output: Starts with 1
            Console.WriteLine(AnalyzeArray(new[] { 4, 5, 9 })); // Output: Ends with 9
            Console.WriteLine(AnalyzeArray(new[] { 7, 8, 9, 10 })); // Output: Four-element array
            Console.WriteLine(AnalyzeArray(new int[0])); // Output: Empty array
            Console.WriteLine(AnalyzeArray(new[] { 2, 4, 6 })); // Output: Any three-element array
        }
    }

    private class Template3
    {
        private void AnalyzeNestedList(object[] data)
        {
            Console.WriteLine(data switch
            {
                [int x, [.. var inner], string last] =>
                    $"Outer int: {x}, Inner: [{string.Join(", ", inner)}], Last: {last}",
                [_, [1, 2, ..], ..] => "Contains [1, 2] at second position",
                _ => "Unknown structure"
            });
        }

        private void Main()
        {
            // Usage
            AnalyzeNestedList(new object[] { 42, new[] { 1, 2, 3, 4 }, "End" });
            // Output: Outer int: 42, Inner: [1, 2, 3, 4], Last: End

            AnalyzeNestedList(new object[] { "Start", new[] { 1, 2, 99 }, "End" });
            // Output: Contains [1, 2] at second position

            AnalyzeNestedList(new object[] { "Hello", 123 });
            // Output: Unknown structure
        }
    }
}