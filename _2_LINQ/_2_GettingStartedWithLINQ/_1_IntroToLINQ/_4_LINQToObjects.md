### **LINQ to Objects**

**LINQ to Objects** allows querying in-memory data collections using LINQ queries. It operates on any collection that
implements `IEnumerable` or `IEnumerable<T>`. Examples include arrays, lists, dictionaries, and other collection types.
LINQ to Objects is ideal for filtering, sorting, grouping, or transforming data in these collections with a declarative
coding style.

* * * * *

### **Key Advantages**

1. **Conciseness and Readability:** LINQ queries are typically more concise and easier to understand than traditional
   loops, especially for complex operations.
2. **Powerful Query Capabilities:** LINQ supports advanced operations like filtering, ordering, and grouping with
   minimal code.
3. **Portability:** Queries written for one data source can often be adapted to work with other LINQ-enabled sources,
   such as databases or XML, with minimal changes.

* * * * *

### **Example: LINQ Query on a List**

### **Code**

```
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Data source: A list of integers
        List<int> numbers = new List<int> { 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

        // LINQ query: Find numbers that are divisible by 4
        IEnumerable<int> queryFactorsOfFour =
            from num in numbers
            where num % 4 == 0
            select num;

        // Lazy execution: Query is not evaluated yet
        Console.WriteLine("Query created but not executed.");

        // Force execution and store results in memory
        List<int> factorsOfFourList = queryFactorsOfFour.ToList();

        // Access and modify the results
        Console.WriteLine($"Third element: {factorsOfFourList[2]}"); // Output: 12
        factorsOfFourList[2] = 0; // Modify the result
        Console.WriteLine($"Modified third element: {factorsOfFourList[2]}"); // Output: 0

        // Enumerate the results
        Console.WriteLine("Factors of Four:");
        foreach (var factor in factorsOfFourList)
        {
            Console.WriteLine(factor);
        }
    }
}

```

* * * * *

### **Explanation**

1. **Data Source:** The data source is a `List<int>` containing integers.
2. **Query Creation:** The LINQ query filters numbers divisible by 4 using the `where` clause.
3. **Lazy Execution:** The query is not executed until the results are enumerated (e.g., via a `foreach` loop or calling
   `ToList`).
4. **Storing Results:** The `ToList` method forces immediate execution and caches the query results in memory, allowing
   subsequent modifications or repeated access without re-executing the query.
5. **Modifications:** After storing results in a `List`, elements can be accessed and modified like any other list.

* * * * *

### **Using LINQ Instead of Loops**

### **Traditional Approach**

```
List<int> factorsOfFour = new List<int>();
foreach (int num in numbers)
{
    if (num % 4 == 0)
    {
        factorsOfFour.Add(num);
    }
}

```

### **LINQ Approach**

```
List<int> factorsOfFour =
    (from num in numbers
     where num % 4 == 0
     select num).ToList();

```

- **LINQ reduces boilerplate code** and clearly expresses the developer's intent, making it more readable and
  maintainable.

* * * * *

### **Other Ways to Store Query Results**

- **`ToArray`:** Converts query results into an array.

  ```
  int[] factorsOfFourArray = queryFactorsOfFour.ToArray();

  ```

- **`ToDictionary`:** Converts results into a dictionary, requiring a key selector.

  ```
  var factorsDictionary = queryFactorsOfFour.ToDictionary(f => f, f => f * f);

  ```

- **`ToLookup`:** Creates a lookup, grouping results by a key.

  ```
  var factorsLookup = queryFactorsOfFour.ToLookup(f => f % 8 == 0);

  ```

* * * * *

### **Output of Example**

```
Query created but not executed.
Third element: 12
Modified third element: 0
Factors of Four:
4
8
0
16
20

```

This demonstrates how LINQ queries operate lazily, store results, and allow manipulation of query outcomes when forced
into memory.