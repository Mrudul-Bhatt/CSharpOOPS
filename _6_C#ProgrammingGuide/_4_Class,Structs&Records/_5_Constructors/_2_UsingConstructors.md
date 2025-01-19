### **Using Constructors in C#**

A **constructor** is a special method used to initialize an object when it is created. It sets up the class or struct instance by initializing its fields, properties, or executing any logic necessary before the object is used.

---

### **Key Characteristics of Constructors**
1. **Same Name as the Class**:
   - A constructor has the same name as the class or struct it belongs to.
   - It does not have a return type, not even `void`.

2. **Automatic Invocation**:
   - Constructors are called automatically when an object is created using the `new` keyword.

3. **Customizable Initialization**:
   - Constructors can take parameters to customize the object initialization.

---

### **Types of Constructors**
#### **1. Parameterless Constructor**
- A constructor with no parameters.
- The C# compiler automatically generates a parameterless constructor if none is defined (except for static classes).

**Example**:
```csharp
public class Taxi
{
    private string taxiTag;

    // Parameterless constructor
    public Taxi() 
    {
        taxiTag = "DefaultTag";
    }
}
```

#### **2. Parameterized Constructor**
- Accepts arguments to initialize object properties or fields.

**Example**:
```csharp
public class Taxi
{
    private string taxiTag;

    // Parameterized constructor
    public Taxi(string tag) => taxiTag = tag;

    public override string ToString() => $"Taxi: {taxiTag}";
}
```

Usage:
```csharp
Taxi t = new Taxi("Tag123");
Console.WriteLine(t); // Output: Taxi: Tag123
```

#### **3. Static Constructor**
- Initializes static members of the class.
- Called automatically before any static member is accessed.
- Runs **once** per application domain.

**Example**:
```csharp
public class Configuration
{
    public static string DefaultConfig;

    // Static constructor
    static Configuration() 
    {
        DefaultConfig = "Default Settings";
    }
}
```

#### **4. Primary Constructor** (Introduced in C# 12)
- Declares required parameters directly in the class definition.

**Example**:
```csharp
public class LabelledItem(string label)
{
    public string Label { get; } = label;
}
```

Usage:
```csharp
var item = new LabelledItem("Important Item");
Console.WriteLine(item.Label); // Output: Important Item
```

#### **5. Private Constructor**
- Prevents the instantiation of a class from outside its definition.
- Commonly used in singleton patterns or utility classes.

**Example**:
```csharp
public class Singleton
{
    private static readonly Singleton instance = new Singleton();

    // Private constructor
    private Singleton() { }

    public static Singleton Instance => instance;
}
```

---

### **Constructor Overloading**
- A class can define multiple constructors with different parameter lists.
- The appropriate constructor is chosen based on the arguments passed during instantiation.

**Example**:
```csharp
public class Employee
{
    public int Salary;

    public Employee() { }

    public Employee(int annualSalary) => Salary = annualSalary;

    public Employee(int weeklySalary, int weeks) => Salary = weeklySalary * weeks;
}
```

Usage:
```csharp
var emp1 = new Employee();
var emp2 = new Employee(50000);
var emp3 = new Employee(1000, 52);
```

---

### **Calling Base Class Constructors**
A derived class constructor can explicitly invoke a base class constructor using the `base` keyword.

**Example**:
```csharp
public class Manager : Employee
{
    public Manager(int salary)
        : base(salary)
    {
        Console.WriteLine("Manager Constructor");
    }
}
```

---

### **Chaining Constructors**
Use the `this` keyword to call another constructor in the same class.

**Example**:
```csharp
public class Employee
{
    public int Salary;

    // Constructor chaining
    public Employee(int annualSalary) => Salary = annualSalary;

    public Employee(int weeklySalary, int weeks)
        : this(weeklySalary * weeks) { }
}
```

---

### **Struct Constructors**
- Struct constructors behave similarly to class constructors.
- When a struct is instantiated with `new`, its constructor is called.
- Structs require a parameterless constructor if field initializers are used.

**Example**:
```csharp
public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}
```

---

### **Best Practices**
1. **Avoid Long Parameter Lists**:
   - Use object initializers or builder patterns for complex initialization.

2. **Minimize Dependency on Static Constructors**:
   - Keep static initialization simple to prevent unexpected behaviors.

3. **Use Private Constructors for Singletons or Utility Classes**:
   - Prevent unnecessary instantiation.

4. **Leverage Constructor Overloading**:
   - Provide multiple ways to initialize an object for flexibility.

5. **Ensure Required Parameters**:
   - Use primary constructors or mandatory arguments to enforce proper initialization.