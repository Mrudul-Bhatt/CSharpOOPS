namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._9_ImplicitAnonymousNullable;

public class IAN
{
    private void ImplicitlyTypedLocalVariables()
    {
        var number = 10; // Compiler infers int
        var name = "John"; // Compiler infers string
        var isActive = true; // Compiler infers bool

        // number = "Hello";  // Error: Type of 'number' is int and cannot be reassigned to a string
        Console.WriteLine(number.GetType()); // Output: System.Int32
    }

    private void AnonymousTypes()
    {
        var person = new { Name = "Alice", Age = 30 };
        Console.WriteLine(person.Name); // Output: Alice
        Console.WriteLine(person.Age); // Output: 30

        // person.Age = 31;  // Error: Anonymous types are immutable
        Console.WriteLine(person.GetType()); // Output: <>f__AnonymousType0`2[System.String,System.Int32]
    }

    private void NullableTypes()
    {
        int? nullableInt = null;

        if (!nullableInt.HasValue) Console.WriteLine("Value is null");

        nullableInt = 42;
        Console.WriteLine(nullableInt.Value); // Output: 42

        // Using the null-coalescing operator
        var result = nullableInt ?? 0; // If nullableInt is null, use 0
        Console.WriteLine(result); // Output: 42
    }
}