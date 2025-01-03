### **Cancel a List of Tasks**

This example demonstrates how to cancel a list of tasks in a .NET console application using asynchronous programming with a cancellation mechanism. It includes downloading web content from multiple URLs and enabling user-triggered cancellation via the Enter key.

* * * * *

### **Key Concepts**

1.  **Asynchronous Task Cancellation**:
    -   Uses `CancellationTokenSource` to signal cancellation.
    -   Each task monitors the cancellation token to stop ongoing operations when requested.
2.  **Task Management**:
    -   Tracks multiple tasks, such as downloading content or waiting for user input, using `Task.WhenAny` to determine which task completes first.
3.  **User Interaction**:
    -   Detects when the user presses the Enter key to trigger the cancellation process.
4.  **HttpClient**:
    -   Facilitates asynchronous downloading of web content.

* * * * *

### **Step-by-Step Explanation**

### **1\. Application Setup**

-   **Fields**:
    -   `s_cts`: The `CancellationTokenSource` provides a `CancellationToken` that tasks can observe to check for cancellation requests.
    -   `s_client`: Configures an `HttpClient` to handle HTTP requests.
    -   `s_urlList`: A collection of URLs to download.

### **2\. Main Method**

This is the entry point for the application.

-   Starts two tasks:
    1.  **Cancel Task**:
        -   Monitors for the Enter key press.
        -   Signals cancellation using `s_cts.Cancel()`.
    2.  **SumPageSizesAsync Task**:
        -   Iterates over the URLs and downloads their content.
-   Uses `Task.WhenAny` to wait for the first task to complete (either cancellation or all downloads).
-   If the `CancelTask` completes first:
    -   Awaits `sumPageSizesTask` to ensure proper handling of cancellations.
    -   Catches `OperationCanceledException` to indicate the downloads were stopped.

### **3\. SumPageSizesAsync Method**

-   Iterates over the `s_urlList`, calling `ProcessUrlAsync` for each URL.
-   Passes the `CancellationToken` to each task for responsive cancellation.
-   Tracks the total size of downloaded content and measures elapsed time with a `Stopwatch`.

### **4\. ProcessUrlAsync Method**

-   Downloads the content of a single URL as a byte array.
-   Monitors the `CancellationToken` to handle cancellation requests.
-   Returns the content size.

* * * * *

### **Cancellation Workflow**

1.  **CancellationTokenSource**:
    -   Centralized mechanism for managing task cancellation.
    -   Its `Token` property is passed to tasks to monitor for cancellation requests.
2.  **Triggering Cancellation**:
    -   The `CancelTask` listens for the Enter key.
    -   Calls `s_cts.Cancel()`, which triggers cancellation for all tasks observing the token.
3.  **Handling Cancellation**:
    -   Tasks like `ProcessUrlAsync` check the token during operations, throwing an `OperationCanceledException` when cancellation occurs.

* * * * *

### **Complete Example Code**

```
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

    static readonly HttpClient s_client = new HttpClient
    {
        MaxResponseContentBufferSize = 1_000_000
    };

    static readonly IEnumerable<string> s_urlList = new string[]
    {
        "<https://learn.microsoft.com>",
        "<https://learn.microsoft.com/aspnet/core>",
        "<https://learn.microsoft.com/azure>",
        "<https://learn.microsoft.com/azure/devops>",
        "<https://learn.microsoft.com/dotnet>",
        "<https://learn.microsoft.com/dynamics365>",
        "<https://learn.microsoft.com/education>",
        "<https://learn.microsoft.com/enterprise-mobility-security>",
        "<https://learn.microsoft.com/gaming>",
        "<https://learn.microsoft.com/graph>",
        "<https://learn.microsoft.com/microsoft-365>",
        "<https://learn.microsoft.com/office>",
        "<https://learn.microsoft.com/powershell>",
        "<https://learn.microsoft.com/sql>",
        "<https://learn.microsoft.com/surface>",
        "<https://learn.microsoft.com/system-center>",
        "<https://learn.microsoft.com/visualstudio>",
        "<https://learn.microsoft.com/windows>",
        "<https://learn.microsoft.com/maui>"
    };

    static async Task Main()
    {
        Console.WriteLine("Application started.");
        Console.WriteLine("Press the ENTER key to cancel...\\n");

        Task cancelTask = Task.Run(() =>
        {
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press the ENTER key to cancel...");
            }

            Console.WriteLine("\\nENTER key pressed: cancelling downloads.\\n");
            s_cts.Cancel();
        });

        Task sumPageSizesTask = SumPageSizesAsync();

        Task finishedTask = await Task.WhenAny(new[] { cancelTask, sumPageSizesTask });
        if (finishedTask == cancelTask)
        {
            try
            {
                await sumPageSizesTask;
                Console.WriteLine("Download task completed before cancel request was processed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Download task has been cancelled.");
            }
        }

        Console.WriteLine("Application ending.");
    }

    static async Task SumPageSizesAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        int total = 0;
        foreach (string url in s_urlList)
        {
            int contentLength = await ProcessUrlAsync(url, s_client, s_cts.Token);
            total += contentLength;
        }

        stopwatch.Stop();

        Console.WriteLine($"\\nTotal bytes returned:  {total:#,#}");
        Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\\n");
    }

    static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
    {
        HttpResponseMessage response = await client.GetAsync(url, token);
        byte[] content = await response.Content.ReadAsByteArrayAsync(token);
        Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

        return content.Length;
    }
}

```

* * * * *

### **Example Output**

```
Application started.
Press the ENTER key to cancel...

<https://learn.microsoft.com>                                       37,357
<https://learn.microsoft.com/aspnet/core>                           85,589
<https://learn.microsoft.com/azure>                                398,939

ENTER key pressed: cancelling downloads.

Download task has been cancelled.
Application ending.

```

* * * * *

### **Benefits**

1.  **Responsive Applications**:
    -   Users can cancel long-running tasks, enhancing user experience.
2.  **Resource Efficiency**:
    -   Avoids wasting bandwidth or processing power on unwanted tasks.
3.  **Code Clarity**:
    -   Separation of cancellation logic and task execution improves maintainability.