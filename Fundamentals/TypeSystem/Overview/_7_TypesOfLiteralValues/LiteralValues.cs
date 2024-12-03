namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._7_TypesOfLiteralValues;

public class LiteralValues
{
    /**
     * Implicit Typing of Literals
     */
    private void ImplicitTypingOfLiterals()
    {
        var intLiteral = 42; // Default type: int
        var doubleLiteral = 3.14; // Default type: double
        var charLiteral = 'A'; // Default type: char
        var stringLiteral = "Hello"; // Default type: string
    }

    // private char charLiteral = 'A'; // Default type: char
    // private double doubleLiteral = 3.14; // Default type: double
    // private int intLiteral = 42; // Default type: int
    // private string stringLiteral = "Hello"; // Default type: string

    /**
     * Explicit Typing of Literals : Using Type Suffixes
     */
    private void UsingTypeSuffixex()
    {
        var floatLiteral = 4.56f; // Explicitly typed as float
        var decimalLiteral = 100.5m; // Explicitly typed as decimal
        var longLiteral = 123456789L; // Explicitly typed as long
        var uintLiteral = 42U; // Explicitly typed as unsigned integer
    }

    // float floatLiteral = 4.56f;       // Explicitly typed as float
    // decimal decimalLiteral = 100.5m; // Explicitly typed as decimal
    // long longLiteral = 123456789L;    // Explicitly typed as long
    // uint uintLiteral = 42U;           // Explicitly typed as unsigned integer

    /**
     * Calling Methods on Literals
     */
    private void CallingMethodsOnLiterals()
    {
        var s = "The answer is " + 5;
        // Output: "The answer is 5"
        Console.WriteLine(s);

        var type = 12345.GetType();
        // Output: "System.Int32"
        Console.WriteLine(type);
    }

    /**
     * Operations on Explicit Types
     */
    private void OperationsOnExplicitTypes()
    {
        var a = 5.5f;
        var b = 2.2f;
        var result = a + b; // Allowed because both are float
        Console.WriteLine($"Result: {result}"); // Output: Result: 7.7

        var price = 19.99m;
        var tax = 1.99m;
        var total = price + tax; // Allowed because both are decimal
        Console.WriteLine($"Total: {total}"); // Output: Total: 21.98
    }
}