### Dynamically Specifying Predicate Filters at Runtime in LINQ

In some scenarios, you may need to define filters dynamically based on runtime conditions. LINQ allows this flexibility,
making it easy to apply filters that vary depending on the data or user inputs.

* * * * *

### **1\. Using the `Contains` Method for Dynamic Filters**

The `Contains` method is often used to create dynamic filters. By checking whether a property value exists in a
collection, you can filter data dynamically.

### Example:

```
int[] ids = { 111, 114, 112 };

var queryNames =
    from student in students
    where ids.Contains(student.ID)
    select new
    {
        student.LastName,
        student.ID
    };

foreach (var name in queryNames)
{
    Console.WriteLine($"{name.LastName}: {name.ID}");
}

/* Output:
   Omelchenko: 111
   Garcia: 114
   O'Donnell: 112
*/

// Change the ids dynamically.
ids = { 122, 117, 120, 115 };

// The query now returns results based on the updated `ids` array.
foreach (var name in queryNames)
{
    Console.WriteLine($"{name.LastName}: {name.ID}");
}

/* Output:
   Tucker: 122
   Feng: 117
   Adams: 120
   Garcia: 115
*/

```

### Key Points:

- **Dynamic Behavior:** The `ids` array can be modified at runtime, and the query automatically adapts to these changes
  when executed.
- **Deferred Execution:** LINQ queries like this are not executed until enumerated (e.g., in the `foreach` loop). This
  ensures they use the latest state of the `ids` array.

* * * * *

### **2\. Dynamic Filters with Conditional Logic**

You can apply different filters using control flow statements like `if-else` or `switch`. This approach dynamically
modifies the query based on runtime conditions.

### Example: Using Conditional Logic

```
void FilterByYearType(bool oddYear)
{
    IEnumerable<Student> studentQuery = oddYear
        ? (from student in students
           where student.Year is GradeLevel.FirstYear or GradeLevel.ThirdYear
           select student)
        : (from student in students
           where student.Year is GradeLevel.SecondYear or GradeLevel.FourthYear
           select student);

    var descr = oddYear ? "odd" : "even";
    Console.WriteLine($"The following students are at an {descr} year level:");
    foreach (Student name in studentQuery)
    {
        Console.WriteLine($"{name.LastName}: {name.ID}");
    }
}

FilterByYearType(true);
/* Output:
   The following students are at an odd year level:
   Fakhouri: 116
   Feng: 117
   Garcia: 115
   Mortensen: 113
   Tucker: 119
   Tucker: 122
*/

FilterByYearType(false);
/* Output:
   The following students are at an even year level:
   Adams: 120
   Garcia: 114
   Garcia: 118
   O'Donnell: 112
   Omelchenko: 111
   Zabokritski: 121
*/

```

### Key Points:

- **Flexible Queries:** Use `if-else`, `switch`, or the ternary operator to choose among predefined queries.
- **Readable Output:** Dynamically include information in the output (`odd` or `even` in this case) for better user
  feedback.

* * * * *

### **3\. Advantages of Dynamic Filters**

1. **Reusability:** Dynamic filters make queries reusable for multiple scenarios by simply modifying filter conditions.
2. **Deferred Execution:** LINQ queries execute when enumerated, ensuring they use the latest filter values at runtime.
3. **Customizability:** Easily adapt queries to user inputs or runtime conditions without hardcoding filters.

* * * * *

### **4\. Combining Dynamic Filters with Lambda Expressions**

Dynamic filtering can also be achieved using **method syntax** with lambda expressions. This is a more compact
alternative for certain scenarios.

### Example: Method Syntax with Dynamic Filters

```
int[] ids = { 111, 114, 112 };

var filteredStudents = students
    .Where(student => ids.Contains(student.ID))
    .Select(student => new { student.LastName, student.ID });

foreach (var student in filteredStudents)
{
    Console.WriteLine($"{student.LastName}: {student.ID}");
}

```

* * * * *

### **Conclusion**

Dynamic predicate filters in LINQ provide powerful tools to tailor queries at runtime:

- Use `Contains` for simple dynamic conditions.
- Combine LINQ with control flow statements for more complex logic.
- Leverage deferred execution to ensure filters reflect runtime changes.

This makes LINQ queries highly adaptable to evolving runtime scenarios while maintaining readability and efficiency.