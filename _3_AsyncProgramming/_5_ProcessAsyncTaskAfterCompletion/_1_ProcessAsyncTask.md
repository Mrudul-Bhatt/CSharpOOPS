### **Process Asynchronous Tasks as They Complete in C#**

This approach allows you to efficiently handle multiple asynchronous tasks by processing each as soon as it completes, rather than waiting for all tasks to finish or processing them sequentially.

The key method used for this is `Task.WhenAny`, which returns the first task to complete among a collection of tasks. By removing the completed task from the collection and processing it, you can avoid waiting for slower tasks unnecessarily.

* * * * *

### **Core Concepts**

1.  **Task.WhenAny**:
    -   Takes a collection of tasks and returns a `Task` that represents the first completed task.
    -   The remaining tasks are still running in the background.
2.  **Deferred Execution**:
    -   LINQ queries are used to create the collection of tasks but are not executed immediately.
    -   Execution begins when you enumerate the collection, such as by calling `.ToList()`.
3.  **Efficient Task Processing**:
    -   Instead of processing tasks in the order they were started, this approach processes them in the order of completion.

* * * * *

### **Step-by-Step Explanation**

### **1\. Creating the Example Application**

-   Create a new .NET Core Console Application using Visual Studio or `dotnet new console`.

### **2\. Define Fields**

-   An `HttpClient` is initialized with a maximum response size.
-   A list of URLs (`s_urlList`) is defined as the data source.

### **3\. Asynchronous Entry Point**

-   The `Main` method is updated to be asynchronous by calling `SumPageSizesAsync`, enabling asynchronous operations at the program's entry point.

### **4\. Processing Tasks Asynchronously**

-   **Query for Tasks**: A LINQ query is used to create tasks for processing each URL.
-   **Execute Tasks**: The query is converted to a list (`downloadTasks`) to start all tasks.

### **5\. Handle Tasks with `Task.WhenAny`**

-   The `while` loop processes tasks as they complete:
    -   **Await `Task.WhenAny`**: Identifies the first completed task.
    -   **Remove the Completed Task**: Removes the task from the collection.
    -   **Await the Completed Task**: Retrieves the result of the completed task.

* * * * *

### **Complete Example Code**

```
using System.Diagnostics;

HttpClient s_client = new()
{
    MaxResponseContentBufferSize = 1_000_000
};

IEnumerable<string> s_urlList = new string[]
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

await SumPageSizesAsync();

async Task SumPageSizesAsync()
{
    var stopwatch = Stopwatch.StartNew();

    IEnumerable<Task<int>> downloadTasksQuery =
        from url in s_urlList
        select ProcessUrlAsync(url, s_client);

    List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

    int total = 0;
    while (downloadTasks.Any())
    {
        Task<int> finishedTask = await Task.WhenAny(downloadTasks);
        downloadTasks.Remove(finishedTask);
        total += await finishedTask;
    }

    stopwatch.Stop();

    Console.WriteLine($"\\nTotal bytes returned:    {total:#,#}");
    Console.WriteLine($"Elapsed time:            {stopwatch.Elapsed}\\n");
}

static async Task<int> ProcessUrlAsync(string url, HttpClient client)
{
    byte[] content = await client.GetByteArrayAsync(url);
    Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

    return content.Length;
}

```

* * * * *

### **How It Works**

1.  **Task Initialization**:
    -   Each URL is processed by `ProcessUrlAsync`, which uses `HttpClient.GetByteArrayAsync` to download the webpage content and returns its size.
2.  **Processing Tasks**:
    -   `Task.WhenAny` ensures that the fastest task is processed first.
    -   Completed tasks are removed from the list to avoid re-processing.
3.  **Output**:
    -   The program prints the size of each downloaded webpage and calculates the total size.

* * * * *

### **Example Output**

```
<https://learn.microsoft.com>                                      132,517
<https://learn.microsoft.com/azure/devops>                          86,034
<https://learn.microsoft.com/dotnet>                                78,706
...

Total bytes returned:    1,454,184
Elapsed time:            00:00:01.1290403

```

* * * * *

### **Key Considerations**

1.  **Error Handling**:
    -   Wrap task processing in a `try-catch` block to handle exceptions gracefully.
2.  **Task Scalability**:
    -   For a large number of tasks, consider batching or other parallel processing techniques to optimize performance.
3.  **Network Usage**:
    -   Ensure appropriate `HttpClient` configuration to avoid overwhelming the network or server.
4.  **Concurrency**:
    -   Use `HttpClient` efficiently by reusing a single instance across tasks.

* * * * *

### **Advantages**

-   **Efficiency**: Processes tasks as soon as they complete.
-   **Flexibility**: Handles tasks of varying durations effectively.
-   **Resource Utilization**: Avoids waiting for slower tasks unnecessarily.

This approach is ideal for scenarios where tasks involve asynchronous operations with varying completion times, such as downloading data from multiple sources or querying multiple APIs.