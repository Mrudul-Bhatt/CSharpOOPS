### **Asynchronous Exceptions**

Asynchronous methods can throw exceptions just like synchronous methods. However, managing exceptions in asynchronous
code involves understanding how exceptions are propagated, stored, and rethrown in the context of tasks.

---

### **Key Concepts**

1.  **Exception Handling in Asynchronous Code**
    - Exceptions in asynchronous methods are captured and stored in the faulted `Task` object.
    - When an `await` statement encounters a faulted task, the exception is **re-thrown** to mimic synchronous
      error-handling behavior.
2.  **Faulted Tasks**
    - A task is marked as _faulted_ if it cannot complete due to an exception.
    - Faulted tasks store their exceptions in the `Task.Exception` property as a `System.AggregateException`.
3.  **AggregateException**
    - This object can hold multiple exceptions in its `InnerExceptions` collection.
    - In most scenarios, the `InnerExceptions` collection contains only one exception.
4.  **Re-throwing Exceptions**
    - When `await` is used on a faulted task, the first exception in the `AggregateException.InnerExceptions` collection
      is **unwrapped** and re-thrown.
    - This behavior aligns asynchronous exception handling with synchronous patterns, making it easier to work with.

---

### **Example: Simulating an Exception**

Here's an example where the `ToastBreadAsync` method throws an exception:

```
private static async Task<Toast> ToastBreadAsync(int slices)
{
    for (int slice = 0; slice < slices; slice++)
    {
        Console.WriteLine("Putting a slice of bread in the toaster");
    }
    Console.WriteLine("Start toasting...");
    await Task.Delay(2000);

    Console.WriteLine("Fire! Toast is ruined!");
    throw new InvalidOperationException("The toaster is on fire");

    // Unreachable code (demonstrates the fire stops further operations)
    await Task.Delay(1000);
    Console.WriteLine("Remove toast from toaster");
    return new Toast();
}

```

---

### **Behavior of Faulted Tasks**

1.  **Capturing Exceptions**

    - The `InvalidOperationException` thrown by `ToastBreadAsync` is stored in the faulted task's `Task.Exception`
      property.

2.  **Awaiting a Faulted Task**

    - When you `await` the `ToastBreadAsync` task, the exception is **unwrapped and rethrown**.

3.  **Console Output**

    The output might look like this:

    ```
    Pouring coffee
    Coffee is ready
    Putting a slice of bread in the toaster
    Start toasting...
    Fire! Toast is ruined!
    Unhandled exception. System.InvalidOperationException: The toaster is on fire
        at AsyncBreakfast.Program.ToastBreadAsync(Int32 slices) in Program.cs:line 65
        at AsyncBreakfast.Program.MakeToastWithButterAndJamAsync(Int32 number) in Program.cs:line 36
        at AsyncBreakfast.Program.Main(String[] args) in Program.cs:line 24

    ```

---

### **Handling Exceptions**

To handle asynchronous exceptions properly, use `try`-`catch` blocks around the `await` statements or tasks:

```
try
{
    var toast = await ToastBreadAsync(2);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

```

This allows you to gracefully handle failures without terminating the application unexpectedly.

---

### **Unpacking Exceptions**

1.  **When Awaiting Faulted Tasks**
    - If a faulted task is awaited, the first exception in the `AggregateException.InnerExceptions` collection is
      re-thrown.
2.  **When Accessing Task.Exception Directly**

    - If you access the `Task.Exception` property, you get an `AggregateException`. Use its `InnerExceptions` collection
      to inspect all exceptions:

      ```
      if (toastTask.IsFaulted)
      {
          foreach (var ex in toastTask.Exception.InnerExceptions)
          {
              Console.WriteLine($"Exception: {ex.Message}");
          }
      }

      ```

---

### **Best Practices**

1.  **Fail Fast for Argument Validation**

    - Any argument validation exceptions should be thrown synchronously:

      ```
      if (slices < 1)
          throw new ArgumentException("Number of slices must be at least 1.");

      ```

2.  **Graceful Exception Handling**

    - Use `trycatch` blocks around `await` calls to handle known errors.

3.  **AggregateException Awareness**

    - When directly inspecting task results, be prepared to handle `AggregateException` for multiple faults.

4.  **Avoid Ignoring Exceptions**

    - Always handle exceptions, even if it's just for logging purposes.

---

### **Summary**

Asynchronous exceptions behave similarly to synchronous exceptions in many ways but require understanding of faulted
tasks and `AggregateException`. By applying proper exception-handling patterns, you can ensure your asynchronous code is
robust and can handle errors gracefully without disrupting application flow.
