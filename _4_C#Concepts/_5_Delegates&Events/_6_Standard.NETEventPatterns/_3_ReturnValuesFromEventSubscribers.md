### **Understanding Return Values from Event Subscribers: Cancellation Pattern**

In C#, event handlers do not directly return values because events use multicast delegates, which may invoke multiple subscribers. Instead, communication back to the event source (e.g., to request cancellation) is achieved through **modifying properties of the `EventArgs` object** passed to the event handlers.

---

### **Cancellation Using EventArgs**

#### **Scenario**
When a file is found, subscribers should have the ability to:
1. Process the event (e.g., log or display the file name).
2. Indicate whether the operation (e.g., searching for files) should stop.

This is achieved by adding a **cancellation flag** (`CancelRequested`) to the `EventArgs` object, which subscribers can modify.

---

### **Implementation Steps**

#### 1. Modify `FileFoundArgs` to Include `CancelRequested`

The `FileFoundArgs` class is updated to include a property (`CancelRequested`) that indicates whether the operation should stop:

```csharp
public class FileFoundArgs : EventArgs
{
    public string FoundFile { get; }
    public bool CancelRequested { get; set; } // Default: false

    public FileFoundArgs(string fileName) => FoundFile = fileName;
}
```

- **Default Value**: The `CancelRequested` property is initialized to `false` by default, ensuring the operation does not cancel unless explicitly requested by a subscriber.

---

#### 2. Update the Search Logic to Handle Cancellation

The `FileSearcher` class's `Search` method is updated to check for cancellation after raising the event:

```csharp
private void SearchDirectory(string directory, string searchPattern)
{
    foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
    {
        FileFoundArgs args = RaiseFileFound(file);

        // Stop processing if cancellation is requested
        if (args.CancelRequested)
        {
            break;
        }
    }
}

private FileFoundArgs RaiseFileFound(string file)
{
    var args = new FileFoundArgs(file);

    // Raise the event (invoke all subscribers)
    FileFound?.Invoke(this, args);

    return args;
}
```

- **Cancellation Check**: After the event is raised and all subscribers have processed it, the `CancelRequested` flag is examined. If it is `true`, the search is terminated.

---

#### 3. Update Event Subscribers to Support Cancellation

Subscribers can now set the `CancelRequested` property to `true` when they want to stop further processing. For example:

```csharp
EventHandler<FileFoundArgs> onFileFound = (sender, eventArgs) =>
{
    Console.WriteLine(eventArgs.FoundFile);

    // Cancel the search when the first executable file is found
    if (eventArgs.FoundFile.EndsWith(".exe"))
    {
        eventArgs.CancelRequested = true;
    }
};
```

---

### **Advantages of This Approach**

1. **Non-Breaking Change**: 
   - Existing subscribers do not need to change their code unless they want to support cancellation.
   - Default behavior (no cancellation) is preserved by initializing `CancelRequested` to `false`.

2. **Loose Coupling**: 
   - Event subscribers independently decide whether to request cancellation.
   - The event source (`FileSearcher`) simply checks the `CancelRequested` flag without knowing the specifics of the subscribers.

3. **Scalability**: 
   - Multiple subscribers can respond to the same event. The cancellation logic remains simple and robust.

---

### **Alternatives: Cancel-If-All Pattern**

In some cases, you may require **all subscribers** to agree on cancellation. For example:

- Initialize `CancelRequested` to `true`.
- Subscribers set `CancelRequested` to `false` if they want the operation to continue.
- The event source determines cancellation based on the final value of `CancelRequested` **only if at least one subscriber exists**.

This pattern adds a bit of complexity, as the event source must check if there are any subscribers:

```csharp
private void SearchDirectory(string directory, string searchPattern)
{
    foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
    {
        FileFoundArgs args = new FileFoundArgs(file) { CancelRequested = true };

        // Raise the event
        FileFound?.Invoke(this, args);

        // If no subscribers or if all agree to cancel
        if (FileFound == null || args.CancelRequested)
        {
            break;
        }
    }
}
```

---

### **Example: Complete Implementation**

#### `FileFoundArgs`
```csharp
public class FileFoundArgs : EventArgs
{
    public string FoundFile { get; }
    public bool CancelRequested { get; set; }

    public FileFoundArgs(string fileName) => FoundFile = fileName;
}
```

#### `FileSearcher`
```csharp
public class FileSearcher
{
    public event EventHandler<FileFoundArgs>? FileFound;

    public void SearchDirectory(string directory, string searchPattern)
    {
        foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
        {
            var args = new FileFoundArgs(file);

            // Raise event
            FileFound?.Invoke(this, args);

            // Check for cancellation
            if (args.CancelRequested)
            {
                Console.WriteLine("Search cancelled.");
                break;
            }
        }
    }
}
```

#### Usage
```csharp
var fileSearcher = new FileSearcher();
fileSearcher.FileFound += (sender, args) =>
{
    Console.WriteLine($"Found: {args.FoundFile}");

    // Cancel if the file is an executable
    if (args.FoundFile.EndsWith(".exe"))
    {
        args.CancelRequested = true;
    }
};

// Start search
fileSearcher.SearchDirectory("C:\\example", "*.exe");
```

---

### **Summary**

- **Problem**: Events do not return values because they support multiple subscribers. 
- **Solution**: Use a shared `EventArgs` object to communicate state changes, such as cancellation.
- **Pattern**: 
  - Add a `CancelRequested` flag to `EventArgs`.
  - Subscribers set the flag based on their logic.
  - The event source checks the flag after raising the event and acts accordingly.