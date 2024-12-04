### **Introduction to Record Types in C#**

A **record** in C# is a data type designed to make working with **data models** easier. It is a special type of **class
** or **struct** with built-in behavior for:

1. **Value equality**: Two records are equal if their types match and all their property values are equal.
2. **Immutability**: Records are often used for types whose properties should not change after creation.

* * * * *

### **When to Use Records**

Use records instead of classes or structs in the following scenarios:

- **Value Equality**: You want two instances with the same data to be considered equal.
- **Immutability**: You need a type where data cannot be modified after creation (e.g., for thread safety).

* * * * *

### **1\. Value Equality**

Unlike regular classes, which use **reference equality** by default (two instances are only equal if they refer to the
same memory), records use **value equality**.

### **Example: Value Equality in Records**

```
public record Person(string Name, int Age);

var person1 = new Person("Alice", 30);
var person2 = new Person("Alice", 30);

Console.WriteLine(person1 == person2); // True, because their values are equal.

```

### **Example: Reference Equality in Classes**

```
public class PersonClass
{
    public string Name { get; init; }
    public int Age { get; init; }

    public PersonClass(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

var person1 = new PersonClass("Alice", 30);
var person2 = new PersonClass("Alice", 30);

Console.WriteLine(person1 == person2); // False, because they are different objects.

```

* * * * *

### **2\. Immutability**

Records provide concise syntax for **immutable types**. Once a record is created, its properties cannot be changed. This
is helpful when data consistency or thread safety is critical.

### **Example: Immutable Record**

```
public record Book(string Title, string Author);

var book = new Book("1984", "George Orwell");
// book.Title = "Animal Farm"; // Error: Properties of a record are readonly.

```

To create a new instance with modified properties, use the **`with` expression**:

```
var updatedBook = book with { Title = "Animal Farm" };
Console.WriteLine(updatedBook.Title); // "Animal Farm"

```

* * * * *

### **3\. Records as Data Models**

Records are often used in scenarios where data objects are treated as values rather than entities, such as **DTOs (Data
Transfer Objects)** or **data moeling**.

### **Example: Creating a Record**

```
public record Employee(int Id, string Name, decimal Salary);

```

This single line automatically generates:

- A constructor to initialize the properties.

- Value-based equality (`==`, `!=`).

- Overridden `ToString()` method for better readability:

  ```
  var employee = new Employee(1, "John Doe", 50000m);
  Console.WriteLine(employee); // Output: Employee { Id = 1, Name = John Doe, Salary = 50000 }

  ```

* * * * *

### **When Not to Use Records**

1. **Reference Equality Required**: Use classes for scenarios where two variables must refer to the same object (e.g., *
   *Entity Framework Core** entities).
2. **Mutable Data**: If you need properties to change after creation, use a class or struct instead.

### **Example: EF Core Entity**

```
// Use classes for EF Core entities to ensure reference equality.
public class Order
{
    public int Id { get; set; }
    public string Product { get; set; }
}

```

* * * * *

### **Summary**

Records in C# simplify the creation of **immutable** and **value-equality** types, making them ideal for use in data
models, DTOs, and **scenarios where thread safety is crucial**. However, they are not suitable for use cases like **ORM
entities** where reference equality or mutability is required

### **Summary Table**

| Feature                   | **Records**                    | **Classes**                  | **Structs**                  | **Record Structs** |
|---------------------------|--------------------------------|------------------------------|------------------------------|--------------------|
| **Default Equality**      | Value Equality                 | Reference Equality           | Value Equality               | Value Equality     |
| **Immutable Properties**  | Supported (with `init`)        | Manual Implementation Needed | Not Supported                | Not Supported      |
| **`with` Expression**     | Supported                      | Not Supported                | Not Supported                | Supported          |
| **Enhanced `ToString()`** | Automatic                      | Manual Implementation Needed | Manual Implementation Needed | Automatic          |
| **Inheritance**           | Records Only (record → record) | Classes Only (class → class) | Not Supported                | Not Supported      |