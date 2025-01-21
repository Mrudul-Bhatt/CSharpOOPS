### **Generic Classes in C#**

Generic classes allow developers to create reusable code that can operate on a variety of data types while maintaining type safety. This approach is especially useful in scenarios where operations remain consistent regardless of the underlying data type, such as with collections or algorithms.

---

### **Key Features and Concepts**

1. **Definition of a Generic Class**:
   - A generic class uses **type parameters** to specify the type(s) of data it works with. Type parameters are placeholders for actual types provided when the class is instantiated.
   - Example:
     ```csharp
     public class GenericClass<T>
     {
         private T _value;

         public void SetValue(T value) => _value = value;
         public T GetValue() => _value;
     }
     ```

2. **Applications**:
   - Commonly used in collections like lists, dictionaries, and queues (`List<T>`, `Dictionary<TKey, TValue>`).
   - Useful for encapsulating algorithms or data structures that work with multiple data types.

---

### **Design Considerations**

1. **Generalization**:
   - Deciding which types to parameterize:
     - Parameterizing too few types may limit flexibility.
     - Over-generalizing can make the code harder to read and maintain.

2. **Constraints on Type Parameters**:
   - Constraints define rules for the types that can be used with the generic class.
   - Examples of constraints:
     - `where T : class` – T must be a reference type.
     - `where T : struct` – T must be a value type.
     - `where T : new()` – T must have a parameterless constructor.
     - `where T : BaseClass` – T must inherit from a specific base class.
   - Example:
     ```csharp
     public class GenericClassWithConstraints<T> where T : new()
     {
         public T CreateInstance() => new T();
     }
     ```

3. **Inheritance in Generic Classes**:
   - Generic classes can inherit from:
     - Concrete (non-generic) classes.
     - Closed constructed types (generic classes with specific type arguments).
     - Open constructed types (generic classes with unspecified type parameters).

   - Example:
     ```csharp
     class BaseNode { }
     class BaseNodeGeneric<T> { }

     // Inheriting from a concrete base class
     class NodeConcrete<T> : BaseNode { }

     // Inheriting from a closed constructed type
     class NodeClosed<T> : BaseNodeGeneric<int> { }

     // Inheriting from an open constructed type
     class NodeOpen<T> : BaseNodeGeneric<T> { }
     ```

4. **Constraints on Inheritance**:
   - Constraints on type parameters in derived classes must match or extend those in the base class.
   - Example:
     ```csharp
     class NodeItem<T> where T : IComparable<T>, new() { }
     class SpecialNodeItem<T> : NodeItem<T> where T : IComparable<T>, new() { }
     ```

---

### **Advanced Concepts**

1. **Multiple Type Parameters and Constraints**:
   - A generic class can have multiple type parameters, each with its own constraints.
   - Example:
     ```csharp
     class MultiTypeGeneric<K, V, U>
         where U : IComparable<U>
         where V : new()
     {
         public K Key { get; set; }
         public V Value { get; set; }
         public U Metadata { get; set; }
     }
     ```

2. **Open and Closed Constructed Types**:
   - **Open Constructed Type**: The type parameter remains unspecified (e.g., `List<T>`).
   - **Closed Constructed Type**: The type parameter is specified (e.g., `List<int>`).

   - Example:
     ```csharp
     void Swap<T>(List<T> list1, List<T> list2) { }
     void Swap(List<int> list1, List<int> list2) { }
     ```

3. **Generic Interfaces**:
   - If a generic class implements a generic interface, all instances of the class can be cast to the interface.
   - Example:
     ```csharp
     public interface IMyInterface<T>
     {
         T GetData();
     }

     public class MyClass<T> : IMyInterface<T>
     {
         private T _data;

         public MyClass(T data) => _data = data;

         public T GetData() => _data;
     }
     ```

4. **Invariance in Generic Classes**:
   - Generic classes are **invariant**, meaning you cannot substitute a `List<BaseClass>` with a `List<DerivedClass>`, even if `DerivedClass` inherits from `BaseClass`.
   - Example:
     ```csharp
     List<BaseClass> baseList = new List<BaseClass>();
     List<DerivedClass> derivedList = new List<DerivedClass>();

     // Invalid:
     // baseList = derivedList; // Compile-time error
     ```

---

### **Practical Example**

Here’s a complete example of a generic class with multiple type parameters, constraints, and inheritance:

```csharp
public class Pair<K, V>
    where K : IComparable<K>
    where V : new()
{
    public K Key { get; set; }
    public V Value { get; set; }

    public Pair(K key)
    {
        Key = key;
        Value = new V();
    }

    public void Display()
    {
        Console.WriteLine($"Key: {Key}, Value: {Value}");
    }
}

class Program
{
    static void Main()
    {
        var pair = new Pair<int, StringBuilder>(42);
        pair.Value.Append("Hello, Generics!");
        pair.Display(); // Output: Key: 42, Value: Hello, Generics!
    }
}
```

---

### **Conclusion**

Generic classes in C# provide powerful tools for creating reusable, type-safe, and flexible code. By leveraging type parameters, constraints, and inheritance, you can design classes and algorithms that adapt to various data types while maintaining robust compile-time checks. Understanding the balance between generalization and usability, as well as mastering advanced concepts like invariance and open/closed constructed types, allows for the creation of highly efficient and maintainable code.