### Sorting Data in C#

Sorting operations in LINQ allow you to order the elements of a collection based on one or more attributes. Sorting can
be done in ascending or descending order, and you can also specify secondary sorting criteria for fine-grained control.

---

### **Key Sorting Methods in LINQ**

| **Method Name**     | **Description**                                     | **C# Query Syntax**           | **Example**                               |
| ------------------- | --------------------------------------------------- | ----------------------------- | ----------------------------------------- |
| `OrderBy`           | Sorts values in ascending order.                    | `orderby`                     | `.OrderBy(x => x.Property)`               |
| `OrderByDescending` | Sorts values in descending order.                   | `orderby ... descending`      | `.OrderByDescending(x => x.Property)`     |
| `ThenBy`            | Adds a secondary ascending sort.                    | `orderby ..., ...`            | `.ThenBy(x => x.OtherProperty)`           |
| `ThenByDescending`  | Adds a secondary descending sort.                   | `orderby ..., ... descending` | `.ThenByDescending(x => x.OtherProperty)` |
| `Reverse`           | Reverses the order of the elements in a collection. | Not applicable                | `.Reverse()`                              |

---

### **Sorting Examples**

### **1\. Primary Ascending Sort**

Sort teachers by last name in ascending order.

- **Query Syntax**:

  ```
  IEnumerable<string> query = from teacher in teachers
                              orderby teacher.Last
                              select teacher.Last;

  foreach (string lastName in query)
  {
      Console.WriteLine(lastName);
  }

  ```

- **Method Syntax**:

  ```
  IEnumerable<string> query = teachers
      .OrderBy(teacher => teacher.Last)
      .Select(teacher => teacher.Last);

  foreach (string lastName in query)
  {
      Console.WriteLine(lastName);
  }

  ```

---

### **2\. Primary Descending Sort**

Sort teachers by last name in descending order.

- **Query Syntax**:

  ```
  IEnumerable<string> query = from teacher in teachers
                              orderby teacher.Last descending
                              select teacher.Last;

  foreach (string lastName in query)
  {
      Console.WriteLine(lastName);
  }

  ```

- **Method Syntax**:

  ```
  IEnumerable<string> query = teachers
      .OrderByDescending(teacher => teacher.Last)
      .Select(teacher => teacher.Last);

  foreach (string lastName in query)
  {
      Console.WriteLine(lastName);
  }

  ```

---

### **3\. Secondary Ascending Sort**

Sort teachers by city (primary) and last name (secondary), both in ascending order.

- **Query Syntax**:

  ```
  IEnumerable<(string LastName, string City)> query = from teacher in teachers
                                                      orderby teacher.City, teacher.Last
                                                      select (teacher.Last, teacher.City);

  foreach ((string lastName, string city) in query)
  {
      Console.WriteLine($"City: {city}, Last Name: {lastName}");
  }

  ```

- **Method Syntax**:

  ```
  IEnumerable<(string LastName, string City)> query = teachers
      .OrderBy(teacher => teacher.City)
      .ThenBy(teacher => teacher.Last)
      .Select(teacher => (teacher.Last, teacher.City));

  foreach ((string lastName, string city) in query)
  {
      Console.WriteLine($"City: {city}, Last Name: {lastName}");
  }

  ```

---

### **4\. Secondary Descending Sort**

Sort teachers by city (ascending) and last name (descending).

- **Query Syntax**:

  ```
  IEnumerable<(string LastName, string City)> query = from teacher in teachers
                                                      orderby teacher.City, teacher.Last descending
                                                      select (teacher.Last, teacher.City);

  foreach ((string lastName, string city) in query)
  {
      Console.WriteLine($"City: {city}, Last Name: {lastName}");
  }

  ```

- **Method Syntax**:

  ```
  IEnumerable<(string LastName, string City)> query = teachers
      .OrderBy(teacher => teacher.City)
      .ThenByDescending(teacher => teacher.Last)
      .Select(teacher => (teacher.Last, teacher.City));

  foreach ((string lastName, string city) in query)
  {
      Console.WriteLine($"City: {city}, Last Name: {lastName}");
  }

  ```

---

### **5\. Reverse**

Reverse the order of elements in a collection.

```
IEnumerable<string> reversedNames = teachers
    .Select(teacher => teacher.Last)
    .Reverse();

foreach (string lastName in reversedNames)
{
    Console.WriteLine(lastName);
}

```

---

### **Key Considerations**

1.  **Deferred Execution**: Sorting in LINQ is executed only when the query is enumerated (e.g., with a `foreach` loop).

2.  **Stability**: LINQ sorting methods are stable, meaning the original order of elements is preserved for equal keys.

3.  **Custom Comparers**: Use custom comparers to handle complex sorting scenarios.

    ```
    teachers.OrderBy(t => t.Last, StringComparer.OrdinalIgnoreCase);

    ```

4.  **Query Provider Limitations**: For `IQueryable<T>` (e.g., with EF Core), ensure the sorting logic is compatible
    with the underlying data source.

---

### **See Also**

- [Enumerable.OrderBy](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby)
- [Queryable.OrderBy](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.orderby)
- [LINQ Query Syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/query-syntax-and-method-syntax-in-linq)
