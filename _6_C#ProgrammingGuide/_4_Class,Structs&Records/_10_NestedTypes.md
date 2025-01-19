### Nested Types in C#

A **nested type** is a type defined within another type (class, struct, or interface). This feature can be used to logically group related types, hide implementation details, or scope a type to its containing type.

---

### Key Points about Nested Types

1. **Default Accessibility**:
   - Nested types are **private** by default, meaning they are accessible only within their containing type.
   - Example:
     ```csharp
     public class Container
     {
         class Nested
         {
             // Only accessible within Container.
         }
     }
     ```

2. **Explicit Access Modifiers**:
   - You can specify access modifiers to control the visibility of a nested type.
   - For classes:
     - **Allowed modifiers**: `public`, `protected`, `internal`, `protected internal`, `private`, and `private protected`.
     - Example:
       ```csharp
       public class Container
       {
           public class PublicNested { }
           private class PrivateNested { }
       }
       ```
   - For structs:
     - **Allowed modifiers**: `public`, `internal`, or `private`.

3. **Access Levels in Sealed Classes**:
   - Defining `protected`, `protected internal`, or `private protected` nested types in a sealed class generates compiler warning **CS0628**, as it implies inheritance, which sealed classes do not allow.

4. **Code Quality Rule CA1034**:
   - Making nested types externally visible (e.g., `public`) violates code quality rule **CA1034**. It's generally recommended to define such types outside their containing type unless there's a compelling reason to nest them.

---

### Accessing Containing Type from Nested Type

A nested type can access all members (including `private` and `protected`) of its containing type. To link a nested type to its containing type, you can pass a reference to the containing type through the nested type's constructor.

Example:
```csharp
public class Container
{
    private int containerValue = 42;

    public class Nested
    {
        private Container? parent;

        public Nested(Container parent)
        {
            this.parent = parent;
        }

        public void DisplayContainerValue()
        {
            Console.WriteLine(parent?.containerValue);
        }
    }
}

// Usage
var container = new Container();
var nested = new Container.Nested(container);
nested.DisplayContainerValue(); // Outputs: 42
```

---

### Full Name of Nested Types

The full name of a nested type is `<ContainingType>.<NestedType>`. This name is used to create an instance of the nested type.

Example:
```csharp
Container.Nested nestedInstance = new Container.Nested(container);
```

---

### Use Cases for Nested Types

1. **Logical Grouping**:
   - Use nested types to group types that are used only within their containing type.
   - Example:
     ```csharp
     public class Tree
     {
         public class Node
         {
             public int Value { get; set; }
             public Node? Left { get; set; }
             public Node? Right { get; set; }
         }
     }
     ```

2. **Encapsulation**:
   - Use nested types to hide implementation details.
   - Example:
     ```csharp
     public class Graph
     {
         private class Edge
         {
             public int From { get; set; }
             public int To { get; set; }
         }
     }
     ```

3. **Scoping**:
   - Restrict a type's usage to the context of its containing type.
   - Example:
     ```csharp
     public class Logger
     {
         private class LogEntry
         {
             public string Message { get; set; }
             public DateTime Timestamp { get; set; }
         }
     }
     ```

---

### Summary

- Nested types are defined within other types and default to `private` accessibility.
- Use explicit access modifiers to change their visibility.
- Nested types can access private and protected members of their containing type.
- Avoid making nested types `public` unless there's a compelling reason, as it can violate code quality rules.
- Use nested types for logical grouping, encapsulation, or scoping of related functionality.