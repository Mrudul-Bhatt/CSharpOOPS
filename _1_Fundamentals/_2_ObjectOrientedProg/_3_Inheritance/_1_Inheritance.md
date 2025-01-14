### **Understanding Inheritance in C#**

Inheritance allows you to define a new class (derived class) that reuses, extends, or modifies behavior from an existing
class (base class). It is one of the pillars of object-oriented programming (OOP), along with encapsulation and
polymorphism.

* * * * *

### **Key Concepts**

1. **Base Class**: The class whose members (fields, properties, methods, etc.) are inherited.
2. **Derived Class**: The class that inherits members from the base class.
3. **Transitive Nature**: Inheritance is transitive; a class can inherit members from a hierarchy of base classes.
4. **Access Modifiers**:
    - Members with `public` and `protected` access are accessible to the derived class.
    - `private` members are not directly accessible but can be used indirectly via methods.

* * * * *

### **C# Example**

### **Base Class: `WorkItem`**

The `WorkItem` class models a generic task with attributes like `ID`, `Title`, and `Description`. It includes methods
like `Update` and overrides `ToString`.

### **Derived Class: `ChangeRequest`**

The `ChangeRequest` class specializes the `WorkItem` by adding the `originalItemID` property to track related items. It
inherits all properties and methods from `WorkItem`.

* * * * *

### **Code Example**

```
using System;

public class WorkItem
{
    private static int currentID;
    protected int ID { get; set; }
    protected string Title { get; set; }
    protected string Description { get; set; }
    protected TimeSpan JobLength { get; set; }

    // Default constructor
    public WorkItem()
    {
        ID = 0;
        Title = "Default Title";
        Description = "Default Description";
        JobLength = new TimeSpan();
    }

    // Parameterized constructor
    public WorkItem(string title, string description, TimeSpan jobLength)
    {
        ID = GetNextID();
        Title = title;
        Description = description;
        JobLength = jobLength;
    }

    // Static constructor
    static WorkItem() => currentID = 0;

    // Increment and get next ID
    protected int GetNextID() => ++currentID;

    // Update method
    public void Update(string title, TimeSpan jobLength)
    {
        Title = title;
        JobLength = jobLength;
    }

    // Override ToString
    public override string ToString() => $"{ID} - {Title}";
}

public class ChangeRequest : WorkItem
{
    protected int OriginalItemID { get; set; }

    // Default constructor
    public ChangeRequest() { }

    // Parameterized constructor
    public ChangeRequest(string title, string description, TimeSpan jobLength, int originalID)
        : base(title, description, jobLength)
    {
        OriginalItemID = originalID;
    }
}

class Program
{
    static void Main()
    {
        // Creating an instance of WorkItem
        WorkItem item = new WorkItem("Fix Bugs", "Fix all bugs in code", new TimeSpan(2, 0, 0));
        Console.WriteLine(item.ToString());

        // Creating an instance of ChangeRequest
        ChangeRequest change = new ChangeRequest("Add Features", "Implement new features", new TimeSpan(3, 0, 0), 1);

        // Using inherited method
        change.Update("Update Feature Implementation", new TimeSpan(4, 0, 0));

        // Display the ChangeRequest
        Console.WriteLine(change.ToString());
    }
}

```

* * * * *

### **Explanation of Features**

1. **Base Class Usage**:
    - `WorkItem` acts as the base class.
    - Defines reusable fields and methods like `Update` and `ToString`.
2. **Derived Class Features**:
    - `ChangeRequest` inherits `WorkItem` properties and methods.
    - Extends functionality by adding the `OriginalItemID` property and custom constructors.
3. **Behavior**:
    - `ChangeRequest` reuses and overrides the base class's methods.
    - Calls the `WorkItem` constructor using the `base` keyword in the parameterized constructor.

* * * * *

### **Output**

```
1 - Fix Bugs
2 - Update Feature Implementation

```

* * * * *

### **Key Notes**

- **Encapsulation with Inheritance**: Use `protected` for base class members that should be accessible in derived
  classes but hidden from external code.
- **Polymorphism**: Virtual methods in the base class can be overridden in the derived class for custom behavior.
- **Constructors**:
    - Constructors are not inherited.
    - The base class's default constructor is automatically called if not specified.
    - You can explicitly call a base constructor using `base()`.

* * * * *

### **When to Use Inheritance**

1. To create a hierarchy of related classes.
2. To reuse code from the base class.
3. When the "is-a" relationship applies (e.g., a `Dog` is an `Animal`).

Inheritance promotes code reuse and specialization, making it a cornerstone of OOP design.