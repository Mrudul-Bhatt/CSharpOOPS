### **Defining and Reading Custom Attributes in C#**

Attributes in C# provide a declarative way to associate metadata with code elements. They don't execute code themselves but serve as metadata that tools, frameworks, or your application can use at runtime, usually accessed through **reflection**.

---

### **Creating Attributes**

#### **Definition**
Attributes are classes that inherit from the `System.Attribute` base class. For example:
```csharp
public class MySpecialAttribute : Attribute
{
}
```
- The attribute name can be used with or without the `Attribute` suffix: `[MySpecial]` or `[MySpecialAttribute]`.

---

#### **Usage**
Attributes can be applied to various targets such as classes, methods, properties, etc. Example:
```csharp
[MySpecial]
public class SomeClass
{
}
```
This marks `SomeClass` with the `MySpecial` attribute, associating metadata with the class.

---

### **Parameters in Attributes**

Attributes can have parameters, which are passed to their constructors.

1. **Positional Parameters**  
   These are defined in the attribute's constructor and must be provided in the exact order:
   ```csharp
   public class MyAttribute : Attribute
   {
       public MyAttribute(string message) { }
   }

   [MyAttribute("This is a message")]
   public class AnotherClass { }
   ```

2. **Named Parameters**  
   These are optional public fields or properties:
   ```csharp
   public class MyAttribute : Attribute
   {
       public string Name { get; set; }
       public int Version { get; set; }
   }

   [MyAttribute(Name = "Test", Version = 1)]
   public class YetAnotherClass { }
   ```

3. **Limitations**  
   Only certain types are valid for attribute parameters:  
   - Primitive types (`int`, `bool`, etc.), `string`, `Type`, enums, and arrays of these types.
   - Custom classes like `Foo` cannot be used:
     ```csharp
     public class InvalidAttribute : Attribute
     {
         public InvalidAttribute(Foo myClass) { } // Error: 'Foo' is not a valid type.
     }
     ```

---

### **Restricting Attribute Usage**

You can restrict where an attribute can be applied using the `[AttributeUsage]` attribute:
```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class MyRestrictedAttribute : Attribute
{
}
```
- `AttributeTargets` restricts usage to specific elements like `Class`, `Method`, `Property`, etc.
- If applied incorrectly, the compiler throws an error:
  ```csharp
  [MyRestrictedAttribute]
  public int MyProperty { get; set; } // Error
  ```

---

### **Reading Attributes via Reflection**

Reflection allows you to inspect metadata, including attributes. Here's how you can retrieve attributes at runtime:

1. **Basic Example**
   ```csharp
   [Obsolete("This class is deprecated.")]
   public class OldClass { }

   var typeInfo = typeof(OldClass).GetTypeInfo();
   var attributes = typeInfo.GetCustomAttributes();
   foreach (var attr in attributes)
   {
       Console.WriteLine($"Attribute: {attr.GetType().Name}");
   }
   ```
   **Output**:
   ```
   Attribute: ObsoleteAttribute
   ```

2. **Getting Specific Attribute**
   You can retrieve a specific attribute type:
   ```csharp
   if (typeInfo.GetCustomAttribute<ObsoleteAttribute>() is ObsoleteAttribute obsoleteAttr)
   {
       Console.WriteLine($"Message: {obsoleteAttr.Message}");
   }
   ```

3. **Attribute Instantiation**
   Attributes are **lazily instantiated**, meaning they are created only when accessed via `GetCustomAttribute` or `GetCustomAttributes`.

---

### **Common .NET Attributes**

1. **`[Obsolete]`**
   Marks code as obsolete and triggers compiler warnings or errors:
   ```csharp
   [Obsolete("Use NewClass instead.")]
   public class OldClass { }
   ```

2. **`[Conditional]`**
   In the `System.Diagnostics` namespace, this allows conditional compilation:
   ```csharp
   [Conditional("DEBUG")]
   public void DebugOnlyMethod() { }
   ```

3. **`[CallerMemberName]`**
   Automatically captures the name of the calling member:
   ```csharp
   public void Log([CallerMemberName] string methodName = "")
   {
       Console.WriteLine($"Called from: {methodName}");
   }
   ```

---

### **Creating a Complete Example**

#### **Define Custom Attribute**
```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MyCustomAttribute : Attribute
{
    public string Description { get; }
    public MyCustomAttribute(string description)
    {
        Description = description;
    }
}
```

#### **Apply Custom Attribute**
```csharp
[MyCustomAttribute("This is a test class.")]
public class TestClass
{
    [MyCustomAttribute("This is a test method.")]
    public void TestMethod() { }
}
```

#### **Read Attribute**
```csharp
var type = typeof(TestClass);
foreach (var attr in type.GetCustomAttributes<MyCustomAttribute>())
{
    Console.WriteLine($"Class Attribute: {attr.Description}");
}

var method = type.GetMethod("TestMethod");
foreach (var attr in method.GetCustomAttributes<MyCustomAttribute>())
{
    Console.WriteLine($"Method Attribute: {attr.Description}");
}
```

**Output**:
```
Class Attribute: This is a test class.
Method Attribute: This is a test method.
```

---

### **Conclusion**

Attributes in C# allow you to add metadata to your code, which can be leveraged at runtime via reflection. They are used extensively in frameworks and libraries for declarative programming. Understanding how to define, apply, and read attributes is essential for advanced C# development.