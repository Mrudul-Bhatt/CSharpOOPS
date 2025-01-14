### **Adding Another Event: Tracking Directory Search Progress**

This example demonstrates how to expand the functionality of the `FileSearcher` class to include **progress reporting** when searching through directories and subdirectories. It introduces a new event (`DirectoryChanged`) and uses idiomatic C# patterns for event declaration and handling.

---

### **Key Steps in Adding the `DirectoryChanged` Event**

#### **1. Define a New `EventArgs` Class**
The new event requires information about:
- The directory currently being searched (`CurrentSearchDirectory`).
- Total number of directories to be searched (`TotalDirs`).
- Number of directories already processed (`CompletedDirs`).

This information is encapsulated in the `SearchDirectoryArgs` class:

```csharp
internal class SearchDirectoryArgs : EventArgs
{
    internal string CurrentSearchDirectory { get; }
    internal int TotalDirs { get; }
    internal int CompletedDirs { get; }

    internal SearchDirectoryArgs(string dir, int totalDirs, int completedDirs)
    {
        CurrentSearchDirectory = dir;
        TotalDirs = totalDirs;
        CompletedDirs = completedDirs;
    }
}
```

- **Immutable Properties**: This design ensures that subscribers cannot modify the state of the `EventArgs` object, making it safer and consistent.
- **Internal Access Modifier**: Both the class and its properties are marked as `internal`, restricting their visibility to the same assembly.

---

#### **2. Declare the `DirectoryChanged` Event**
Unlike the simpler, "field-like" event declaration used earlier, this event is explicitly implemented with **add** and **remove** handlers:

```csharp
internal event EventHandler<SearchDirectoryArgs> DirectoryChanged
{
    add { _directoryChanged += value; }
    remove { _directoryChanged -= value; }
}
private EventHandler<SearchDirectoryArgs>? _directoryChanged;
```

- **Backing Field**: A private field `_directoryChanged` stores the event handlers.
- **add/remove Handlers**: These handlers allow more control over subscription and unsubscription logic. For this example, the default behavior is used.

---

#### **3. Implement the Overloaded `Search` Method**
The new `Search` method supports searching all subdirectories by using the `searchSubDirs` parameter:

```csharp
public void Search(string directory, string searchPattern, bool searchSubDirs = false)
{
    if (searchSubDirs)
    {
        var allDirectories = Directory.GetDirectories(directory, "*.*", SearchOption.AllDirectories);
        var completedDirs = 0;
        var totalDirs = allDirectories.Length + 1;

        foreach (var dir in allDirectories)
        {
            RaiseSearchDirectoryChanged(dir, totalDirs, completedDirs++);
            SearchDirectory(dir, searchPattern);
        }

        RaiseSearchDirectoryChanged(directory, totalDirs, completedDirs++);
        SearchDirectory(directory, searchPattern);
    }
    else
    {
        SearchDirectory(directory, searchPattern);
    }
}
```

- **Directory Traversal**:
  - Retrieves all subdirectories using `Directory.GetDirectories`.
  - Iterates through each subdirectory, raising the `DirectoryChanged` event before processing files in that directory.
- **Search Logic**:
  - Subdirectories are searched first.
  - The current directory is included after subdirectories.

---

#### **4. Raise the `DirectoryChanged` Event**
The event is raised via the `_directoryChanged` delegate:

```csharp
private void RaiseSearchDirectoryChanged(string directory, int totalDirs, int completedDirs) =>
    _directoryChanged?.Invoke(this, new SearchDirectoryArgs(directory, totalDirs, completedDirs));
```

- **Null Conditional Operator (`?.`)**: Ensures that the event is only invoked if there are subscribers.

---

#### **5. Ensure Compatibility with Existing Logic**
The existing logic for file searching remains unchanged. The `SearchDirectory` and `RaiseFileFound` methods are reused to handle file discovery and cancellation:

```csharp
private void SearchDirectory(string directory, string searchPattern)
{
    foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
    {
        FileFoundArgs args = RaiseFileFound(file);
        if (args.CancelRequested)
        {
            break;
        }
    }
}

private FileFoundArgs RaiseFileFound(string file)
{
    var args = new FileFoundArgs(file);
    FileFound?.Invoke(this, args);
    return args;
}
```

---

### **Subscribing to the `DirectoryChanged` Event**
Subscribers can track progress by handling the `DirectoryChanged` event. For example:

```csharp
fileLister.DirectoryChanged += (sender, eventArgs) =>
{
    Console.Write($"Entering '{eventArgs.CurrentSearchDirectory}'.");
    Console.WriteLine($" {eventArgs.CompletedDirs} of {eventArgs.TotalDirs} completed...");
};
```

- **Console Feedback**: Displays the name of the directory currently being searched and progress as a fraction of total directories.

---

### **Key Advantages of the Implementation**
1. **Idiomatic Event Handling**:
   - Demonstrates both field-like and explicit event declarations.
   - Follows best practices for immutability and encapsulation.

2. **Enhanced Usability**:
   - Subscribers can track progress dynamically.
   - Existing file search logic remains backward-compatible.

3. **Flexible Design**:
   - Internal access modifiers restrict visibility to intended users.
   - The design is loosely coupled, enabling independent evolution of subscribers and event logic.

---

### **Complete Example: FileSearcher Class**
```csharp
internal class FileSearcher
{
    public event EventHandler<FileFoundArgs>? FileFound;

    internal event EventHandler<SearchDirectoryArgs> DirectoryChanged
    {
        add { _directoryChanged += value; }
        remove { _directoryChanged -= value; }
    }
    private EventHandler<SearchDirectoryArgs>? _directoryChanged;

    public void Search(string directory, string searchPattern, bool searchSubDirs = false)
    {
        if (searchSubDirs)
        {
            var allDirectories = Directory.GetDirectories(directory, "*.*", SearchOption.AllDirectories);
            var completedDirs = 0;
            var totalDirs = allDirectories.Length + 1;

            foreach (var dir in allDirectories)
            {
                RaiseSearchDirectoryChanged(dir, totalDirs, completedDirs++);
                SearchDirectory(dir, searchPattern);
            }

            RaiseSearchDirectoryChanged(directory, totalDirs, completedDirs++);
            SearchDirectory(directory, searchPattern);
        }
        else
        {
            SearchDirectory(directory, searchPattern);
        }
    }

    private void SearchDirectory(string directory, string searchPattern)
    {
        foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
        {
            FileFoundArgs args = RaiseFileFound(file);
            if (args.CancelRequested)
            {
                break;
            }
        }
    }

    private void RaiseSearchDirectoryChanged(string directory, int totalDirs, int completedDirs) =>
        _directoryChanged?.Invoke(this, new SearchDirectoryArgs(directory, totalDirs, completedDirs));

    private FileFoundArgs RaiseFileFound(string file)
    {
        var args = new FileFoundArgs(file);
        FileFound?.Invoke(this, args);
        return args;
    }
}
```

---

### **Summary**
This implementation enhances the `FileSearcher` class with:
1. **Progress Reporting**: Adds a `DirectoryChanged` event for tracking directory traversal.
2. **Subscriber Flexibility**: Supports dynamic, real-time feedback.
3. **Idiomatic C# Design**: Adheres to event handling conventions in the .NET ecosystem.