### **Accessing Attributes Using Reflection**

Reflection allows you to retrieve metadata added to your code via attributes at runtime. This is essential when using custom attributes to modify or influence program behavior dynamically.

---

### **Key Method: `GetCustomAttributes`**
- The `GetCustomAttributes` method retrieves an array of attributes applied to a class, struct, or other entities.
- Each element in the returned array is an object representing an attribute instance.
- You can then:
  - **Check** the type of the attribute.
  - **Access** its fields and properties.

---

### **Example Explained**

#### **Step 1: Define the Custom Attribute**
The example uses an `AuthorAttribute` to store metadata about the author of a class:
```csharp
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = true)]
public class AuthorAttribute : System.Attribute
{
    private string Name;      // Positional parameter
    public double Version;    // Named parameter

    public AuthorAttribute(string name)
    {
        Name = name;
        Version = 1.0; // Default value
    }

    public string GetName() => Name;
}
```

#### **Step 2: Apply the Attribute**
The attribute is applied to different classes, including single-use and multi-use cases:
```csharp
[Author("P. Ackerman")]
public class FirstClass { }

public class SecondClass { }

[Author("P. Ackerman"), Author("R. Koch", Version = 2.0)]
public class ThirdClass { }
```

#### **Step 3: Access Attributes Using Reflection**
The `PrintAuthorInfo` method uses reflection to:
1. Call `GetCustomAttributes` on the `Type` object of a class.
2. Iterate through the array of attributes.
3. Check for instances of `AuthorAttribute` and access their properties or methods.

```csharp
private static void PrintAuthorInfo(System.Type t)
{
    System.Console.WriteLine($"Author information for {t}");

    // Retrieve all attributes of the class.
    System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);

    // Process each attribute.
    foreach (System.Attribute attr in attrs)
    {
        if (attr is AuthorAttribute a) // Check for AuthorAttribute
        {
            System.Console.WriteLine($"   {a.GetName()}, version {a.Version:f}");
        }
    }
}
```

#### **Step 4: Execute the Reflection Code**
The `Test` method calls `PrintAuthorInfo` for each class:
```csharp
class TestAuthorAttribute
{
    public static void Test()
    {
        PrintAuthorInfo(typeof(FirstClass));
        PrintAuthorInfo(typeof(SecondClass));
        PrintAuthorInfo(typeof(ThirdClass));
    }
}
```

---

### **Output Analysis**

1. **`FirstClass`**
   - Contains one `AuthorAttribute`.
   - Outputs the author's name and version.

2. **`SecondClass`**
   - No attributes are applied.
   - Outputs only the class name.

3. **`ThirdClass`**
   - Contains two `AuthorAttribute` instances (multi-use).
   - Outputs both authors with their respective versions.

```plaintext
Author information for FirstClass
   P. Ackerman, version 1.00
Author information for SecondClass
Author information for ThirdClass
   R. Koch, version 2.00
   P. Ackerman, version 1.00
```

---

### **Conceptual Details**

1. **Attribute Initialization**
   - Attributes aren't instantiated until `GetCustomAttributes` is called.
   - At runtime, `GetCustomAttributes` creates instances of the attributes, initializing them with their constructor and property values.

2. **Reflection Overhead**
   - Reflection is powerful but has a performance cost. Use it judiciously in performance-critical applications.

3. **Multi-Use Attributes**
   - When `AllowMultiple = true` in `AttributeUsage`, the attribute can be applied more than once to the same entity. `GetCustomAttributes` returns all instances in such cases.

---

### **Practical Applications**

- **Metadata Queries**: Identify and display descriptive information at runtime.
- **Behavior Customization**: Alter program behavior dynamically based on attributes.
- **Validation and Enforcement**: Use attributes to enforce rules or constraints on data or methods.

By combining custom attributes with reflection, you can create extensible and dynamic applications.