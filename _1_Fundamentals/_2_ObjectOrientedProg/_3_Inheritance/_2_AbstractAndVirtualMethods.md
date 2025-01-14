### **Abstract and Virtual Methods in C#**

Abstract and virtual methods provide flexibility in object-oriented programming, enabling inheritance and polymorphism.
Here's a breakdown of the concepts with examples.

* * * * *

### **1\. Virtual Methods**

- A `virtual` method in a base class provides a default implementation that can be **overridden** in a derived class.
- Use the `override` keyword in the derived class to provide a new implementation.

### **Example: Virtual Methods**

```
using System;

public class Animal
{
    // Virtual method
    public virtual void Speak()
    {
        Console.WriteLine("Animal makes a sound");
    }
}

public class Dog : Animal
{
    // Override the virtual method
    public override void Speak()
    {
        Console.WriteLine("Dog barks");
    }
}

public class Cat : Animal
{
    // Override the virtual method
    public override void Speak()
    {
        Console.WriteLine("Cat meows");
    }
}

class Program
{
    static void Main()
    {
        Animal animal = new Animal();
        Animal dog = new Dog();
        Animal cat = new Cat();

        animal.Speak(); // Output: Animal makes a sound
        dog.Speak();    // Output: Dog barks
        cat.Speak();    // Output: Cat meows
    }
}

```

* * * * *

### **2\. Abstract Methods**

- An `abstract` method in an abstract class defines a **method signature** without implementation.
- Derived classes **must override** and provide the implementation for abstract methods.
- Abstract classes **cannot be instantiated** directly.

### **Example: Abstract Methods**

```
using System;

public abstract class Shape
{
    // Abstract method
    public abstract double GetArea();

    // Non-abstract method
    public void Display()
    {
        Console.WriteLine("This is a shape");
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    // Implement the abstract method
    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    // Implement the abstract method
    public override double GetArea()
    {
        return Width * Height;
    }
}

class Program
{
    static void Main()
    {
        Shape circle = new Circle(5);
        Shape rectangle = new Rectangle(4, 6);

        Console.WriteLine($"Circle Area: {circle.GetArea()}");       // Output: Circle Area: 78.53981633974483
        Console.WriteLine($"Rectangle Area: {rectangle.GetArea()}"); // Output: Rectangle Area: 24
    }
}

```

* * * * *

### **3\. Abstract Classes**

- Abstract classes serve as blueprints for derived classes.
- They can include:
    - Abstract members (must be overridden).
    - Non-abstract members (optional to override).

### **Key Features**

- Cannot be instantiated directly.
- Can include fields, properties, and methods with or without implementation.
- Must be inherited to use its members.

* * * * *

### **4\. Interfaces**

- An interface defines a contract of methods and properties that implementing classes **must fulfill**.
- A class can implement **multiple interfaces**.
- Interfaces can have **default implementations** (from C# 8.0 onward).

### **Example: Interfaces**

```
using System;

// Define an interface
public interface IMovable
{
    void Move();
}

public interface IStoppable
{
    void Stop();
}

// Implement multiple interfaces
public class Car : IMovable, IStoppable
{
    public void Move()
    {
        Console.WriteLine("Car is moving");
    }

    public void Stop()
    {
        Console.WriteLine("Car has stopped");
    }
}

class Program
{
    static void Main()
    {
        Car myCar = new Car();
        myCar.Move();  // Output: Car is moving
        myCar.Stop();  // Output: Car has stopped
    }
}

```

* * * * *

### **5\. Preventing Further Inheritance**

- Use the `sealed` keyword to prevent a class or member from being inherited or overridden.

### **Example: Sealed Class**

```
public sealed class FinalClass
{
    public void Display()
    {
        Console.WriteLine("This class cannot be inherited");
    }
}

// The following would produce a compile error
// public class DerivedClass : FinalClass { }

```

### **Example: Sealed Method**

```
public class BaseClass
{
    public virtual void Display()
    {
        Console.WriteLine("Base class display");
    }
}

public class DerivedClass : BaseClass
{
    public sealed override void Display()
    {
        Console.WriteLine("Derived class display");
    }
}

// The following would produce a compile error
// public class AnotherDerivedClass : DerivedClass
// {
//     public override void Display() { }
// }

```

* * * * *

### **6\. Hiding Base Class Members**

- Use the `new` keyword to hide members of a base class in the derived class.

### **Example: Hiding Members**

```
using System;

public class BaseClass
{
    public void Display()
    {
        Console.WriteLine("Base class display");
    }
}

public class DerivedClass : BaseClass
{
    public new void Display()
    {
        Console.WriteLine("Derived class display");
    }
}

class Program
{
    static void Main()
    {
        BaseClass baseObj = new BaseClass();
        DerivedClass derivedObj = new DerivedClass();
        BaseClass baseRef = new DerivedClass();

        baseObj.Display();    // Output: Base class display
        derivedObj.Display(); // Output: Derived class display
        baseRef.Display();    // Output: Base class display
    }
}

```

* * * * *

### **Key Takeaways**

1. Use `virtual` for methods that can be optionally overridden in derived classes.
2. Use `abstract` for methods that **must** be overridden in derived classes.
3. Abstract classes provide a flexible blueprint with both defined and undefined behaviors.
4. Interfaces allow defining contracts for unrelated classes.
5. Use `sealed` to restrict inheritance.
6. Use `new` to explicitly hide base class members.

These features collectively support polymorphism and extensibility in object-oriented design.