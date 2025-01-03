### Explanation of Canceling Async Tasks After a Period of Time

This example demonstrates how to schedule the cancellation of asynchronous tasks after a specified period using the `CancellationTokenSource.CancelAfter` method. It extends the "Cancel a list of tasks" application by adding time-based cancellation functionality.

* * * * *

### **Key Concepts**

1.  **`CancellationTokenSource.CancelAfter`**:
    -   This method schedules the cancellation signal to occur after a specified number of milliseconds. Tasks associated with the `CancellationToken` will honor the cancellation request if they respect the token.
2.  **Handling `OperationCanceledException`**:
    -   If the cancellation is triggered before the tasks are completed, the tasks throw an `OperationCanceledException`. This exception is caught in the `try-catch` block to handle the timeout gracefully.
3.  **Proper Cleanup with `finally`**:
    -   The `finally` block ensures that the `CancellationTokenSource` is disposed of, releasing any underlying resources.

* * * * *

### **Main Method Workflow**

1.  **Application Initialization**:
    -   The application prints an introductory message.
2.  **Scheduling Cancellation**:
    -   The `s_cts.CancelAfter(3500)` schedules a cancellation to be triggered after 3500 milliseconds (3.5 seconds).
3.  **Task Execution**:
    -   The application calls and awaits the `SumPageSizesAsync` method to process URLs.
4.  **Handling Timeouts**:
    -   If the tasks are not completed within 3.5 seconds, the cancellation is triggered. This results in an `OperationCanceledException`, which is caught and handled by printing a cancellation message.
5.  **Cleanup**:
    -   The `finally` block disposes of the `CancellationTokenSource` to ensure resource cleanup.
6.  **Ending the Application**:
    -   The application ends gracefully.

* * * * *

### **Changes to the `Main` Method**

The following block schedules a cancellation:

```
s_cts.CancelAfter(3500);

```

If the `SumPageSizesAsync` method is still running after 3.5 seconds, the `s_cts.Cancel()` is called automatically, and tasks respecting the cancellation token will stop execution.

The `try-catch` block handles cancellation:

```
try
{
    s_cts.CancelAfter(3500);

    await SumPageSizesAsync();
}
catch (OperationCanceledException)
{
    Console.WriteLine("\\nTasks cancelled: timed out.\\n");
}
finally
{
    s_cts.Dispose();
}

```

* * * * *

### **Output Example**

If tasks are completed before the cancellation:

```
Application started.

<https://learn.microsoft.com>                                       37,357
<https://learn.microsoft.com/aspnet/core>                           85,589
<https://learn.microsoft.com/azure>                                398,939

Total bytes returned:  521,885
Elapsed time:          00:00:03.200

Application ending.

```

If tasks are canceled:

```
Application started.

<https://learn.microsoft.com>                                       37,357
<https://learn.microsoft.com/aspnet/core>                           85,589

Tasks cancelled: timed out.

Application ending.

```

* * * * *

### **Why Use Time-Based Cancellation?**

1.  **Resource Management**:
    -   Avoid unnecessary resource usage for long-running tasks.
2.  **Responsiveness**:
    -   Ensure the application remains responsive by timing out stalled or slow operations.
3.  **User Experience**:
    -   Prevent users from waiting indefinitely for operations to complete.

This technique is particularly useful in scenarios like downloading large files, performing database queries, or processing data where a reasonable timeout improves performance and reliability.