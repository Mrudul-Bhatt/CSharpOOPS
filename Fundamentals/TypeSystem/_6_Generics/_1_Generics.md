### **Introduction to Generics in C#**

Generics in C# introduce the concept of **type parameters**, allowing you to create classes, methods, interfaces, or
structs that can operate on a specified type without knowing it at design time. Generics provide:

- **Type safety**: Prevents runtime type errors.
- **Code reuse**: Reduces redundancy by using a single implementation for multiple types.
- **Performance**: Avoids the overhead of boxing/unboxing and runtime type casting.

* * * * *

### **Key Characteristics of Generics**

1. **Type Parameter**:
    - Declared using angle brackets `<T>` (or multiple types like `<T, U>`).
    - Replaced by specific types during **compile-time**.
2. **Usage**:
    - Frequently used with collections (e.g., `List<T>`, `Dictionary<TKey, TValue>`).
    - Can also create custom generic types or methods for specific use cases.
3. **Namespace**:
    - Generic collections are part of the `System.Collections.Generic` namespace.
4. **Supported Types**:
    - You can define **generic classes**, **methods**, **interfaces**, **structs**, and **records**.

* * * * *

### **Generic Class Example**

```
// A simple generic class.
public class GenericList<T>
{
    private List<T> _items = new();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public void PrintAll()
    {
        foreach (T item in _items)
        {
            Console.WriteLine(item);
        }
    }
}

public class ExampleClass { }

public class Program
{
    static void Main()
    {
        // Generic list of integers
        GenericList<int> intList = new();
        intList.Add(10);
        intList.Add(20);
        intList.PrintAll();

        // Generic list of strings
        GenericList<string> stringList = new();
        stringList.Add("Hello");
        stringList.Add("World");
        stringList.PrintAll();

        // Generic list of custom objects
        GenericList<ExampleClass> customList = new();
        customList.Add(new ExampleClass());
        Console.WriteLine("Custom object added to the list.");
    }
}

```

* * * * *

### **Generic Method Example**

### **Defining a Generic Method**

```
public class Utility
{
    public static void PrintTwice<T>(T value)
    {
        Console.WriteLine(value);
        Console.WriteLine(value);
    }
}

public class Program
{
    public static void Main()
    {
        // Call the generic method with different types
        Utility.PrintTwice<int>(42);
        Utility.PrintTwice<string>("Generics in C#");
        Utility.PrintTwice<DateTime>(DateTime.Now);
    }
}

```

### **Output**

```
42
42
Generics in C#
Generics in C#
11/30/2024 12:00:00 PM
11/30/2024 12:00:00 PM

```

* * * * *

### **Generic Constraints**

You can restrict the types that can be used as type parameters using **constraints**:

1. `where T : struct` -- T must be a value type.
2. `where T : class` -- T must be a reference type.
3. `where T : new()` -- T must have a parameterless constructor.
4. `where T : SomeBaseClass` -- T must inherit from `SomeBaseClass`.
5. `where T : ISomeInterface` -- T must implement `ISomeInterface`.

### **Example**

```
public class GenericUtility<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();
    }
}

public class MyClass { }

public class Program
{
    public static void Main()
    {
        GenericUtility<MyClass> utility = new();
        MyClass instance = utility.CreateInstance();
        Console.WriteLine("Instance of MyClass created.");
    }
}

```

* * * * *

### **Benefits of Generics**

1. **Reusability**:
    - Code works with multiple types without duplication.
2. **Type Safety**:
    - Errors are caught at compile-time instead of runtime.
3. **Performance**:
    - No boxing/unboxing for value types.
    - No need for runtime type casting.

* * * * *

### **Built-in Generic Collections**

| Class                      | Description                                       |
|----------------------------|---------------------------------------------------|
| `List<T>`                  | A dynamically sized list of elements of type `T`. |
| `Dictionary<TKey, TValue>` | A collection of key-value pairs.                  |
| `Queue<T>`                 | A FIFO collection of elements of type `T`.        |
| `Stack<T>`                 | A LIFO collection of elements of type `T`.        |

### **Example with `List<T>`**

```
List<int> numbers = new() { 1, 2, 3, 4, 5 };
numbers.Add(6);
numbers.Remove(3);

foreach (int num in numbers)
{
    Console.WriteLine(num);
}

```

* * * * *

Generics provide a powerful tool for creating reusable, efficient, and type-safe code in C#. By understanding and
utilizing them effectively, you can simplify code maintenance and improve application performance.