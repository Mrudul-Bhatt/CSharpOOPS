namespace CSharpOOPS.Fundamentals.TypeSystem;

/**
 * Key Points in Explanation
 * 1. Strongly Typed Language
 * Meaning: Every variable, constant, and expression in C# must have a defined type. This ensures that the compiler
 * knows how the data is represented in memory and what operations are permissible.
 */
public class KeyPoints
{
    private int a = 5; // Variable of type int
    private string name = "C#"; // Variable of type string
}

/**
 * Method Declarations and Parameter Types
 * Every method in C# specifies:
 *
 * Name: The method’s identifier.
 * Input Parameters: The type and nature of data the method expects.
 * Return Type: What type of data the method returns.
 */
public class MethodDeclarationAndParameters
{
    public int Add(int a, int b) // 'Add' takes two integers and returns an integer.
    {
        return a + b;
    }
}

/**
 * 3. Types in .NET
 * C# relies heavily on the .NET class library, which provides:
 *
 * Built-in Numeric Types: int, double, float, etc.
 *
 * Complex Types: For file systems (File), collections (List), arrays, dates (DateTime), network connections (HttpClient), etc.
 * You can create user-defined types to model specific problem domains.
 */
public class TypesInDotNet
{
}

/**
 * * Information Stored in a Type
 * Every type in C# holds information about:
 * Storage Requirements: Memory size needed for the type (e.g., an int typically takes 4 bytes).
 * Value Range: Minimum and maximum values (e.g., int ranges from -2,147,483,648 to 2,147,483,647).
 * Members: Fields, properties, methods, events, etc., that belong to the type.
 * Example: A DateTime type has methods like AddDays and properties like Day.
 * Base Type: The parent class it inherits from (e.g., every type derives from object in C#).
 * Interfaces Implemented: What interfaces the type supports (e.g., IDisposable).
 * Permitted Operations: What you can do with the type (e.g., addition for int but not for bool).
 */
public class InformationStoredInAType
{
}

/**
 * * Compiler and Type Safety
 * The compiler uses type information to:
 *
 * Ensure that operations are type-safe (e.g., no adding int to bool).
 * Prevent unintended behavior caused by incorrect type usage.
 */
public class CompilerAndTypeSafety
{
    private readonly bool flag = true;
    private readonly int x = 10;

    // private int y = x + flag; // Error: Cannot add int and bool.
}

public class Overview1
{
    public static void Main()
    {
        var number = 10; // 'number' is of type int
        var text = "Hello"; // 'text' is of type string

        // Valid operation
        var result = Add(number, 5);
        Console.WriteLine(result); // Output: 15

        // Invalid operation: Compile-time error
        // int invalid = number + text; // Error: Cannot add int and string.
    }

    public static int Add(int a, int b)
    {
        return a + b; // Adding two integers
    }
}

/**
 * * In C#, you must declare a variable's type or use var for the compiler to infer it.
 *
 * Type Safety:
 *
 * Once declared, a variable's type cannot be changed.
 * Assigning incompatible values is not allowed.
 * Implicit conversions (no data loss) are automatic.
 * Explicit casts are required for potential data loss.
 */
public class SpecifyingTypesInVariableDeclarations
{
    // Explicit declaration:

    // float temperature;
    // string name;
    // MyClass myClass;

    // Declaration with initialization:
    private void fun()
    {
        var firstLetter = 'C';
        var limit = 3; // Inferred as int
        int[] source = { 0, 1, 2, 3, 4, 5 };
        var query = from item in source where item <= limit select item; // Inferred as IEnumerable<int>
    }
}

/**
 * * Built-in Types in C#
 * C# offers a set of built-in types to represent commonly used data types such as:
 *
 * Numeric Types:
 * int, double, float, decimal, byte, long, etc.
 * Text and Characters:
 * char (single character) and string (text data).
 * Boolean Type:
 * bool (true or false).
 * Object Type:
 * object (base type for all other types).
 * Special Types:
 * void (for methods that don’t return a value).
 */
public class BuiltInDataTypes
{
    public static void Main()
    {
        // Numeric types
        var age = 25; // Integer
        var height = 5.9; // Floating-point
        var price = 19.99M; // Precise decimal value for financial calculations

        // Text and character
        var grade = 'A'; // Single character
        var name = "Alice"; // String of text

        // Boolean type
        var isStudent = true; // True/false value

        // Object type
        object anything = 42; // Can hold any type

        // Output
        Console.WriteLine(
            $"Name: {name}, Age: {age}, Grade: {grade}, Height: {height}, Price: {price}, Is Student: {isStudent}, Object: {anything}");
    }
}

/**
 * * Custom Types in C#
 * C# allows you to define custom types using constructs like struct, class, interface, enum, and record. These types
 * enable you to model your application-specific data and behaviors. You can also leverage types from the .NET class
 * library, which offers predefined types for common scenarios.
 *
 * When to Choose a Specific Custom Type
 * Struct: For small, immutable types (up to 64 bytes).
 * Record Struct: For immutable types with value semantics.
 * Class: For types with behavior, inheritance, or polymorphism.
 * Record Class: For data-centric types with value-based equality.
 * Interface: To define a contract that multiple types can implement.
 * Enum: For a fixed set of named constants.
 */
public interface IAnimal
{
    void Speak();
}

public class Dog : IAnimal // Class for behavior and polymorphism
{
    public void Speak()
    {
        Console.WriteLine("Woof!");
    }
}

public record struct Point(int X, int Y); // Immutable data type

public enum Direction // Fixed set of values
{
    North,
    East,
    South,
    West
}

public struct Rectangle // Small, lightweight value type
{
    public int Width { get; set; }
    public int Height { get; set; }
}

internal class CustomDataTypes
{
    public static void Main()
    {
        // Using a class
        IAnimal animal = new Dog();
        animal.Speak(); // Output: Woof!

        // Using a record struct
        var point = new Point(3, 4);
        Console.WriteLine($"Point: ({point.X}, {point.Y})"); // Output: Point: (3, 4)

        // Using an enum
        var dir = Direction.North;
        Console.WriteLine($"Direction: {dir}"); // Output: Direction: North

        // Using a struct
        var rect = new Rectangle { Width = 10, Height = 5 };
        Console.WriteLine($"Rectangle: {rect.Width} x {rect.Height}"); // Output: Rectangle: 10 x 5
    }
}

/**
 * *  Common Type System (CTS) in .NET
 * The Common Type System (CTS) in .NET defines the rules for all types and their relationships. It ensures a consistent
 * programming model across all .NET languages.
 *
 * Key Concepts
 *
 * Unified Inheritance Hierarchy:
 *
 * All types derive from the base type System.Object (C# keyword: object).
 * Types can inherit members (methods, properties, etc.) from a base type, supporting hierarchical design.
 *
 * Two Fundamental Type Categories:
 *
 * Value Types: Directly hold data. Examples: int, double, struct.
 * Created using struct or built-in numeric types.
 * Copied when assigned to a new variable.
 * Reference Types: Store references to data in memory. Examples: class, record class.
 * Created using class or record.
 * Shared reference when assigned to a new variable.
 * Class, Struct, and Record Types:
 *
 * Class: Reference type used for complex behavior and mutable data.
 * Struct: Value type for lightweight, immutable data structures.
 * Record: Can be reference (record class) or value type (record struct); designed for data-centric types with value-based equality.
 * Behavior of Types:
 *
 * Classes: Changes in one variable affect others referring to the same instance.
 * Structs: Changes in one copy do not affect others since each holds its own data.
 * Records: Automatically support value equality.
 */
public class CommonTypeSystem
{
}