### **Understanding Attributes in C#**

Attributes in C# provide a mechanism to attach metadata to your code, which can then be retrieved and used at runtime using reflection. They are a declarative way to add descriptive or behavioral information to your program entities like assemblies, classes, methods, properties, and more.

---

### **Key Properties of Attributes**

1. **Metadata Addition**:  
   Attributes embed additional metadata into your program, describing the types and members in detail. This metadata becomes part of the assembly and can be used by tools or during runtime.

2. **Flexible Application**:  
   You can apply attributes to various program elements:
   - **Assemblies**: Add information about the assembly, like version or culture.
   - **Classes and Structs**: Specify characteristics or behaviors.
   - **Methods and Properties**: Define rules or additional processing needs.

3. **Arguments Support**:  
   Attributes can accept parameters similar to methods and properties, allowing customization when applied.

4. **Reflection**:  
   Reflection enables querying the metadata, including attributes, at runtime. This allows dynamic behavior based on the attributes defined in the code.

---

### **Basic Usage of Attributes**

An attribute is applied by placing it in square brackets (`[ ]`) above the program entity. For example:

```csharp
[Obsolete("This method is deprecated. Use NewMethod instead.")]
public void OldMethod()
{
    Console.WriteLine("Old method called.");
}

public void NewMethod()
{
    Console.WriteLine("New method called.");
}
```

Here, the `[Obsolete]` attribute indicates that `OldMethod` should no longer be used, providing a message to developers.

---

### **Reflection with Attributes**

Reflection is a powerful feature that allows you to inspect and interact with metadata, including attributes.

#### **Example: Querying Attributes with Reflection**

```csharp
using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CustomDescriptionAttribute : Attribute
{
    public string Description { get; }

    public CustomDescriptionAttribute(string description)
    {
        Description = description;
    }
}

public class MyClass
{
    [CustomDescription("This is a sample method.")]
    public void MyMethod()
    {
        Console.WriteLine("Executing MyMethod");
    }
}

class Program
{
    static void Main()
    {
        var type = typeof(MyClass);
        var method = type.GetMethod("MyMethod");

        if (method != null)
        {
            var attributes = method.GetCustomAttributes(typeof(CustomDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var customAttr = (CustomDescriptionAttribute)attributes[0];
                Console.WriteLine($"Method Description: {customAttr.Description}");
            }
        }
    }
}
```

**Output**:  
```
Method Description: This is a sample method.
```

#### **Reflection for Type Information**
You can use reflection to examine other metadata, such as type information or assemblies:

```csharp
int number = 42;
Type typeInfo = number.GetType();
Console.WriteLine(typeInfo); // Output: System.Int32
```

---

### **Built-in Attributes in C#**

C# provides several built-in attributes that you can use out of the box:

1. **[Obsolete]**: Marks a program element as outdated.
2. **[Serializable]**: Indicates that a class can be serialized.
3. **[Conditional]**: Specifies that a method should only be included in a build under certain conditions.
4. **[DllImport]**: Used to import unmanaged DLLs.
5. **[AssemblyVersion]**: Specifies the version of an assembly.

---

### **Custom Attributes**

You can create custom attributes by inheriting from the `System.Attribute` class. For example:

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorAttribute : Attribute
{
    public string Name { get; }
    public string Version { get; }

    public AuthorAttribute(string name, string version)
    {
        Name = name;
        Version = version;
    }
}

[Author("John Doe", "1.0")]
public class SampleClass
{
    [Author("Jane Smith", "1.1")]
    public void SampleMethod() { }
}
```

---

### **Important Notes**
- **Accessing Attributes**: Reflection APIs like `GetCustomAttributes` allow retrieving attribute information.
- **AttributeUsage**: Use the `[AttributeUsage]` attribute to specify where your custom attribute can be applied.
- **Intermediate Language (IL)**: Terms like `protected` and `internal` in C# map to `Family` and `Assembly` in IL. Use properties like `IsAssembly` and `IsFamilyOrAssembly` to reflect these.

---

### **Conclusion**

Attributes in C# are a powerful tool for attaching metadata to your program. When combined with reflection, they enable dynamic behavior and extensibility in your applications. By understanding how to use built-in and custom attributes effectively, you can make your code more descriptive, maintainable, and dynamic.