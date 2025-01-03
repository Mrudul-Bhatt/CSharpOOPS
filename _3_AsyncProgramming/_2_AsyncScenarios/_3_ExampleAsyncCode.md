### **Examples of Writing Async Code in C#**

Here are several scenarios demonstrating how to write and use asynchronous code effectively, along with the relevant
considerations.

---

### **1\. Extract Data from a Network**

### **Example: Counting ".NET" occurrences in HTML**

This example downloads the HTML content of a given URL and counts occurrences of ".NET."

**[ASP.NET](http://ASP.NET) Web API Controller**:

```
[HttpGet, Route("DotNetCount")]
public static async Task<int> GetDotNetCount(string URL)
{
    // Asynchronously download the HTML from the provided URL
    var html = await s_httpClient.GetStringAsync(URL);

    // Count the number of ".NET" matches in the HTML
    return Regex.Matches(html, @"\\.NET").Count;
}

```

**Key Points**:

- **Suspending Execution**: The `await` keyword pauses `GetDotNetCount`, allowing the web server to handle other
  requests.
- **HTML Parsing**: For real-world use, avoid regex for HTML parsing; use dedicated libraries like **HtmlAgilityPack**
  or **AngleSharp**.

### **Example: Universal Windows App**

```
private readonly HttpClient _httpClient = new HttpClient();

private async void OnSeeTheDotNetsButtonClick(object sender, RoutedEventArgs e)
{
    // Start the asynchronous task
    var getHtmlTask = _httpClient.GetStringAsync("<https://dotnetfoundation.org>");

    // Perform UI updates before awaiting the task
    NetworkProgressBar.IsEnabled = true;
    NetworkProgressBar.Visibility = Visibility.Visible;

    // Wait for the HTML to download
    var html = await getHtmlTask;

    // Process the result
    int count = Regex.Matches(html, @"\\.NET").Count;
    DotNetCountLabel.Text = $"Number of .NETs: {count}";

    // Update UI after task completion
    NetworkProgressBar.IsEnabled = false;
    NetworkProgressBar.Visibility = Visibility.Collapsed;
}

```

**Key Points**:

- **User Feedback**: UI updates (e.g., enabling a progress bar) occur before the `await` call, improving user
  experience.
- **UI Responsiveness**: The `await` suspends execution, allowing the app to remain responsive.

---

### **2\. Waiting for Multiple Tasks**

When you need to retrieve multiple pieces of data concurrently, you can use **`Task.WhenAll`** or **`Task.WhenAny`**.

### **Example: Retrieve User Data**

**Using a `foreach` Loop**:

```
private static async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<int> userIds)
{
    var getUserTasks = new List<Task<User>>();

    // Start a task for each user ID
    foreach (int userId in userIds)
    {
        getUserTasks.Add(GetUserAsync(userId));
    }

    // Wait for all tasks to complete
    return await Task.WhenAll(getUserTasks);
}

```

**Using LINQ**:

```
private static async Task<User[]> GetUsersAsyncByLINQ(IEnumerable<int> userIds)
{
    // Start tasks for all user IDs and store them in an array
    var getUserTasks = userIds.Select(id => GetUserAsync(id)).ToArray();

    // Wait for all tasks to complete
    return await Task.WhenAll(getUserTasks);
}

```

**Key Points**:

- **`Task.WhenAll`**: Ensures all tasks complete before returning results.
- **LINQ with Async**: Use caution due to deferred execution. Use `ToArray` or `ToList` to force execution immediately.

---

### **3\. Task Combinators**

### **Using `Task.WhenAll`**

- **Scenario**: Wait for all tasks to complete.

- **Usage**:

  ```
  var task1 = DoWorkAsync();
  var task2 = FetchDataAsync();
  var task3 = ProcessDataAsync();

  await Task.WhenAll(task1, task2, task3);

  ```

### **Using `Task.WhenAny`**

- **Scenario**: Wait for the first task to complete and handle it immediately.

- **Usage**:

  ```
  var tasks = new List<Task> { Task1(), Task2(), Task3() };
  while (tasks.Count > 0)
  {
      var finishedTask = await Task.WhenAny(tasks);
      // Process finished task
      tasks.Remove(finishedTask);
  }

  ```

---

### **4\. Practical Considerations**

- **HTML Parsing**:
  - Avoid using regular expressions for parsing HTML in production; they are not robust against malformed HTML or
    changes in structure.
- **LINQ with Async**:
  - LINQ queries are lazy by default. Ensure execution by forcing evaluation with `ToArray` or `ToList`.
- **Task Scheduling**:
  - `Task.WhenAll` runs all tasks concurrently but does not control the order.
  - For dependent tasks, ensure sequential execution if required.
- **Resource Management**:
  - Properly manage `HttpClient` instances, as they can exhaust sockets when overused.

---

### **Summary**

The examples demonstrate:

1.  How to process data asynchronously with `await`.
2.  Techniques for handling multiple tasks simultaneously.
3.  When to use tools like `Task.WhenAll` and `Task.WhenAny`.
4.  Best practices for mixing LINQ and asynchronous programming.
