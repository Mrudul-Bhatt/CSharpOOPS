### Partial Classes in C#

The **partial** keyword in C# allows a class, struct, or interface to be split into multiple files, making it easier to collaborate, manage, and organize code. All the parts of the type are combined into a single type at compile time.

---

### Key Features and Benefits

1. **Splitting Definitions Across Files**:
   - Allows multiple developers to work on different parts of a type simultaneously.
   - Useful for separating user-written code from auto-generated code (e.g., Windows Forms, source generators).

2. **Combine Attributes**:
   - Attributes applied to partial declarations are combined in the final definition.
   - Example:
     ```csharp
     [Serializable]
     partial class Example { }

     [Obsolete]
     partial class Example { }

     // Equivalent to:
     [Serializable]
     [Obsolete]
     class Example { }
     ```

3. **Nested Partial Types**:
   - A nested type can be declared as `partial`, even if the containing type is not.
   - Example:
     ```csharp
     class Container
     {
         partial class Nested { }
         partial class Nested { void Method() { } }
     }
     ```

4. **Base Types and Interfaces**:
   - If one part declares a base class, all parts must agree.
   - Interfaces declared in different parts are combined:
     ```csharp
     partial class Example : Base, IInterface1 { }
     partial class Example : IInterface2 { }

     // Equivalent to:
     class Example : Base, IInterface1, IInterface2 { }
     ```

5. **Shared Accessibility and Members**:
   - All members of a partial type are accessible across all parts.
   - Example:
     ```csharp
     // File1.cs
     public partial class MyClass
     {
         private int x;
         public MyClass(int value) { x = value; }
     }

     // File2.cs
     public partial class MyClass
     {
         public void DisplayX() => Console.WriteLine(x);
     }

     // Usage
     MyClass obj = new MyClass(10);
     obj.DisplayX(); // Outputs: 10
     ```

---

### Rules for Partial Types

1. **Must Be in the Same Assembly and Module**:
   - All parts of a partial type must be in the same assembly (.dll or .exe).

2. **Shared Name and Type Parameters**:
   - All partial declarations must use the same type name and generic parameters.
   - Generic constraints must also match if specified.

3. **Optional Keywords**:
   - If keywords like `public`, `sealed`, or `abstract` are used in one declaration, they must match across all parts.

4. **Attributes Are Merged**:
   - Attributes are combined from all partial declarations into the final type.

5. **Cannot Be Applied to Delegates or Enums**:
   - Delegates and enumerations cannot use the `partial` modifier.

6. **Members Are Combined**:
   - Members from all partial declarations are combined into the final type.

---

### Examples

#### 1. **Partial Class Across Files**

**File 1: `Coords_Part1.cs`**
```csharp
public partial class Coords
{
    private int x, y;

    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
```

**File 2: `Coords_Part2.cs`**
```csharp
public partial class Coords
{
    public void Display()
    {
        Console.WriteLine($"Coords: {x}, {y}");
    }
}
```

**Usage:**
```csharp
class Program
{
    static void Main()
    {
        Coords point = new Coords(10, 20);
        point.Display(); // Output: Coords: 10, 20
    }
}
```

---

#### 2. **Partial Struct and Interface**

```csharp
partial interface IExample
{
    void Method1();
}

partial interface IExample
{
    void Method2();
}

partial struct MyStruct
{
    public int Value { get; set; }
}

partial struct MyStruct
{
    public void Display() => Console.WriteLine(Value);
}

// Usage
var obj = new MyStruct { Value = 42 };
obj.Display(); // Output: 42
```

---

### Common Scenarios

1. **Separation of Auto-Generated Code**:
   - Visual Studio often generates a partial class for auto-generated code, allowing developers to add custom logic in a separate file.

2. **Collaboration**:
   - Splitting large classes into multiple files enables multiple developers to work on different parts without conflicts.

3. **Maintainability**:
   - Breaking down complex classes into smaller, logical files improves readability and manageability.

---

### Summary

- The `partial` keyword allows splitting the definition of classes, structs, and interfaces across multiple files.
- All parts are combined at compile time to form a single type.
- Partial types enhance collaboration, maintainability, and separation of concerns.
- Follow the rules and restrictions to ensure correct usage, such as keeping declarations in the same assembly and using consistent type parameters.