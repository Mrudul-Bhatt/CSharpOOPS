### **Practices for Delegate-Based Logging Systems**

This example expands the logging system, demonstrating several best practices when designing with delegates. These practices ensure flexibility, extensibility, and robust functionality in loosely coupled systems.

---

### **1. Using Core Framework Delegate Types**

- Using standard delegate types like `Action<T>` or `Func<T, TResult>` makes your library easier to use and avoids the need for custom delegate definitions.
- Developers can focus on creating methods that match the delegate's signature without needing to learn new delegate types.

---

### **2. Adding Structure with `Severity`**

To make log messages more meaningful, we add:
- **Severity Levels**: Enum defining log message importance.
- **Components**: Identifies the source of the log message.
- **Timestamps**: Adds a structured format for logs.

#### **Updated Logger with Severity and Filtering**
```csharp
public enum Severity
{
    Verbose,
    Trace,
    Information,
    Warning,
    Error,
    Critical
}

public static class Logger
{
    public static Action<string>? WriteMessage;

    public static Severity LogLevel { get; set; } = Severity.Warning;

    public static void LogMessage(Severity s, string component, string msg)
    {
        if (s < LogLevel)
            return;

        var outputMsg = $"{DateTime.Now}\t{s}\t{component}\t{msg}";
        WriteMessage?.Invoke(outputMsg);
    }
}
```

- **LogLevel**: Filters out messages below a certain severity.
- **Formatted Output**: Combines severity, component, and message into a single structured string.

---

### **3. Adding a File Output Logger**

File logging requires additional management, such as ensuring files are correctly opened and closed after each operation. The `FileLogger` class implements this functionality.

#### **File Logger**
```csharp
public class FileLogger
{
    private readonly string logPath;

    public FileLogger(string path)
    {
        logPath = path;
        Logger.WriteMessage += LogMessage;
    }

    public void DetachLog() => Logger.WriteMessage -= LogMessage;

    private void LogMessage(string msg)
    {
        try
        {
            using (var log = File.AppendText(logPath))
            {
                log.WriteLine(msg);
                log.Flush();
            }
        }
        catch (Exception)
        {
            // Catch but do not rethrow to avoid breaking logging
        }
    }
}
```

- **Attach and Detach**: Dynamically attach or remove file logging from the logger.
- **Exception Handling**: Ensures file operations don't propagate exceptions, maintaining system stability.
- **Resource Management**: Opens and closes the file for each operation to flush data and avoid resource leaks.

---

### **4. Combining Output Destinations**

The `Logger` class supports multiple output destinations by leveraging **multicast delegates**.

#### **Example: Console and File Logging**
```csharp
var fileLogger = new FileLogger("log.txt");
Logger.WriteMessage += LoggingMethods.LogToConsole;

Logger.LogMessage(Severity.Information, "App", "Application started.");
Logger.LogMessage(Severity.Error, "App", "An error occurred.");

// Remove console logging
Logger.WriteMessage -= LoggingMethods.LogToConsole;

Logger.LogMessage(Severity.Critical, "App", "Critical issue logged to file only.");
```

- **Multicast Delegates**: Allow logging to the console and a file simultaneously.
- **Dynamic Changes**: Output destinations can be modified at runtime.

---

### **5. Handling Null Delegates**

The logger should gracefully handle cases where no delegate is attached to `WriteMessage`. Using the **null conditional operator** ensures this:

#### **Robust Logging Method**
```csharp
public static void LogMessage(string msg)
{
    WriteMessage?.Invoke(msg);
}
```

- **Short-Circuiting**: If `WriteMessage` is `null`, no attempt is made to invoke the delegate.
- **Type-Safe Invocation**: The compiler generates the correct `Invoke` method for the delegate.

---

### **6. Best Practices for Delegate-Based Design**

#### **Minimal Interfaces**
- Delegate-based designs avoid rigid base classes or interfaces.
- To add a new logging mechanism, you only need a single method matching the delegate's signature.

#### **Loose Coupling**
- The `Logger` class is decoupled from output mechanisms. 
- Adding or modifying output mechanisms (e.g., file, console, database) doesn't require changes to the logger.

#### **Robustness**
- Logging methods should be designed to avoid throwing exceptions. 
- Multicast delegates can stop invocation if an exception is thrown by one of the delegates, so each output method must handle its own errors gracefully.

#### **Extensibility**
- Delegates simplify adding new features (e.g., structured messages, filtering).
- New output engines (e.g., network logging) can be plugged in with minimal effort.

---

### **7. Final Example: Complete Logging System**

Here’s a comprehensive example:

```csharp
using System;
using System.IO;

public enum Severity
{
    Verbose,
    Trace,
    Information,
    Warning,
    Error,
    Critical
}

public static class Logger
{
    public static Action<string>? WriteMessage;
    public static Severity LogLevel { get; set; } = Severity.Warning;

    public static void LogMessage(Severity s, string component, string msg)
    {
        if (s < LogLevel)
            return;

        var outputMsg = $"{DateTime.Now}\t{s}\t{component}\t{msg}";
        WriteMessage?.Invoke(outputMsg);
    }
}

public static class LoggingMethods
{
    public static void LogToConsole(string message)
    {
        Console.Error.WriteLine($"Console: {message}");
    }
}

public class FileLogger
{
    private readonly string logPath;

    public FileLogger(string path)
    {
        logPath = path;
        Logger.WriteMessage += LogMessage;
    }

    public void DetachLog() => Logger.WriteMessage -= LogMessage;

    private void LogMessage(string msg)
    {
        try
        {
            using (var log = File.AppendText(logPath))
            {
                log.WriteLine(msg);
                log.Flush();
            }
        }
        catch
        {
            // Swallow exceptions to avoid breaking the system
        }
    }
}

class Program
{
    static void Main()
    {
        var fileLogger = new FileLogger("log.txt");
        Logger.WriteMessage += LoggingMethods.LogToConsole;

        Logger.LogMessage(Severity.Information, "App", "Application started.");
        Logger.LogMessage(Severity.Error, "App", "An error occurred.");

        Logger.WriteMessage -= LoggingMethods.LogToConsole;

        Logger.LogMessage(Severity.Critical, "App", "Critical issue logged to file only.");
    }
}
```

---

### **8. Summary**

This example demonstrates:
1. **Flexibility**: Adding and removing logging mechanisms dynamically.
2. **Extensibility**: Easily supporting new logging destinations without modifying the core system.
3. **Robustness**: Handling edge cases like null delegates and exceptions in output methods.

These practices make delegate-based designs powerful tools for building loosely coupled and scalable software systems.