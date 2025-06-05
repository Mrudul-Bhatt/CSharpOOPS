### Explanation of LINQ Grouping Variants

Grouping in LINQ allows data to be organized based on specific criteria. Here, we'll dive into the various ways to group
data: **by a single property**, **by value**, **by range**, **by comparison**, and **by anonymous types**.

---

### **1\. Group by Single Property**

**Definition:**

Grouping by a single property involves using one property of the source object as the key. Each unique value of this
property forms a separate group.

**Example:**

Group students by their year in school.

**Code (Query Syntax):**

```
var groupByYear = from student in students
                  group student by student.Year into yearGroup
                  orderby yearGroup.Key
                  select yearGroup;

foreach (var group in groupByYear)
{
    Console.WriteLine($"Year: {group.Key}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Code (Method Syntax):**

```
var groupByYear = students
    .GroupBy(student => student.Year)
    .OrderBy(group => group.Key);

foreach (var group in groupByYear)
{
    Console.WriteLine($"Year: {group.Key}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Key:** `student.Year`.

---

### **2\. Group by Value**

**Definition:**

Instead of grouping by a property, this involves grouping by a computed value. For instance, you can group students by
the first letter of their last name.

**Example:**

Group students by the first letter of their last name.

**Code (Query Syntax):**

```
var groupByFirstLetter = from student in students
                         let firstLetter = student.LastName[0]
                         group student by firstLetter;

foreach (var group in groupByFirstLetter)
{
    Console.WriteLine($"Key: {group.Key}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.LastName}, {student.FirstName}");
    }
}

```

**Code (Method Syntax):**

```
var groupByFirstLetter = students
    .GroupBy(student => student.LastName[0]);

foreach (var group in groupByFirstLetter)
{
    Console.WriteLine($"Key: {group.Key}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.LastName}, {student.FirstName}");
    }
}

```

**Key:** `student.LastName[0]` (first letter of last name).

---

### **3\. Group by Range**

**Definition:**

Grouping by range involves dividing elements into ranges or intervals, such as score percentiles or age groups.

**Example:**

Group students by their score percentiles. A helper function calculates the percentile based on their average scores.

**Code (Query Syntax):**

```
static int GetPercentile(Student student)
{
    double avg = student.Scores.Average();
    return (int)(avg / 10);
}

var groupByPercentile = from student in students
                        let percentile = GetPercentile(student)
                        group new { student.FirstName, student.LastName } by percentile into percentGroup
                        orderby percentGroup.Key
                        select percentGroup;

foreach (var group in groupByPercentile)
{
    Console.WriteLine($"Percentile: {group.Key * 10}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.LastName}, {student.FirstName}");
    }
}

```

**Code (Method Syntax):**

```
var groupByPercentile = students
    .Select(student => new { student, Percentile = GetPercentile(student) })
    .GroupBy(student => student.Percentile)
    .OrderBy(group => group.Key);

foreach (var group in groupByPercentile)
{
    Console.WriteLine($"Percentile: {group.Key * 10}");
    foreach (var student in group.Select(s => s.student))
    {
        Console.WriteLine($"{student.LastName}, {student.FirstName}");
    }
}

```

**Key:** Percentile computed by `GetPercentile()`.

---

### **4\. Group by Comparison**

**Definition:**

Grouping by comparison involves grouping data based on a Boolean condition or another comparison.

**Example:**

Group students by whether their average exam score is greater than 75.

**Code (Query Syntax):**

```
var groupByHighAverage = from student in students
                         group new { student.FirstName, student.LastName }
                         by student.Scores.Average() > 75 into studentGroup
                         select studentGroup;

foreach (var group in groupByHighAverage)
{
    Console.WriteLine(group.Key ? "High Average" : "Low Average");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Code (Method Syntax):**

```
var groupByHighAverage = students
    .GroupBy(student => student.Scores.Average() > 75);

foreach (var group in groupByHighAverage)
{
    Console.WriteLine(group.Key ? "High Average" : "Low Average");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Key:** Boolean (`true` or `false`), based on average score comparison.

---

### **5\. Group by Anonymous Type**

**Definition:**

This involves using an anonymous type as the key. The anonymous type can encapsulate multiple properties or computed
values into a single key.

**Example:**

Group students by the first letter of their last name and whether their first exam score exceeds 85.

**Code (Query Syntax):**

```
var groupByCompoundKey = from student in students
                         group student by new
                         {
                             FirstLetter = student.LastName[0],
                             IsScoreOver85 = student.Scores[0] > 85
                         } into studentGroup
                         orderby studentGroup.Key.FirstLetter
                         select studentGroup;

foreach (var group in groupByCompoundKey)
{
    Console.WriteLine($"Key: {group.Key.FirstLetter}, Score > 85: {group.Key.IsScoreOver85}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Code (Method Syntax):**

```
var groupByCompoundKey = students
    .GroupBy(student => new
    {
        FirstLetter = student.LastName[0],
        IsScoreOver85 = student.Scores[0] > 85
    })
    .OrderBy(group => group.Key.FirstLetter);

foreach (var group in groupByCompoundKey)
{
    Console.WriteLine($"Key: {group.Key.FirstLetter}, Score > 85: {group.Key.IsScoreOver85}");
    foreach (var student in group)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName}");
    }
}

```

**Key:** Anonymous type containing `FirstLetter` and `IsScoreOver85`.

---

### Summary of Grouping Techniques

| **Grouping Type**     | **Key Example**                  | **Description**                              |
| --------------------- | -------------------------------- | -------------------------------------------- |
| **Single Property**   | `student.Year`                   | Groups by a single property.                 |
| **By Value**          | `student.LastName[0]`            | Groups by a computed value.                  |
| **By Range**          | `GetPercentile(student)`         | Groups by numeric ranges or intervals.       |
| **By Comparison**     | `student.Scores.Average() > 75`  | Groups by a Boolean condition.               |
| **By Anonymous Type** | `{ FirstLetter, IsScoreOver85 }` | Groups by multiple properties or conditions. |

Each grouping technique provides a flexible way to categorize data, enabling efficient queries and insightful analysis.
