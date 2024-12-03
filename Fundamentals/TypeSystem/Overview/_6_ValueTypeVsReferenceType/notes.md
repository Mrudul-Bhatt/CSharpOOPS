### Key Concepts

1. Unified Inheritance Hierarchy:
    - All types derive from the base type System.Object (C# keyword: object).
    - Types can inherit members (methods, properties, etc.) from a base type, supporting hierarchical design.
2. Two Fundamental Type Categories:
    - Value Types: Directly hold data. Examples: int, double, struct.
        - Created using struct or built-in numeric types.
        - Copied when assigned to a new variable.
    - Reference Types: Store references to data in memory. Examples: class, record class.
        - Created using class or record.
        - Shared reference when assigned to a new variable.
3. Class, Struct, and Record Types:
    - Class: Reference type used for complex behavior and mutable data.
    - Struct: Value type for lightweight, immutable data structures.
    - Record: Can be reference (record class) or value type (record struct); designed for data-centric types with
      value-based
      equality.
4. Behavior of Types:
    - Classes: Changes in one variable affect others referring to the same instance.
    - Structs: Changes in one copy do not affect others since each holds its own data.
    - Records: Automatically support value equality.

using System;

public struct Point // Value Type

{

public int X { get; set; }

public int Y { get; set; }

}

public class Person // Reference Type

{

public string Name { get; set; }

}

public record PersonRecord(string Name); // Record Reference Type

class Program

{

public static void Main()

{

// Struct Example

Point p1 = new Point { X = 10, Y = 20 };

Point p2 = p1; // Creates a copy

p2.X = 30;

Console.WriteLine($"p1.X: {p1.X}, p2.X: {p2.X}"); // p1.X: 10, p2.X: 30

// Class Example

Person person1 = new Person { Name = "Alice" };

Person person2 = person1; // References the same object

person2.Name = "Bob";

Console.WriteLine($"person1.Name: {person1.Name}"); // person1.Name: Bob

// Record Example

var record1 = new PersonRecord("Alice");

var record2 = record1 with { Name = "Bob" }; // Non-destructive mutation

Console.WriteLine($"record1.Name: {record1.Name}, record2.Name: {record2.Name}"); // record1.Name: Alice, record2.Name:
Bob

}

}