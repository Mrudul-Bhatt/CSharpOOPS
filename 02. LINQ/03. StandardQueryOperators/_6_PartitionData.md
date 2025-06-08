### Partitioning Data in LINQ (C#)

Partitioning in LINQ refers to dividing an input sequence into sections based on specific criteria and returning one of
the sections. The operation does not rearrange the elements, preserving their original order.

---

### **Key Partitioning Methods in LINQ**

| **Method Name** | **Description**                                                                                           | **Syntax in Query Expressions** | **Method Syntax**               |
| --------------- | --------------------------------------------------------------------------------------------------------- | ------------------------------- | ------------------------------- |
| `Take`          | Returns the first `n` elements of a sequence.                                                             | Not applicable                  | `sequence.Take(n)`              |
| `TakeWhile`     | Returns elements from a sequence **while a condition is true**. Stops at the first element that fails.    | Not applicable                  | `sequence.TakeWhile(predicate)` |
| `Skip`          | Skips the first `n` elements of a sequence and returns the rest.                                          | Not applicable                  | `sequence.Skip(n)`              |
| `SkipWhile`     | Skips elements from the sequence **while a condition is true**. Starts returning after the first failure. | Not applicable                  | `sequence.SkipWhile(predicate)` |
| `Chunk`         | Splits the sequence into chunks of a specified size.                                                      | Not applicable                  | `sequence.Chunk(size)`          |

---

### **Examples**

### **1\. Using `Take`**

The `Take` method returns the first `n` elements of a sequence.

**Example**:

```
foreach (int number in Enumerable.Range(0, 8).Take(3))
{
    Console.WriteLine(number);
}
// Output:
// 0
// 1
// 2

```

---

### **2\. Using `Skip`**

The `Skip` method skips the first `n` elements and returns the remaining ones.

**Example**:

```
foreach (int number in Enumerable.Range(0, 8).Skip(3))
{
    Console.WriteLine(number);
}
// Output:
// 3
// 4
// 5
// 6
// 7

```

---

### **3\. Using `TakeWhile`**

The `TakeWhile` method returns elements **while a condition is true**. It stops at the first element that does not
satisfy the condition.

**Example**:

```
foreach (int number in Enumerable.Range(0, 8).TakeWhile(n => n < 5))
{
    Console.WriteLine(number);
}
// Output:
// 0
// 1
// 2
// 3
// 4

```

---

### **4\. Using `SkipWhile`**

The `SkipWhile` method skips elements **while a condition is true** and starts returning elements when the condition
fails.

**Example**:

```
foreach (int number in Enumerable.Range(0, 8).SkipWhile(n => n < 5))
{
    Console.WriteLine(number);
}
// Output:
// 5
// 6
// 7

```

---

### **5\. Using `Chunk`**

The `Chunk` method divides a sequence into chunks of a specified size. Each chunk is an array.

**Example**:

```
int chunkNumber = 1;
foreach (int[] chunk in Enumerable.Range(0, 8).Chunk(3))
{
    Console.WriteLine($"Chunk {chunkNumber++}:");
    foreach (int item in chunk)
    {
        Console.WriteLine($"    {item}");
    }
    Console.WriteLine();
}
// Output:
// Chunk 1:
//     0
//     1
//     2
//
// Chunk 2:
//     3
//     4
//     5
//
// Chunk 3:
//     6
//     7

```

---

### **Key Considerations**

1.  **Deferred Execution**:
    - These methods do not execute immediately but are deferred until the resulting sequence is enumerated.
2.  **Preserved Order**:
    - The original order of elements in the sequence is always preserved during partitioning.
3.  **Chunking**:
    - The `Chunk` operator is especially useful for splitting large datasets into manageable parts.
4.  **Combination**:

    - These methods can be combined for complex partitioning operations:

      ```
      var result = sequence.Skip(2).Take(3);

      ```

---

### **Real-World Use Cases**

- **Pagination**: Use `Skip` and `Take` for fetching specific ranges of data, such as paginating through records in a
  database.

  ```
  var pageItems = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);

  ```

- **Conditional Partitioning**: Use `TakeWhile` and `SkipWhile` for filtering based on conditions while preserving
  order.

- **Batch Processing**: Use `Chunk` to divide data into manageable groups for processing.

---

### **See Also**

- [Enumerable.Take Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take)
- [Enumerable.Skip Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skip)
- [Enumerable.Chunk Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk)
