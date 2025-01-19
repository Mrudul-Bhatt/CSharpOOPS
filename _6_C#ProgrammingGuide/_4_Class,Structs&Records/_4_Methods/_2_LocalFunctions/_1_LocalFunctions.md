### **Local Functions in C#**

**Definition and Purpose:**
Local functions are methods nested inside another member, like a method, constructor, or property accessor. They are private to their containing member and cannot be accessed from outside it. This ensures encapsulation and prevents unintended access or misuse.

---

### **Key Features of Local Functions:**

1. **Accessibility:**
   - Local functions can only be called within their containing member.
   - They are implicitly private and cannot include access modifiers (e.g., `private`).

2. **Syntax:**
   ```csharp
   <modifiers> <return-type> <method-name> <parameter-list>
   ```

   - Modifiers allowed: `async`, `unsafe`, `static`, `extern`.
   - Static local functions cannot capture local variables or instance state.

3. **Context Access:**
   - Non-static local functions can access variables from their containing method, including parameters.
   - Static local functions are isolated and do not have access to these variables.

4. **Attributes:**
   - You can apply attributes to a local function, its parameters, or its type parameters.

---

### **Advantages:**

1. **Encapsulation:**
   - Keeps related logic together and prevents it from being called elsewhere.

2. **Improved Readability:**
   - Makes the intent of the code clearer by scoping methods closer to where they are used.

3. **Performance:**
   - Eliminates the overhead of defining a separate private method in cases where the function is only relevant locally.

4. **Safety:**
   - Prevents other parts of the class or struct from mistakenly calling the function.

---

### **Example 1: Basic Local Function**

```csharp
private static string GetText(string path, string filename)
{
    var reader = File.OpenText($"{AppendPathSeparator(path)}{filename}");
    var text = reader.ReadToEnd();
    return text;

    string AppendPathSeparator(string filepath)
    {
        return filepath.EndsWith(@"\") ? filepath : filepath + @"\";
    }
}
```

- The `AppendPathSeparator` function is only accessible within `GetText`.

---

### **Example 2: Local Functions with Attributes**

```csharp
private static void Process(string?[] lines, string mark)
{
    foreach (var line in lines)
    {
        if (IsValid(line))
        {
            // Processing logic...
        }
    }

    bool IsValid([NotNullWhen(true)] string? line)
    {
        return !string.IsNullOrEmpty(line) && line.Length >= mark.Length;
    }
}
```

- The `[NotNullWhen(true)]` attribute assists in nullable static analysis.

---

### **Local Functions and Exceptions**

Local functions can surface exceptions immediately in iterator and async methods, which improves error handling.

#### **Without Local Functions:**
Exceptions in iterators only surface when the enumerator is accessed:

```csharp
public static IEnumerable<int> OddSequence(int start, int end)
{
    if (end > 100)
        throw new ArgumentOutOfRangeException(nameof(end), "end must be <= 100.");
        
    for (int i = start; i <= end; i++)
    {
        if (i % 2 == 1)
            yield return i;
    }
}
```

#### **With Local Functions:**
Exceptions surface as soon as the iterator is created, enhancing predictability:

```csharp
public static IEnumerable<int> OddSequence(int start, int end)
{
    if (end > 100)
        throw new ArgumentOutOfRangeException(nameof(end), "end must be <= 100.");
        
    return GetOddSequenceEnumerator();

    IEnumerable<int> GetOddSequenceEnumerator()
    {
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 1)
                yield return i;
        }
    }
}
```

---

### **Local Functions vs. Lambda Expressions**

1. **Usage:**
   - Local functions are more versatile (can include multiple statements, attributes, and modifiers).
   - Lambda expressions are often more concise and suitable for single-use delegates.

2. **Performance:**
   - Local functions typically perform better as they avoid heap allocations for closures unless required.

---

### **Summary**

Local functions are a powerful feature in C# that improve code organization, readability, and safety. They provide a scoped, private way to encapsulate helper logic within a method or other member, ensuring that such logic is not exposed unnecessarily.