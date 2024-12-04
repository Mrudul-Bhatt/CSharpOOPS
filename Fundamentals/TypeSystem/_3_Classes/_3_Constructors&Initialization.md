### **Constructors and Initialization in C#**

When creating an instance of a class, it is crucial to initialize its fields and properties properly. C# provides various mechanisms to initialize objects, ranging from default values to using constructors and object initializers.

---

### **1\. Default Values**

- Every .NET type has a **default value**:
  - **Value types**: Defaults are typically `0`, `false`, etc.
  - **Reference types**: The default is `null`.

### **Example of Default Values:**

```
public class Sample
{
    public int Number;       // Default: 0
    public string Name;      // Default: null
    public bool IsActive;    // Default: false
}

var obj = new Sample();
Console.WriteLine(obj.Number);   // Output: 0
Console.WriteLine(obj.Name);     // Output: (null)
Console.WriteLine(obj.IsActive); // Output: False

```

---

### **2\. Field Initializers**

- You can set default values for fields directly in their declaration.
- Useful when a specific value is consistently required as the starting point.

### **Example:**

```
public class Container
{
    // Field initializer for the `_capacity` field
    private int _capacity = 10;

    public void ShowCapacity()
    {
        Console.WriteLine($"Capacity: {_capacity}");
    }
}

var container = new Container();
container.ShowCapacity(); // Output: Capacity: 10

```

---

### **3\. Constructors**

A **constructor** is a special method used to initialize objects. It has the same name as the class and is invoked when you create a new object.

### **Key Points:**

- Constructors can take parameters to set initial values.
- If no constructor is defined, the compiler provides a **default parameterless constructor**.

### **Example: Using Constructor Parameters**

```
public class Container
{
    private int _capacity;

    // Constructor with a parameter to set `_capacity`
    public Container(int capacity)
    {
        _capacity = capacity;
    }

    public void ShowCapacity()
    {
        Console.WriteLine($"Capacity: {_capacity}");
    }
}

var container = new Container(20);
container.ShowCapacity(); // Output: Capacity: 20

```

---

### **4\. Primary Constructors (C# 12)**

Introduced in C# 12, **primary constructors** simplify initialization by combining class declaration and constructor definition.

### **Example: Primary Constructor**

```
public class Container(int capacity)
{
    private int _capacity = capacity;

    public void ShowCapacity()
    {
        Console.WriteLine($"Capacity: {_capacity}");
    }
}

var container = new Container(30);
container.ShowCapacity(); // Output: Capacity: 30

```

---

### **5\. Required Properties**

C# allows the use of the **`required`** keyword to enforce property initialization. This is useful when some properties must always be set when creating an object.

### **Example: Required Properties**

```
public class Person
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

// Usage
// var p1 = new Person(); // Error: Required properties not set

var p2 = new Person() { FirstName = "Grace", LastName = "Hopper" };
Console.WriteLine($"{p2.FirstName} {p2.LastName}"); // Output: Grace Hopper

```

---

### **6\. Object Initializers**

With object initializers, you can assign values to fields or properties directly when creating an object. This makes code more concise and readable.

### **Example: Using Object Initializers**

```
public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

var person = new Person
{
    FirstName = "Alan",
    LastName = "Turing"
};

Console.WriteLine($"{person.FirstName} {person.LastName}"); // Output: Alan Turing

```

---

### **Comparison of Initialization Methods**

| **Method**               | **Advantages**                                                              | **Example**                                    |
| ------------------------ | --------------------------------------------------------------------------- | ---------------------------------------------- |
| **Default Values**       | Simplest, automatic for all types.                                          | `int x;` (default is `0`)                      |
| **Field Initializers**   | Consistent starting values for fields without additional code.              | `private int _capacity = 10;`                  |
| **Constructors**         | Provides flexibility for setting required values when an object is created. | `new Container(20)`                            |
| **Primary Constructors** | Reduces boilerplate code, especially for short-lived types (C# 12+).        | `public class Container(int capacity) { ... }` |
| **Required Properties**  | Enforces initialization for critical properties, ensuring data integrity.   | `required string FirstName { get; set; }`      |
| **Object Initializers**  | Simple and readable for setting multiple properties in one step.            | `new Person { FirstName = "Alan" }`            |

---

### **Conclusion**

- Constructors, field initializers, and object initializers provide various levels of control over how objects are initialized.
- **Use default values** when appropriate but rely on **constructors or required properties** for important or critical fields.
- **Primary constructors** and **object initializers** simplify code and improve readability, especially for lightweight types.
