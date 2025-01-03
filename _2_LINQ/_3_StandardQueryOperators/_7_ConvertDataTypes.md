### **Converting Data Types in LINQ (C#)**

Conversion methods in LINQ are used to change the data type of input objects or collections. These methods are helpful
for adapting data for specific operations, enabling LINQ queries on non-generic collections, or forcing immediate query
execution.

---

### **Key Points**

1.  **Purpose**:
    - Convert data types to enable querying or processing.
    - Control when a LINQ query executes (deferred or immediate execution).
    - Adapt collections to generic or specific types for flexibility.
2.  **Execution**:
    - Methods starting with `As` (e.g., `AsEnumerable`, `AsQueryable`) change the **static type** of the source but do
      not enumerate it.
    - Methods starting with `To` (e.g., `ToArray`, `ToList`, `ToDictionary`) enumerate the source and convert it into
      the corresponding type.

---

### **Common Conversion Methods**

| **Method Name**  | **Description**                                                                                      | **Execution**       |
| ---------------- | ---------------------------------------------------------------------------------------------------- | ------------------- |
| **AsEnumerable** | Casts input to `IEnumerable<T>` to bypass custom implementations of query operators.                 | Deferred Execution  |
| **AsQueryable**  | Converts an `IEnumerable` to `IQueryable`.                                                           | Deferred Execution  |
| **Cast**         | Casts each element in a collection to a specified type.                                              | Immediate Execution |
| **OfType**       | Filters elements that can be cast to a specified type.                                               | Immediate Execution |
| **ToArray**      | Converts the collection to an array. Forces query execution.                                         | Immediate Execution |
| **ToDictionary** | Converts the collection into a `Dictionary<TKey, TValue>` based on a key selector. Forces execution. | Immediate Execution |
| **ToList**       | Converts the collection to a `List<T>`. Forces query execution.                                      | Immediate Execution |
| **ToLookup**     | Converts the collection into a `Lookup<TKey, TElement>` (a one-to-many dictionary).                  | Immediate Execution |

---

### **Examples**

### **1\. AsEnumerable**

Used to treat the collection as `IEnumerable<T>` and bypass custom implementations of query operators.

```
var query = students.AsEnumerable()
    .Where(s => s.Year == GradeLevel.ThirdYear);

foreach (var student in query)
{
    Console.WriteLine(student.FirstName);
}

```

---

### **2\. Cast**

Casts each element to a specified type.

```
IEnumerable people = students;

var query = people.Cast<Student>()
    .Where(student => student.Year == GradeLevel.ThirdYear);

foreach (var student in query)
{
    Console.WriteLine(student.FirstName);
}

```

---

### **3\. OfType**

Filters elements based on whether they can be cast to a specified type.

```
IEnumerable mixedCollection = new object[] { 1, "two", 3, "four" };

var strings = mixedCollection.OfType<string>();

foreach (var str in strings)
{
    Console.WriteLine(str);
}
// Output:
// two
// four

```

---

### **4\. ToArray**

Converts the collection to an array.

```
var scoreArray = students
    .Where(s => s.Year == GradeLevel.SecondYear)
    .SelectMany(s => s.Scores)
    .ToArray();

```

---

### **5\. ToList**

Converts the collection to a `List<T>`.

```
var studentList = students
    .Where(s => s.Scores.Average() > 80)
    .ToList();

```

---

### **6\. ToDictionary**

Converts the collection to a dictionary.

```
var studentDictionary = students
    .ToDictionary(s => s.ID, s => s.FirstName);

foreach (var kvp in studentDictionary)
{
    Console.WriteLine($"ID: {kvp.Key}, Name: {kvp.Value}");
}

```

---

### **7\. ToLookup**

Creates a `Lookup<TKey, TElement>` (similar to a dictionary but supports multiple values for a single key).

```
var studentsByDepartment = students
    .ToLookup(s => s.DepartmentID);

foreach (var group in studentsByDepartment)
{
    Console.WriteLine($"Department ID: {group.Key}");
    foreach (var student in group)
    {
        Console.WriteLine($"    {student.FirstName}");
    }
}

```

---

### **Use Cases**

1.  **Filtering Non-Generic Collections**:
    - Use `OfType` or `Cast` to query collections without generic types.
2.  **Forcing Immediate Execution**:
    - Use `ToList`, `ToArray`, or `ToDictionary` to execute queries immediately and store results in a specific
      structure.
3.  **Query Adaptation**:
    - Use `AsEnumerable` or `AsQueryable` to adapt collections for LINQ queries.

---

### **Query Expression Syntax Example**

You can cast a type to a subtype explicitly in LINQ query syntax.

```
var query = from Student student in students
            where student.Year == GradeLevel.ThirdYear
            select student;

foreach (Student student in query)
{
    Console.WriteLine(student.FirstName);
}

```

Equivalent using method syntax:

```
IEnumerable people = students;

var query = people.Cast<Student>()
    .Where(student => student.Year == GradeLevel.ThirdYear);

foreach (Student student in query)
{
    Console.WriteLine(student.FirstName);
}

```

---

### **Conclusion**

LINQ conversion methods are powerful tools for adapting, filtering, or restructuring data for queries or processing.
They enable flexible workflows while ensuring that you can handle data types effectively, whether working with generic
collections, non-generic collections, or forcing query execution when needed.
