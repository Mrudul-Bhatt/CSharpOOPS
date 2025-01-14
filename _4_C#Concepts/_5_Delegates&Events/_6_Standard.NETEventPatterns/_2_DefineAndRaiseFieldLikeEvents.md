### **Understanding Field-Like Events in C#**

Field-like events are the simplest and most common way to declare events in C#. They offer a concise way to define an event while ensuring safety and encapsulation. Here's an explanation of how they work and why they are safe despite their simplicity.

---

### **1. Defining a Field-Like Event**

A field-like event is declared using the `event` keyword:

```csharp
public event EventHandler<FileFoundArgs>? FileFound;
```

#### **Key Points**
- **Syntax Resemblance**: This looks like a public field declaration, but it is not. The `event` keyword ensures that the compiler generates additional code to enforce encapsulation and proper usage.
- **Encapsulation**: The compiler ensures that the event can only be accessed in controlled ways (subscribing or unsubscribing handlers). External code cannot raise the event or manipulate its invocation list directly.

---

### **2. How Field-Like Events Work**

#### **Generated Code**
Behind the scenes, the compiler generates code similar to the following:

```csharp
private EventHandler<FileFoundArgs>? _fileFound;

public event EventHandler<FileFoundArgs>? FileFound
{
    add { _fileFound += value; }
    remove { _fileFound -= value; }
}
```

- **Private Backing Field**: The event uses a private delegate field to store its subscribers.
- **Add/Remove Accessors**: The `add` and `remove` methods handle the subscription (`+=`) and unsubscription (`-=`) of event handlers.

---

### **3. Subscribing to a Field-Like Event**

You can add an event handler using the `+=` operator:

```csharp
var fileLister = new FileSearcher();
int filesFound = 0;

EventHandler<FileFoundArgs> onFileFound = (sender, eventArgs) =>
{
    Console.WriteLine(eventArgs.FoundFile);
    filesFound++;
};

fileLister.FileFound += onFileFound; // Subscribe to the event
```

#### **Important Details**
- **Handler as a Local Variable**: Store the lambda or method reference in a variable (`onFileFound`). This is crucial because each lambda expression creates a new delegate instance. Without the variable, unsubscribing would silently fail because it would reference a different delegate instance.

---

### **4. Unsubscribing from a Field-Like Event**

Remove an event handler using the `-=` operator:

```csharp
fileLister.FileFound -= onFileFound; // Unsubscribe from the event
```

- **Correct Unsubscription**: The handler variable ensures that the correct delegate instance is removed from the event's invocation list.

---

### **5. Encapsulation and Safety**

#### **What Code Outside the Class Can Do**
- Subscribe to the event using `+=`.
- Unsubscribe from the event using `-=`.

#### **What Code Outside the Class Cannot Do**
- **Raise the Event**: Only the class that declares the event can invoke it.
- **Directly Access the Delegate**: The backing delegate (`_fileFound`) is private, so external code cannot manipulate it.

This ensures that event handling remains controlled and prevents misuse.

---

### **6. Raising a Field-Like Event**

Only the class that defines the event can raise it. Typically, you use a helper method to raise the event safely:

```csharp
private void RaiseFileFound(string file) =>
    FileFound?.Invoke(this, new FileFoundArgs(file));
```

- **Null Check**: The `?.Invoke` syntax ensures the event is only raised if there are subscribers.
- **Encapsulation**: External code cannot raise the event, protecting its integrity.

---

### **7. Why Use Field-Like Events?**

#### **Advantages**
1. **Conciseness**: A single line of code defines the event, making it easy to use.
2. **Safety**: The compiler-generated add/remove accessors enforce safe access patterns.
3. **Flexibility**: Supports multiple subscribers and ensures that no action is taken if there are no subscribers.

#### **Considerations**
- You cannot customize the `add` and `remove` behavior directly with field-like events. For more control, use a full event declaration with explicit accessors.

---

### **8. Example: FileSearcher with Field-Like Event**

Here’s the full implementation of a `FileSearcher` class using a field-like event:

```csharp
public class FileSearcher
{
    public event EventHandler<FileFoundArgs>? FileFound;

    public void Search(string directory, string searchPattern)
    {
        foreach (var file in Directory.EnumerateFiles(directory, searchPattern))
        {
            RaiseFileFound(file);
        }
    }

    private void RaiseFileFound(string file) =>
        FileFound?.Invoke(this, new FileFoundArgs(file));
}
```

#### **Usage**
```csharp
var fileSearcher = new FileSearcher();
fileSearcher.FileFound += (sender, args) =>
{
    Console.WriteLine($"File found: {args.FoundFile}");
};

fileSearcher.Search("C:\\example", "*.txt");
```

---

### **9. Summary**

Field-like events in C# provide a straightforward way to declare and use events. Although they resemble public fields, the compiler-generated code ensures proper encapsulation and controlled access. Key benefits include simplicity, safety, and support for multiple subscribers. When more control over the event's behavior is needed, consider defining a custom event with explicit add/remove accessors.