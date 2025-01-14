### **Polymorphism in C#**

Polymorphism, meaning "many shapes," is a key concept in object-oriented programming. It allows methods to behave
differently based on the object's actual runtime type, even when accessed through a reference of the base type.

In C#, polymorphism works in two main ways:

1. **Method Overriding with Virtual Methods**: Enables dynamic dispatch to determine which method implementation to
   execute at runtime.
2. **Base Class References for Derived Class Objects**: Allows treating objects of derived classes as objects of their
   base class.

* * * * *

### **How Polymorphism Works**

1. **Base Class Defines Virtual Methods**

   The base class provides a default implementation of a method, marked as `virtual`. Derived classes can override this
   method using the `override` keyword.

2. **Derived Class Provides Specific Implementation**

   Each derived class overrides the virtual method to define its own behavior.

3. **Run-Time Behavior**

   The actual implementation that gets invoked depends on the object's runtime type, not its compile-time type.

* * * * *

### **Example: Drawing Shapes**

Here's how polymorphism works with a drawing application that deals with different shapes (`Rectangle`, `Circle`, and
`Triangle`) but treats them uniformly using a base class `Shape`.

### **Base Class and Derived Classes**

```
using System;
using System.Collections.Generic;

// Base class
public class Shape
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    // Virtual method
    public virtual void Draw()
    {
        Console.WriteLine("Performing base class drawing tasks");
    }
}

// Derived classes
public class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
        base.Draw(); // Calls base class implementation
    }
}

public class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle");
        base.Draw();
    }
}

public class Triangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a triangle");
        base.Draw();
    }
}

```

* * * * *

### **Using Polymorphism**

You can now treat all shapes as `Shape` objects and invoke their `Draw` methods dynamically at runtime:

```
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Polymorphism in action: a collection of Shapes
        var shapes = new List<Shape>
        {
            new Rectangle(), // Runtime type: Rectangle
            new Triangle(),  // Runtime type: Triangle
            new Circle()     // Runtime type: Circle
        };

        // The runtime type determines the method implementation
        foreach (var shape in shapes)
        {
            shape.Draw();
        }
    }
}

```

* * * * *

### **Output**

```
Drawing a rectangle
Performing base class drawing tasks
Drawing a triangle
Performing base class drawing tasks
Drawing a circle
Performing base class drawing tasks

```

* * * * *

### **Key Points**

1. **Virtual Methods and Overrides**
    - Mark methods as `virtual` in the base class to allow overriding.
    - Use `override` in derived classes to provide specific implementations.
2. **Dynamic Dispatch**
    - At runtime, the **actual object's type** determines which method gets called, not the reference type.
3. **Base Class References**
    - A derived object can be assigned to a base class reference. This enables working with groups of related objects in
      a uniform way.

* * * * *

* * * * *

### **Benefits of Polymorphism**

1. **Code Reusability**: Enables writing generic code that works for a wide variety of types.
2. **Extensibility**: Adding new types (e.g., new shapes or payment methods) requires minimal changes to existing code.
3. **Maintainability**: Reduces code duplication by centralizing behavior in the base class.

* * * * *

### **Conclusion**

Polymorphism is a powerful feature that allows a single interface (e.g., a `Draw` method or `Process` method) to
represent multiple types, enhancing code flexibility and scalability.