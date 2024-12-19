### **Example: Query Syntax in LINQ**

**Query Syntax** allows you to write SQL-like queries in a declarative style to filter, order, and group data. The query
is represented as an expression, and execution happens only when iterated (e.g., in a `foreach` loop).

* * * * *

### **1\. Filtering Results**

The **`where` clause** is used to filter the source sequence based on a condition.

### Example Query 1:

```
List<int> numbers = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

// Query #1: Filter numbers less than 3 or greater than 7
IEnumerable<int> filteringQuery =
    from num in numbers
    where num < 3 || num > 7
    select num;

// Execution
foreach (var num in filteringQuery)
{
    Console.Write(num + " ");  // Output: 9 8 7 2 1 0
}

```

* * * * *

### **2\. Ordering Results**

The **`orderby` clause** sorts results in ascending (default) or descending order.

### Example Query 2:

```
// Query #2: Order filtered results
IEnumerable<int> orderingQuery =
    from num in numbers
    where num < 3 || num > 7
    orderby num ascending
    select num;

// Execution
foreach (var num in orderingQuery)
{
    Console.Write(num + " ");  // Output: 0 1 2 8 9
}

```

* * * * *

### **3\. Grouping Results**

The **`group` clause** groups data into collections based on a key.

### Example Query 3:

```
string[] groupingQuery = { "carrots", "cabbage", "broccoli", "beans", "barley" };

// Query #3: Group words by the first letter
IEnumerable<IGrouping<char, string>> queryFoodGroups =
    from item in groupingQuery
    group item by item[0];

// Execution
foreach (var group in queryFoodGroups)
{
    Console.WriteLine($"Group {group.Key}:");
    foreach (var item in group)
    {
        Console.WriteLine($"  {item}");
    }
}

// Output:
// Group c:
//   carrots
//   cabbage
// Group b:
//   broccoli
//   beans
//   barley

```

* * * * *

### **Query Execution**

- LINQ queries like filtering, ordering, and grouping return **`IEnumerable<T>`**.
- Queries are **lazy-evaluated**, meaning they don't execute until iterated (e.g., using `foreach`).

To simplify code, **`var`** can be used to infer query types:

```
var query = from num in numbers where num < 3 select num;

```

* * * * *

### **Example: Method Syntax in LINQ**

Method syntax uses **extension methods** (like `Where`, `Select`, etc.) to perform operations. Some operations, like
aggregations, must use method syntax.

* * * * *

### **4\. Aggregation**

Methods like `Sum`, `Average`, `Max`, etc., return a **single value** and are executed immediately.

### Example Query 4:

```
List<int> numbers1 = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

// Query #4: Compute the average
double average = numbers1.Average();

Console.WriteLine(average);  // Output: 4.5

```

* * * * *

### **5\. Concatenating Sequences**

The **`Concat` method** combines two sequences into one.

### Example Query 5:

```
List<int> numbers2 = new List<int> { 15, 14, 11, 13, 19, 18, 16, 17, 12, 10 };

// Query #5: Concatenate two lists
IEnumerable<int> concatenationQuery = numbers1.Concat(numbers2);

// Execution
foreach (var num in concatenationQuery)
{
    Console.Write(num + " ");
}
// Output: 5 4 1 3 9 8 6 7 2 0 15 14 11 13 19 18 16 17 12 10

```

* * * * *

### **6\. Filtering with Lambdas**

The **`Where` method** uses a **lambda expression** for filtering.

### Example Query 6:

```
// Query #6: Filter numbers greater than 15
IEnumerable<int> largeNumbersQuery = numbers2.Where(c => c > 15);

// Execution
foreach (var num in largeNumbersQuery)
{
    Console.Write(num + " ");
}
// Output: 19 18 16 17

```

* * * * *

### **Query Execution and Deferred Execution**

1. **Deferred Execution:** Queries like `Where` and `Select` return `IEnumerable<T>`, which means they don't execute
   until iterated.

   ```
   var deferredQuery = numbers1.Where(num => num > 5);
   // No filtering is done yet.

   foreach (var num in deferredQuery)
   {
       Console.WriteLine(num); // Execution happens here.
   }

   ```

2. **Immediate Execution:** Aggregation methods (`Sum`, `Average`, etc.) execute immediately and return a single value.

   ```
   double sum = numbers1.Sum(); // Execution happens here.

   ```

* * * * *

### **Key Differences: Query Syntax vs. Method Syntax**

| **Aspect**       | **Query Syntax**                             | **Method Syntax**                                             |
|------------------|----------------------------------------------|---------------------------------------------------------------|
| **Style**        | Declarative, SQL-like                        | Functional, chaining of method calls                          |
| **Readability**  | Easier for filtering, ordering, grouping     | Flexible for custom conditions                                |
| **Required For** | Common operations like filtering or grouping | Aggregations (`Sum`, `Average`, etc.)                         |
| **Execution**    | Deferred unless iterated                     | Depends on method (e.g., `Where`: deferred, `Sum`: immediate) |

Both approaches are interchangeable and can be mixed to utilize their respective strengths.