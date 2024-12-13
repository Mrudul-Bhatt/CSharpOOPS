### **Language Integrated Query (LINQ) Overview**

Language Integrated Query (LINQ) allows querying various data sources such as objects, databases, XML, and more, using a
consistent syntax directly within the C# language. LINQ queries are strongly typed, provide compile-time checking, and
often reduce code complexity compared to traditional approaches.

* * * * *

### **Key Features of LINQ**

1. **Consistent Syntax**

   LINQ provides a unified query experience for different data sources (e.g., arrays, lists, databases, XML).

2. **Type-Safety and IntelliSense**

   LINQ queries are strongly typed, meaning errors can be caught during compilation, and developers benefit from
   IntelliSense in IDEs.

3. **Deferred Execution**

   Queries aren't executed until the results are enumerated (e.g., in a `foreach` loop).

4. **Declarative Query Syntax**

   LINQ queries are written declaratively, making them more readable.

5. **Integration with C# Language Constructs**

   LINQ integrates seamlessly into C# and uses familiar constructs such as `from`, `where`, `select`, and lambda
   expressions.

* * * * *

### **Example: LINQ to Objects**

### **Scenario**

You have a collection of integers, and you want to filter out numbers greater than 80.

```
using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Data source
        int[] scores = { 97, 92, 81, 60 };

        // LINQ query: Query syntax
        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            select score;

        // Execute the query
        Console.WriteLine("Scores above 80:");
        foreach (int score in scoreQuery)
        {
            Console.WriteLine(score);
        }

        // LINQ query: Method syntax (alternative)
        var scoreQueryMethod = scores.Where(score => score > 80);
        Console.WriteLine("\\nScores above 80 (method syntax):");
        foreach (int score in scoreQueryMethod)
        {
            Console.WriteLine(score);
        }
    }
}

```

**Output:**

```
Scores above 80:
97
92
81

Scores above 80 (method syntax):
97
92
81

```

* * * * *

### **Components of LINQ**

1. **Query Syntax**

    - Uses keywords like `from`, `where`, `select`.

    - Example:

      ```
      var query = from item in collection
                  where item > 10
                  select item;

      ```

2. **Method Syntax**

    - Uses extension methods like `Where`, `Select`.

    - Example:

      ```
      var query = collection.Where(item => item > 10);

      ```

* * * * *

### **Advanced Examples**

### **1\. LINQ with Collections**

```
var names = new List<string> { "Alice", "Bob", "Charlie", "Dave" };

// Find names with length > 3
var longNames = from name in names
                where name.Length > 3
                select name;

Console.WriteLine("Names with more than 3 characters:");
foreach (var name in longNames)
{
    Console.WriteLine(name);
}

```

* * * * *

### **2\. LINQ with Sorting and Grouping**

```
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
    foreach (var employee in group)
    {
        Console.WriteLine($"  {employee.Name}");
    }
}

```

**Output:**

```
Department: HR
  Alice
  Charlie
Department: IT
  Bob
  Dave

```

* * * * *

### **3\. LINQ with Aggregation**

```
int[] numbers = { 1, 2, 3, 4, 5 };

// Sum and Average using LINQ
int sum = numbers.Sum();
double average = numbers.Average();

Console.WriteLine($"Sum: {sum}, Average: {average}");

```

**Output:**

```
Sum: 15, Average: 3

```

* * * * *

### **When to Use LINQ**

- When working with collections or datasets that require filtering, grouping, or transformation.
- To simplify and make the code more readable.
- For scenarios involving SQL-like operations (e.g., filtering and sorting).

* * * * *

### **Benefits of LINQ**

1. **Code Readability**: Declarative syntax makes it easier to understand.
2. **Error Detection**: Strongly typed queries help catch errors at compile time.
3. **Reusability**: LINQ queries can be stored in variables for later execution.
4. **Unified Approach**: Use the same syntax for different data sources.

* * * * *

### **Conclusion**

LINQ enhances the way you work with data in C# by offering a concise, type-safe, and readable way to query and
manipulate collections. Whether you are dealing with objects, databases, or XML, LINQ provides a consistent and
efficient approach to querying and transforming data.