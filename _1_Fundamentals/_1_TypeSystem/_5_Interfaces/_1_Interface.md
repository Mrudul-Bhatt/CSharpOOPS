### **Introduction to Interfaces in C#**

An **interface** defines a contract for a group of related functionalities that a class or struct must implement. It
specifies **what behaviors a type must have**, but not **how they are implemented**. Interfaces play a key role in
promoting **polymorphism** and **code reusability**.

* * * * *

### **Key Characteristics of Interfaces**

1. **Definition**:
    - Declared with the `interface` keyword.
    - Does not contain implementation for its members (unless default implementations are provided starting from C#
      8.0).
    - Cannot contain fields, instance constructors, or destructors.
2. **Accessibility**:
    - Members are `public` by default.
    - Supports explicit accessibility modifiers for members (e.g., `public`, `private`).
3. **Implementation**:
    - A class or struct implementing an interface must:
        - Define all its members.
        - Match the signature of the interface members.
4. **Multiple Interfaces**:
    - A class can implement multiple interfaces, even though it cannot inherit from multiple classes.
5. **Static Members**:
    - Starting from C# 11, interfaces can declare static abstract members.

* * * * *

### **Defining an Interface**

```
public interface IEquatable<T>
{
    bool Equals(T obj);
}

```

- **Purpose**: Specifies a contract that types implementing this interface must include an `Equals` method.

* * * * *

### **Implementing an Interface**

A class or struct must implement all members of the interface:

```
public class Car : IEquatable<Car>
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Year { get; set; }

    // Implementation of IEquatable<Car>
    public bool Equals(Car? car)
    {
        return (this.Make, this.Model, this.Year) ==
               (car?.Make, car?.Model, car?.Year);
    }
}

```

- **Explanation**:
    - The `Car` class implements the `IEquatable<Car>` interface.
    - It provides an implementation for the `Equals` method that checks equality based on `Make`, `Model`, and `Year`.

* * * * *

### **Features of Interfaces**

### **1\. Explicit Implementation**

If multiple interfaces have members with the same name, explicit implementation ensures no ambiguity.

```
public interface IPrinter
{
    void Print();
}

public interface IScanner
{
    void Print();
}

public class MultiFunctionDevice : IPrinter, IScanner
{
    void IPrinter.Print()
    {
        Console.WriteLine("Printing as a Printer.");
    }

    void IScanner.Print()
    {
        Console.WriteLine("Printing as a Scanner.");
    }
}

```

- **Usage**:

  ```
  var device = new MultiFunctionDevice();
  ((IPrinter)device).Print(); // Output: Printing as a Printer.
  ((IScanner)device).Print(); // Output: Printing as a Scanner.

  ```

### **2\. Default Interface Implementation**

From C# 8.0, interfaces can provide default implementations.

```
public interface ILogger
{
    void Log(string message)
    {
        Console.WriteLine($"Default Logger: {message}");
    }
}

public class CustomLogger : ILogger { }

public class Program
{
    public static void Main()
    {
        ILogger logger = new CustomLogger();
        logger.Log("Hello, World!"); // Output: Default Logger: Hello, World!
    }
}

```

* * * * *

### **Inheriting Interfaces**

An interface can inherit from one or more interfaces:

```
public interface IBase
{
    void BaseMethod();
}

public interface IDerived : IBase
{
    void DerivedMethod();
}

public class Implementation : IDerived
{
    public void BaseMethod()
    {
        Console.WriteLine("Base method implementation.");
    }

    public void DerivedMethod()
    {
        Console.WriteLine("Derived method implementation.");
    }
}

```

* * * * *

### **Working with Structs**

Structs can implement interfaces to simulate inheritance:

```
public interface IShape
{
    double GetArea();
}

public struct Circle : IShape
{
    public double Radius { get; set; }

    public double GetArea()
    {
        return Math.PI * Radius * Radius;
    }
}

```

* * * * *

### **Real-Life Example: Dependency Injection**

Interfaces are often used for dependency injection to achieve loose coupling:

```
public interface INotificationService
{
    void SendNotification(string message);
}

public class EmailNotification : INotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class SmsNotification : INotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}

public class NotificationManager
{
    private readonly INotificationService _service;

    public NotificationManager(INotificationService service)
    {
        _service = service;
    }

    public void Notify(string message)
    {
        _service.SendNotification(message);
    }
}

```

- **Usage**:

  ```
  var emailService = new EmailNotification();
  var manager = new NotificationManager(emailService);
  manager.Notify("Hello via Email!");

  var smsService = new SmsNotification();
  manager = new NotificationManager(smsService);
  manager.Notify("Hello via SMS!");

  ```

* * * * *

### **Summary**

| Feature                     | Description                                                      |
|-----------------------------|------------------------------------------------------------------|
| **Definition**              | Defines a contract for implementing types.                       |
| **Default Implementation**  | Supported since C# 8.0 for shared behavior.                      |
| **Explicit Implementation** | Resolves member ambiguity when implementing multiple interfaces. |
| **Static Members**          | Supported since C# 11 for shared static behavior.                |
| **Multiple Interfaces**     | A class/struct can implement multiple interfaces.                |
| **Usage**                   | Promotes polymorphism, loose coupling, and reusable designs.     |

Interfaces are a foundational feature in C# for creating robust and flexible applications by defining behaviors that
multiple classes or structs can share.