### **Properties in C#: A Detailed Explanation**

In C#, **properties** provide a flexible way to encapsulate data by offering controlled access to class or struct fields. They appear as public fields to the outside but are implemented with special methods called **accessors**. Properties provide encapsulation, validation, and flexibility while exposing or updating data safely. Here’s a breakdown:

---

### **1. Syntax Overview**
A basic field defines a storage location directly:
```csharp
public class Person
{
    public string? FirstName; // A public field
}
```

A **property** uses accessors (`get` and `set`) to provide controlled access:
```csharp
public class Person
{
    public string? FirstName { get; set; } // Property with both get and set
}
```

---

### **2. Automatically Implemented Properties**
With **automatically implemented properties**, the compiler generates a hidden **backing field** and implements the `get` and `set` methods:
```csharp
public class Person
{
    public string? FirstName { get; set; } = "Unknown"; // Default initialization
}
```
This reduces boilerplate code for simple properties.

---

### **3. Properties with Validation or Custom Logic**
A property can use a **backing field** explicitly to add validation or logic:
```csharp
public class Person
{
    private string? _firstName;

    public string? FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name cannot be empty.");
            _firstName = value;
        }
    }
}
```

---

### **4. Field-Backed Properties (Preview Feature in C# 13)**
The `field` keyword allows access to the compiler-synthesized backing field without declaring it explicitly:
```csharp
public class Person
{
    public string? FirstName
    {
        get;
        set => field = value?.Trim(); // Automatically trims whitespace
    }
}
```

**Caution**: Avoid naming fields as `field` to prevent conflicts.

---

### **5. Required Properties**
Introduced in **C# 11**, the `required` modifier ensures a property is initialized during object construction:
```csharp
public class Person
{
    public required string FirstName { get; init; } // Required during initialization

    public Person(string firstName) => FirstName = firstName;
}
```
Example of usage:
```csharp
var person = new Person { FirstName = "John" }; // Required initializer
```

---

### **6. Read-Only, Write-Only, and Restricted Access**
- **Read-only property**: Only provides a `get` accessor:
  ```csharp
  public string FullName => $"{FirstName} {LastName}";
  ```

- **Write-only property** (rare): Only provides a `set` accessor:
  ```csharp
  private string _password;
  public string Password
  {
      set => _password = value;
  }
  ```

- **Restricted Access**: Accessors can have different access levels:
  ```csharp
  public string? FirstName { get; private set; } // Only readable externally
  ```

---

### **7. Lazy Evaluation with Backing Fields**
A **computed property** can cache results to optimize performance:
```csharp
public class Person
{
    private string? _fullName;
    public string FullName
    {
        get
        {
            if (_fullName == null)
                _fullName = $"{FirstName} {LastName}";
            return _fullName;
        }
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```
If `FirstName` or `LastName` changes, the cached `_fullName` must be invalidated:
```csharp
public string FirstName
{
    get => _firstName;
    set
    {
        _firstName = value;
        _fullName = null; // Invalidate cache
    }
}
```

---

### **8. Expression-Bodied Properties**
For concise one-liner logic:
```csharp
public string Name => $"{FirstName} {LastName}";
```

---

### **9. Properties and Access Levels**
Accessors can have modifiers (`private`, `protected`, etc.), but the property-level access defines the maximum scope:
```csharp
public string? FirstName { get; private set; } // Public property, private set
```

---

### **10. Key Concepts Recap**
- Properties are **smart fields** that provide controlled access to data.
- Use `get`, `set`, and `init` for flexibility.
- Add validation, logic, or lazy evaluation with backing fields.
- Access levels and `required` properties enforce proper initialization and usage.

Would you like an example illustrating all these concepts together?