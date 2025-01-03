### **Void Return Type in Async Methods**

The `void` return type is primarily used in **asynchronous event handlers**, where the method signature requires it. However, its use in other async methods is discouraged due to its limitations, which can lead to difficulties in exception handling and program flow control.

* * * * *

### **Key Characteristics**

1.  **Use Case for Void**:
    -   **Event Handlers**: `void` is mandatory for asynchronous methods used as event handlers since the method signature must match the event delegate.
    -   **Fire-and-Forget**: The method runs asynchronously without requiring the caller to wait for its completion.
2.  **Cannot Be Awaited**:
    -   Async methods with a `void` return type cannot be awaited. The calling method must proceed without waiting for the `void` async method to finish.
3.  **Exception Handling**:
    -   If an exception occurs in a `void` async method, it cannot be caught by the caller.
    -   Unhandled exceptions in `void` async methods propagate to the **SynchronizationContext** or **TaskScheduler**, potentially causing application crashes.
4.  **Alternative**:
    -   For methods that don't need to return a value but are not event handlers, use `Task` as the return type to allow awaiting and proper exception handling.

* * * * *

### **Example: Void in Event Handlers**

The following example illustrates the use of a `void`-returning async method in event handling:

```
public class NaiveButton
{
    public event EventHandler? Clicked;

    public void Click()
    {
        Console.WriteLine("Somebody has clicked a button. Let's raise the event...");
        Clicked?.Invoke(this, EventArgs.Empty);
        Console.WriteLine("All listeners are notified.");
    }
}

public class AsyncVoidExample
{
    static readonly TaskCompletionSource<bool> s_tcs = new TaskCompletionSource<bool>();

    public static async Task MultipleEventHandlersAsync()
    {
        Task<bool> secondHandlerFinished = s_tcs.Task;

        var button = new NaiveButton();

        button.Clicked += OnButtonClicked1;
        button.Clicked += OnButtonClicked2Async;
        button.Clicked += OnButtonClicked3;

        Console.WriteLine("Before button.Click() is called...");
        button.Click();
        Console.WriteLine("After button.Click() is called...");

        await secondHandlerFinished;
    }

    private static void OnButtonClicked1(object? sender, EventArgs e)
    {
        Console.WriteLine("   Handler 1 is starting...");
        Task.Delay(100).Wait();
        Console.WriteLine("   Handler 1 is done.");
    }

    private static async void OnButtonClicked2Async(object? sender, EventArgs e)
    {
        Console.WriteLine("   Handler 2 is starting...");
        Task.Delay(100).Wait();
        Console.WriteLine("   Handler 2 is about to go async...");
        await Task.Delay(500);
        Console.WriteLine("   Handler 2 is done.");
        s_tcs.SetResult(true);
    }

    private static void OnButtonClicked3(object? sender, EventArgs e)
    {
        Console.WriteLine("   Handler 3 is starting...");
        Task.Delay(100).Wait();
        Console.WriteLine("   Handler 3 is done.");
    }
}

```

* * * * *

### **How It Works**

1.  **Event Handlers**:
    -   `OnButtonClicked1` and `OnButtonClicked3` are synchronous.
    -   `OnButtonClicked2Async` is asynchronous and uses `async void`. It performs an asynchronous delay after initial synchronous work.
2.  **Flow of Execution**:
    -   When the `Click` method is called:
        -   All handlers are invoked synchronously in the order they are attached.
        -   After all synchronous handlers are invoked, the program continues execution without waiting for `OnButtonClicked2Async` to complete.
3.  **Completion Notification**:
    -   A `TaskCompletionSource<bool>` (`s_tcs`) is used to signal when `OnButtonClicked2Async` completes, enabling the main thread to wait asynchronously.

* * * * *

### **Output**

```
Before button.Click() is called...
Somebody has clicked a button. Let's raise the event...
   Handler 1 is starting...
   Handler 1 is done.
   Handler 2 is starting...
   Handler 2 is about to go async...
   Handler 3 is starting...
   Handler 3 is done.
All listeners are notified.
After button.Click() is called...
   Handler 2 is done.

```

-   Handlers are invoked sequentially, but `OnButtonClicked2Async` becomes asynchronous and continues independently after notifying the caller.

* * * * *

### **Key Takeaways**

1.  **Avoid `async void`**:
    -   Except for event handlers, always prefer returning `Task` to enable awaiting and proper exception handling.
2.  **Challenges with `async void`**:
    -   Exceptions are uncatchable by the caller.
    -   Makes debugging and control flow management harder.
3.  **Best Practices**:
    -   Use `Task` or `Task<TResult>` for async methods that aren't event handlers.
    -   Use `async void` **only** when the method must conform to an event handler signature.