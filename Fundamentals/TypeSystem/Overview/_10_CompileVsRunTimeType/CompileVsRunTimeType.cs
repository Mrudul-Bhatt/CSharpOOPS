namespace CSharpOOPS.Fundamentals.TypeSystem.Overview._10_CompileVsRunTimeType;

public class CompileVsRunTimeType
{
    /**
     * Case 1: Compile-Time Type = Run-Time Type
     * Here, both compile-time and run-time types are the same:
     */
    private void Case1()
    {
        var message = "Hello, World!"; // Compile-time type: string, Run-time type: string

        Console.WriteLine(message.ToUpper()); // Valid, because both types are `string`
    }

    /**
     * Case 2: Compile-Time Type ≠ Run-Time Type
     * The compile-time type (object) restricts you from directly calling string-specific methods like ToLower() without a cast.
     * At runtime, the instance is recognized as a string.
     */
    private void Case2_1()
    {
        object anotherMessage = "This is a string of characters";
        Console.WriteLine(anotherMessage.GetType()); // Output: System.String
        // anotherMessage.ToLower() // Inaccessible : Error

        // Compile-time type: object
        // Run-time type: string

        // This works because the actual instance is a string:
        var lower = ((string)anotherMessage).ToLower();
        Console.WriteLine(lower); // Output: this is a string of characters
    }

    /**
     * Case 2: Compile-Time Type ≠ Run-Time Type
     * * The compile-time type (IEnumerable
     * <char>
     *     ) only exposes methods defined by the interface.
     *     The run-time type (string) provides the actual implementation of those methods.
     */
    private void Case2_2()
    {
        IEnumerable<char> someCharacters = "abcdefghijklmnopqrstuvwxyz";
        Console.WriteLine(someCharacters.GetType()); // Output: System.String

        // Compile-time type: IEnumerable<char>
        // Run-time type: string

        // Allowed because `string` implements IEnumerable<char>:
        foreach (var c in someCharacters) Console.Write(c + " ");
        // Output: a b c ... z
    }

    /**
     * Method Call Resolution (Compile-Time vs. Run-Time):
     */
    private void Case3()
    {
        //The compiler uses the compile-time type to resolve method calls
        object obj1 = "Hello";
        // obj1.ToUpper(); // Error: 'object' does not contain a definition for 'ToUpper'

        //The runtime type determines behavior for virtual methods:
        object obj2 = "Hello";
        Console.WriteLine(((string)obj2).ToUpper()); // Output: HELLO

        //Type Casting and is Operator:
        // 
        // Compile-time type determines valid casts, while the runtime type ensures correctness

        object obj3 = "Test String";

        if (obj3 is string str) Console.WriteLine(str.ToUpper()); // Output: TEST STRING
    }
}