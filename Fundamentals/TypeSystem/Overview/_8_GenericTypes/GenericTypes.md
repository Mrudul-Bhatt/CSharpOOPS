### **Generic Types in C#**

Generic types are types declared with type parameters, which act as placeholders for concrete types. These parameters
allow you to create reusable, type-safe data structures and methods. Generics are widely used in C# for collections,
algorithms, and utility classes.

* * * * *

### **Key Points**

1. **Type Parameters**:

    - Generic types use type parameters (e.g., `<T>`) as placeholders for concrete types.
    - The actual type is specified when an instance of the type is created.
2. **Strongly Typed Collections**:

    - Generics enforce type safety at compile time, preventing runtime errors caused by type mismatches.
3. **Advantages**:

    - **Code Reusability**: A single generic type can work with multiple types.
    - **Type Safety**: Errors like adding incorrect types to a collection are caught at compile time.
    - **Performance**: Generics avoid the need for boxing/unboxing (conversion between value types and `object`).

### **Common Generic Types in .NET**

- **Collections**:
- `List<T>`: A strongly typed list.
- `Dictionary<TKey, TValue>`: A key-value pair collection.
- `Queue<T>`: A first-in, first-out collection.
- `Stack<T>`: A last-in, first-out collection.
- **Other Examples**:
- `Nullable<T>`: A value type that can also be `null`.
- `Task<T>`: Used in asynchronous programming.

* * * * *

### **Summary**

Generics allow you to create reusable, type-safe, and efficient types and methods. By using type parameters, you can
write code that works for any data type while maintaining compile-time type checking.