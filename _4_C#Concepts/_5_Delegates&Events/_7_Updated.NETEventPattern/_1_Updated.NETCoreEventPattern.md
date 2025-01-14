### **The Updated .NET Core Event Pattern**

The updated event pattern in .NET Core introduces greater flexibility and modern practices for handling events. Here's a detailed breakdown of the updates:

---

### **1. Removal of the `System.EventArgs` Constraint**
In earlier versions of .NET, event argument types were required to inherit from `System.EventArgs`. This constraint has been relaxed in .NET Core, enabling more flexibility.

#### **Why the Change?**
- **Minimal Benefit from `System.EventArgs`:**  
  The only functionality provided by `System.EventArgs` is the `MemberwiseClone()` method for shallow copying, which uses reflection. This behavior can often be implemented more efficiently and specifically in custom event argument types.
- **Less Restrictive Design:**  
  Removing the constraint allows developers to create more lightweight or specialized argument types, such as structs.

#### **Practical Implications:**
- You can modify classes like `FileFoundArgs` and `SearchDirectoryArgs` to no longer inherit from `System.EventArgs`. The application will continue to function correctly.
- Example modification:

```csharp
internal struct SearchDirectoryArgs
{
    internal string CurrentSearchDirectory { get; }
    internal int TotalDirs { get; }
    internal int CompletedDirs { get; }

    internal SearchDirectoryArgs(string dir, int totalDirs, int completedDirs) : this()
    {
        CurrentSearchDirectory = dir;
        TotalDirs = totalDirs;
        CompletedDirs = completedDirs;
    }
}
```

---

### **2. Using Structs for Event Arguments**
Structs (value types) can now be used as event arguments, providing better performance in scenarios where large numbers of events are raised.

#### **Rules for Using Structs:**
- **Parameterless Constructor Requirement:**  
  When initializing a struct with properties, you must call the parameterless constructor explicitly before assigning values, as shown above.
- **Caution with Reference Semantics:**  
  Do not use structs if the event argument needs to be passed by reference (e.g., for cancellation). In such cases, use a class (reference type) instead. For example:
  - **Struct:** `SearchDirectoryArgs` (no state modification needed by subscribers).
  - **Class:** `FileFoundArgs` (state modification is needed for `CancelRequested`).

---

### **3. Backward Compatibility**
The updated pattern is fully backward-compatible:
- Existing event argument types (`FileFoundArgs`, etc.) that derive from `System.EventArgs` continue to function without modification.
- New event argument types that do not derive from `System.EventArgs` are compatible with the updated pattern and do not break existing codebases.

This ensures a smooth transition for developers working with older .NET libraries.

---

### **4. Handling Async Event Subscribers**
Async methods in event handlers introduce unique challenges due to the signature requirements of the event delegate (`void` return type). This is discouraged for async methods but necessary for event handlers.

#### **The Problem with `async void`**
- Exceptions thrown from `async void` methods are not captured in a `Task` and can lead to:
  - Termination of the process or thread.
  - Indeterminate application states.

#### **Best Practices for Safe Async Event Handlers**
Wrap the `await` statement in a `try-catch` block to handle exceptions gracefully:

```csharp
worker.StartWorking += async (sender, eventArgs) =>
{
    try
    {
        await DoWorkAsync();
    }
    catch (Exception e)
    {
        // Log the error
        Console.WriteLine($"Async task failure: {e}");
        // Consider graceful exit or recovery
    }
};
```

- **Key Points:**
  - Ensure exceptions are caught and logged.
  - Avoid leaving unhandled exceptions that may crash the application.
  - Use this pattern whenever `async void` is unavoidable.

---

### **5. Summary of Changes**
| **Feature**                    | **Previous Pattern**                                  | **Updated Pattern**                      |
| ------------------------------ | ----------------------------------------------------- | ---------------------------------------- |
| **Event Argument Constraint**  | Must inherit from `System.EventArgs`.                 | No inheritance constraint.               |
| **Structs as Event Arguments** | Not allowed.                                          | Supported, with appropriate usage rules. |
| **Compatibility**              | Required inheritance for existing libraries.          | Fully backward-compatible.               |
| **Async Event Handlers**       | Required `async void`, prone to unhandled exceptions. | Requires `try-catch` for safety.         |

---

### **Conclusion**
The updated .NET Core event pattern emphasizes **flexibility, performance, and modern best practices**:
1. Removing the `System.EventArgs` constraint allows for more tailored designs.
2. Structs can be used for lightweight event arguments when reference semantics aren't required.
3. Async event handlers can now be implemented safely with proper exception handling.

Understanding and applying these patterns ensures that your event-driven code aligns with modern .NET standards while maintaining compatibility with legacy systems.