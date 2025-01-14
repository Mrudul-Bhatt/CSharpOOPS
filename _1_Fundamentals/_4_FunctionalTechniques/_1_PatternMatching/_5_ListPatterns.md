### **List Patterns in C#: Explanation and Examples**

List patterns in C# allow you to match and analyze sequences, such as arrays, lists, or any enumerable data structures, by their **shape** (number of elements and their positions). This feature simplifies working with data where the structure might vary or contain unstructured values.

---

### **Key Concepts in List Patterns**

1.  **Match by Shape**: Test sequences based on the number and arrangement of elements.
2.  **Discard Pattern (`_`)**: Ignore elements you don't need.
3.  **Slice Pattern (`..`)**: Match zero or more elements at any position in a sequence.
4.  **Var Pattern (`var name`)**: Capture a value for further processing.

---

### **Example 1: Processing Transactions from a CSV File**

### **Scenario**

Imagine you're working with transaction records in a **CSV-like structure**. Some rows have varying numbers of columns due to user-generated content or optional fields.

### **Code Example**

```
decimal balance = 0m;

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
        [_, "DEPOSIT", .., var amount]     => decimal.Parse(amount), // Match deposits
        [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount), // Match withdrawals
        [_, "INTEREST", var interest]     => decimal.Parse(interest), // Match interest
        [_, "FEE", var fee]               => -decimal.Parse(fee),    // Match fees
        _                                 => throw new InvalidOperationException(
                                                $"Record {string.Join(", ", transaction)} is not in the expected format!")
    };

    Console.WriteLine($"Processed: {string.Join(", ", transaction)}, New Balance: {balance:C}");
}

```

### **Explanation**

1.  **Transaction Matching**:
    - The **second element** of each array determines the transaction type (`DEPOSIT`, `WITHDRAWAL`, etc.).
    - Depending on the type, the relevant amount is extracted using the `var` pattern.
2.  **Patterns in Use**:
    - `[_, "DEPOSIT", .., var amount]`: Matches deposits and captures the **last field** (`amount`).
    - `[_, "WITHDRAWAL", .., var amount]`: Matches withdrawals and captures the **last field**.
    - `[_, "INTEREST", var interest]`: Matches interest records with exactly 3 fields.
    - `[_, "FEE", var fee]`: Matches fees with exactly 3 fields.
3.  **Error Handling**:
    - The `_` (default) case throws an exception for invalid records.
4.  **Output**:
    - For each valid record, the new balance is computed and printed.

---

### **Example 2: Analyzing Arrays**

### **Scenario**

You want to classify arrays based on their content.

### **Code Example**

```
string AnalyzeArray(int[] numbers) =>
    numbers switch
    {
        [1, 2, 3] => "Exact match: [1, 2, 3]",
        [_, _, _] => "Any three-element array",
        [1, ..] => "Starts with 1",
        [.., 9] => "Ends with 9",
        [_, _, _, _] => "Four-element array",
        [] => "Empty array",
        _ => "Unknown pattern"
    };

// Usage
Console.WriteLine(AnalyzeArray(new[] { 1, 2, 3 }));       // Output: Exact match: [1, 2, 3]
Console.WriteLine(AnalyzeArray(new[] { 1, 5, 9 }));       // Output: Starts with 1
Console.WriteLine(AnalyzeArray(new[] { 4, 5, 9 }));       // Output: Ends with 9
Console.WriteLine(AnalyzeArray(new[] { 7, 8, 9, 10 }));   // Output: Four-element array
Console.WriteLine(AnalyzeArray(new int[0]));              // Output: Empty array
Console.WriteLine(AnalyzeArray(new[] { 2, 4, 6 }));       // Output: Any three-element array

```

### **Explanation**

1.  **Patterns**:
    - `[1, 2, 3]`: Matches an array with exactly `[1, 2, 3]`.
    - `[_, _, _]`: Matches any array with exactly **three elements**.
    - `[1, ..]`: Matches arrays **starting with `1`**.
    - `[.., 9]`: Matches arrays **ending with `9`**.
    - `[_, _, _, _]`: Matches arrays with **exactly four elements**.
    - `[]`: Matches an **empty array**.
2.  **Default**:
    - The `_` case handles arrays that don't match any other pattern.

---

### **Example 3: Handling Nested Lists**

### **Scenario**

You have nested lists and need to extract specific data based on their structure.

### **Code Example**

```
void AnalyzeNestedList(object[] data)
{
    Console.WriteLine(data switch
    {
        [int x, [.. var inner], string last] =>
            $"Outer int: {x}, Inner: [{string.Join(", ", inner)}], Last: {last}",
        [_, [1, 2, ..], ..] => "Contains [1, 2] at second position",
        _ => "Unknown structure"
    });
}

// Usage
AnalyzeNestedList(new object[] { 42, new int[] { 1, 2, 3, 4 }, "End" });
// Output: Outer int: 42, Inner: [1, 2, 3, 4], Last: End

AnalyzeNestedList(new object[] { "Start", new int[] { 1, 2, 99 }, "End" });
// Output: Contains [1, 2] at second position

AnalyzeNestedList(new object[] { "Hello", 123 });
// Output: Unknown structure

```

### **Explanation**

1.  **Nested Patterns**:
    - `[int x, [.. var inner], string last]`: Matches a sequence where:
      - The first element is an `int`.
      - The second element is a list (captured as `inner`).
      - The last element is a `string`.
    - `[_, [1, 2, ..], ..]`: Matches sequences where:
      - The second element is a list starting with `[1, 2]`.
2.  **Flexible Matching**:
    - The slice (`..`) ensures other elements in the array or sublist are ignored.

---

### **Advantages of List Patterns**

1.  **Simplifies Complex Matching**:
    - Processes irregular or dynamic data structures without verbose loops or conditionals.
2.  **Expressive Syntax**:
    - Matches both the **shape** and **content** of sequences concisely.
3.  **Error Handling**:
    - Provides exhaustive checks and meaningful errors for invalid data.

---

By leveraging **list patterns**, you can elegantly handle complex data structures like arrays, lists, or nested sequences in C#. This feature enhances code readability, correctness, and maintainability.
