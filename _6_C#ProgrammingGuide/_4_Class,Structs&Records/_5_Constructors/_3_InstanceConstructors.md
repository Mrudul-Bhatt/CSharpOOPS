### **Instance Constructors in C#**

An **instance constructor** is a special method used to initialize an instance of a class or struct when it is created using the `new` keyword. Instance constructors allow developers to set up an object with default or specific values and execute any initialization logic required.

---

### **Key Features of Instance Constructors**
1. **Automatic Invocation**:
   - Called automatically during the creation of an object using the `new` keyword.
   - Cannot be called explicitly like other methods.

2. **Same Name as the Class**:
   - An instance constructor has the same name as its class or struct.
   - Does not have a return type.

3. **Supports Overloading**:
   - A class can have multiple constructors with different parameter lists, enabling different ways to initialize objects.

4. **`this` and `base` Keywords**:
   - Use `this` to call another constructor in the same class.
   - Use `base` to call a constructor from the base class.

---

### **Basic Example of Instance Constructors**

```csharp
class Coords
{
    public int X { get; set; }
    public int Y { get; set; }

    // Parameterless constructor
    public Coords()
        : this(0, 0) // Calls the parameterized constructor
    { }

    // Parameterized constructor
    public Coords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

class Example
{
    static void Main()
    {
        var defaultCoords = new Coords(); // Calls parameterless constructor
        Console.WriteLine(defaultCoords); // Output: (0, 0)

        var customCoords = new Coords(5, 3); // Calls parameterized constructor
        Console.WriteLine(customCoords); // Output: (5, 3)
    }
}
```

---

### **Calling Base Class Constructors**

When working with inheritance, you can call the constructor of a base class using the `base` keyword. This ensures proper initialization of the base class before executing the derived class's constructor.

**Example**:
```csharp
abstract class Shape
{
    protected double X, Y;

    public Shape(double x, double y)
    {
        X = x;
        Y = y;
    }

    public abstract double Area();
}

class Circle : Shape
{
    public Circle(double radius)
        : base(radius, 0) // Calls Shape's constructor
    { }

    public override double Area() => Math.PI * X * X;
}
```

Usage:
```csharp
var circle = new Circle(5);
Console.WriteLine(circle.Area()); // Output: 78.54 (area of the circle)
```

---

### **Parameterless Constructors**
If no constructors are explicitly defined, C# provides a default **parameterless constructor** for classes.

**Example**:
```csharp
public class Person
{
    public int Age;
    public string Name = "Unknown";
}

class Example
{
    static void Main()
    {
        var person = new Person(); // Default constructor is used
        Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
        // Output: Name: Unknown, Age: 0
    }
}
```

**Important**:
- If at least one constructor is explicitly defined, C# does not provide a parameterless constructor.

---

### **Primary Constructors (C# 12)**

Primary constructors allow you to declare required parameters directly in the class or struct definition. This feature simplifies object initialization and ensures required parameters are always provided.

**Example**:
```csharp
public class NamedItem(string name)
{
    public string Name => name;
}
```

Usage:
```csharp
var item = new NamedItem("Important");
Console.WriteLine(item.Name); // Output: Important
```

Primary constructors can also work with base constructors, initializer syntax, and additional methods.

**Advanced Example**:
```csharp
public class Widget(string name, int width, int height, int depth) : NamedItem(name)
{
    public int Volume => width * height * depth;

    // Constructor chaining using primary constructor parameters
    public Widget() : this("Default", 1, 1, 1) { }
}
```

---

### **Key Features of Primary Constructors**
1. **Compact Syntax**:
   - Parameters are defined directly in the type declaration.
2. **Mandatory Parameters**:
   - Ensures required parameters are always provided for object creation.
3. **Compiler Optimization**:
   - Parameters are captured as private fields only if necessary.

---

### **Structs and Instance Constructors**
Structs behave similarly to classes but include some differences:
1. Structs always have a **parameterless constructor** that initializes fields to their default values.
2. You can define parameterized constructors for custom initialization.

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

### **Summary of Instance Constructors**
1. Instance constructors are essential for initializing objects and ensuring required fields or properties are set.
2. Overloading, `this`, and `base` provide flexibility and code reuse.
3. Parameterless constructors are implicit unless another constructor is defined.
4. C# 12's primary constructors simplify initialization and enforce mandatory parameters.