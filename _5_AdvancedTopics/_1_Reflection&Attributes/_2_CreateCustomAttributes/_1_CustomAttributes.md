### **Creating Custom Attributes in C#**

Custom attributes allow you to define your own metadata for program elements (classes, methods, properties, etc.). This can be useful for adding descriptive or behavioral information that can be accessed using reflection at runtime.

---

### **Steps to Create and Use Custom Attributes**

#### **1. Derive from `System.Attribute`**
- Custom attributes are created by defining a class that inherits directly or indirectly from the `System.Attribute` class.

---

#### **2. Define Constructor and Parameters**
- **Positional Parameters**: These are defined through the constructor of the attribute class. They must be supplied when the attribute is used.
- **Named Parameters**: These are public read-write fields or properties. They are optional and can be specified in any order.

---

#### **3. Use `[AttributeUsage]`**
- The `AttributeUsage` attribute specifies:
  - **Valid Targets**: Where the attribute can be applied (e.g., classes, methods, structs).
  - **AllowMultiple**: Whether the attribute can be applied multiple times to the same element.
  - **Inherited**: Whether the attribute is inherited by derived classes.

---

### **Example: Custom Attribute with Positional and Named Parameters**

#### **Defining the Custom Attribute**
```csharp
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public class AuthorAttribute : System.Attribute
{
    private string Name; // Positional parameter
    public double Version { get; set; } // Named parameter

    // Constructor for positional parameter
    public AuthorAttribute(string name)
    {
        Name = name;
        Version = 1.0; // Default value for named parameter
    }

    // Method to retrieve the name
    public string GetName() => Name;
}
```

#### **Using the Custom Attribute**
```csharp
[Author("P. Ackerman", Version = 1.1)]
class SampleClass
{
    // Code written by P. Ackerman
}
```

---

### **Example: Multiuse Custom Attribute**

#### **Defining Multiuse Attribute**
```csharp
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = true)]
public class AuthorAttribute : System.Attribute
{
    private string Name;
    public double Version { get; set; }

    public AuthorAttribute(string name)
    {
        Name = name;
        Version = 1.0;
    }

    public string GetName() => Name;
}
```

#### **Using Multiuse Attribute**
```csharp
[Author("P. Ackerman"), Author("R. Koch", Version = 2.0)]
public class ThirdClass
{
    // Code written by multiple authors
}
```

---

### **Key Concepts**

1. **Positional Parameters**
   - Defined in the attribute class constructor.
   - Must be provided when the attribute is applied.

2. **Named Parameters**
   - Public fields or properties.
   - Optional and can be specified in any order.

3. **`AttributeUsage` Attribute**
   - Controls where the custom attribute can be applied and whether it can be used multiple times.

4. **Reflection**
   - Custom attributes are accessed at runtime using reflection.
   - Example:
     ```csharp
     var attributes = typeof(ThirdClass).GetCustomAttributes(typeof(AuthorAttribute), false);
     foreach (AuthorAttribute attribute in attributes)
     {
         Console.WriteLine($"Author: {attribute.GetName()}, Version: {attribute.Version}");
     }
     ```

---

### **Benefits of Custom Attributes**

- **Flexibility**: Custom attributes provide a way to extend the metadata of your program in a reusable way.
- **Behavioral Control**: Used with reflection, custom attributes can influence runtime behavior.
- **Code Organization**: Adds descriptive information that is helpful for documentation or runtime processing.

By leveraging custom attributes, developers can create a more extensible and descriptive codebase.