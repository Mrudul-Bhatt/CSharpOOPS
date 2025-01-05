### **Known Pitfalls in Nullable Reference Types**

Nullable reference types introduce new tools to avoid null reference errors at compile time. However, there are some scenarios---like working with **structs** and **arrays**---where the static analysis might fail to detect potential null issues. These situations are known pitfalls because they can result in runtime exceptions even when the code appears safe to the compiler.

* * * * *

### **1\. Structs**

Structs containing reference types can unintentionally initialize non-nullable references to `null` without generating warnings. This occurs because the `default` value of a struct doesn't guarantee proper initialization for its fields.

### **Example: Struct with Non-Nullable References**

```
#nullable enable

public struct Student
{
    public string FirstName;    // Non-nullable
    public string? MiddleName;  // Nullable
    public string LastName;     // Non-nullable
}

public static class Program
{
    public static void PrintStudent(Student student)
    {
        // Dereferencing without null checks
        Console.WriteLine($"First name: {student.FirstName.ToUpper()}");
        Console.WriteLine($"Middle name: {student.MiddleName?.ToUpper()}");
        Console.WriteLine($"Last name: {student.LastName.ToUpper()}");
    }

    public static void Main()
    {
        PrintStudent(default); // No warning, but FirstName and LastName are null
    }
}

```

-   **Issue**: `default(Student)` initializes all fields to their default values. For reference types, the default value is `null`, which violates the non-nullable annotation of `FirstName` and `LastName`.
-   **Result**: A **runtime null reference exception** occurs when `ToUpper()` is called on these fields.

* * * * *

### **Example: Generic Struct**

```
#nullable enable

public struct S<T>
{
    public T Prop { get; set; }
}

public static class Program
{
    public static void Main()
    {
        string s = default(S<string>).Prop; // No warning
        // Runtime exception because Prop is null
    }
}

```

-   **Issue**: The generic property `Prop` is initialized to its default value (`null` for reference types) when the struct is created with `default`. However, the compiler assumes `Prop` adheres to the declared nullability rules.
-   **Result**: A runtime exception occurs when trying to assign `null` to `s`, a non-nullable reference.

* * * * *

### **2\. Arrays**

Arrays of reference types are another common pitfall. When an array is created, all its elements are initialized to their default values. For reference types, this means all elements are `null` even if the array type is declared as non-nullable.

### **Example: Array of Non-Nullable References**

```
#nullable enable

public static class Program
{
    public static void Main()
    {
        string[] values = new string[10]; // Non-nullable string array
        string s = values[0];             // No warning
        Console.WriteLine(s.ToUpper());   // Runtime exception
    }
}

```

-   **Issue**: The array is declared to hold non-nullable `string` elements. However, the array elements are initialized to `null` by default.
-   **Result**: Assigning `values[0]` (which is `null`) to the non-nullable variable `s` does not generate a warning, but dereferencing it (`ToUpper()`) causes a **runtime null reference exception**.

* * * * *

### **Mitigation Strategies**

1.  **Structs**

    -   Explicitly initialize all fields in structs, even when using `default`.
    -   Avoid using structs with reference type fields or enforce initialization using custom constructors.
    -   Use nullable reference types (`string?`) in structs where `null` values might occur.

    **Example Fix:**

    ```
    public struct Student
    {
        public string FirstName { get; init; } = string.Empty;
        public string? MiddleName { get; init; }
        public string LastName { get; init; } = string.Empty;
    }

    ```

2.  **Generic Structs**

    -   Avoid using generic structs with non-nullable reference type constraints when `default` might be used.
    -   Provide constraints or initializers for generic types to handle null safety explicitly.
3.  **Arrays**

    -   Use loops or LINQ to initialize array elements explicitly.
    -   Check array elements for `null` before dereferencing them.
    -   Consider using `ImmutableArray` or other collection types that enforce initialization.

    **Example Fix:**

    ```
    string[] values = new string[10];
    for (int i = 0; i < values.Length; i++)
    {
        values[i] = string.Empty; // Initialize with a non-null value
    }

    string s = values[0];
    Console.WriteLine(s.ToUpper());

    ```

* * * * *

### **Key Takeaways**

-   **Default Values**: Be cautious with `default` initialization for structs and arrays containing reference types, as fields or elements may be `null` unexpectedly.
-   **Static Analysis Limitations**: The compiler's nullable reference type analysis doesn't detect uninitialized fields or null array elements during initialization.
-   **Best Practices**:
    -   Always explicitly initialize non-nullable reference fields or array elements.
    -   Use nullable reference types (`T?`) for fields or array elements that might legitimately hold `null`.
    -   Check values for `null` before accessing or assigning them to non-nullable variables.