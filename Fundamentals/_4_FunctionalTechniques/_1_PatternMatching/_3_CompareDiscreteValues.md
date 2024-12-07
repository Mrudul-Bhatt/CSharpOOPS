### **Compare Discrete Values in C#: Explanation and Example**

Pattern matching in C# allows you to compare a variable against a set of discrete values, such as enumeration values, numeric constants, or strings. This approach is concise and readable, especially when using a **`switch` expression**.

---

### **How It Works**

1.  **`switch` Expression**:
    - Tests a variable against multiple possible values.
    - Returns a result based on the first matching case.
    - Can be used with enums, strings, numeric constants, or other comparable values.
2.  **Discard Pattern (`_`)**:
    - Ensures all possible values are handled.
    - Acts as a catch-all for any unmatched cases.
    - If omitted and there's no match, the program throws a runtime exception.

---

### **Example 1: Using an Enum**

This example dispatches methods based on a value from an enumeration:

```
public enum Operation
{
    SystemTest,
    Start,
    Stop,
    Reset
}

public class SystemController
{
    public string PerformOperation(Operation command) =>
       command switch
       {
           Operation.SystemTest => RunDiagnostics(),
           Operation.Start => StartSystem(),
           Operation.Stop => StopSystem(),
           Operation.Reset => ResetToReady(),
           _ => throw new ArgumentException("Invalid enum value for command", nameof(command)),
       };

    private string RunDiagnostics() => "Diagnostics running...";
    private string StartSystem() => "System started.";
    private string StopSystem() => "System stopped.";
    private string ResetToReady() => "System reset.";
}

// Usage:
var controller = new SystemController();
Console.WriteLine(controller.PerformOperation(Operation.Start)); // Output: System started.
Console.WriteLine(controller.PerformOperation(Operation.Reset)); // Output: System reset.

```

---

### **Example 2: Using String Values**

This version uses strings for commands instead of an enum, suitable for text-based input:

```
public class SystemController
{
    public string PerformOperation(string command) =>
       command switch
       {
           "SystemTest" => RunDiagnostics(),
           "Start" => StartSystem(),
           "Stop" => StopSystem(),
           "Reset" => ResetToReady(),
           _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
       };

    private string RunDiagnostics() => "Diagnostics running...";
    private string StartSystem() => "System started.";
    private string StopSystem() => "System stopped.";
    private string ResetToReady() => "System reset.";
}

// Usage:
var controller = new SystemController();
Console.WriteLine(controller.PerformOperation("Start")); // Output: System started.
Console.WriteLine(controller.PerformOperation("Invalid")); // Throws ArgumentException

```

---

### **Example 3: Using `ReadOnlySpan<char>`**

Starting with C# 11, you can optimize string comparisons by using `ReadOnlySpan<char>` for better performance, especially with large data.

```
public class SystemController
{
    public string PerformOperation(ReadOnlySpan<char> command) =>
       command switch
       {
           "SystemTest" => RunDiagnostics(),
           "Start" => StartSystem(),
           "Stop" => StopSystem(),
           "Reset" => ResetToReady(),
           _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
       };

    private string RunDiagnostics() => "Diagnostics running...";
    private string StartSystem() => "System started.";
    private string StopSystem() => "System stopped.";
    private string ResetToReady() => "System reset.";
}

// Usage:
var controller = new SystemController();
Console.WriteLine(controller.PerformOperation("Stop".AsSpan())); // Output: System stopped.

```

---

### **Key Points**

1.  **Enum-based Approach**:
    - Strongly typed and prevents invalid values at compile time.
    - Use this for well-defined sets of operations.
2.  **String-based Approach**:
    - Flexible but prone to errors due to typos.
    - Use this for text-based input, such as commands from a user interface.
3.  **Span-based Approach**:
    - Optimized for performance when comparing constant string values.
    - Useful for high-performance applications where strings are used frequently.
4.  **Error Handling**:
    - Always include the discard pattern (`_`) to handle unexpected values.
    - Omitting it may lead to runtime exceptions if an input doesn't match any cases.

---

### **Advantages of Comparing Discrete Values**

- **Readability**: The concise syntax of `switch` expressions improves code readability.
- **Type Safety**: Enum-based implementations ensure that only valid values are used.
- **Comprehensive Handling**: The discard pattern (`_`) ensures no cases are missed.
- **Flexibility**: Supports a variety of value types, including enums, strings, and constants.

These techniques are highly versatile, helping you write clean, efficient, and maintainable code.
