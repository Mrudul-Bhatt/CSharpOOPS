### **Assigning, Adding, and Removing Invocation Targets for Delegates**

Delegates in C# allow you to define and manage invocation lists—lists of methods that are executed when the delegate is invoked. Here's how you can assign methods, add multiple methods to a delegate's invocation list, and remove them.

---

### **Assigning a Single Target**

#### **Example with `List.Sort()`**
Suppose you want to sort a list of strings by their lengths. First, define a comparison method:

```csharp
private static int CompareLength(string left, string right) =>
    left.Length.CompareTo(right.Length);
```

- This method compares the lengths of two strings and returns an integer:
  - `0` if equal length.
  - `<0` if the first string is shorter.
  - `>0` if the first string is longer.

To use this method as a delegate for `List.Sort()`, simply pass it as an argument:

```csharp
phrases.Sort(CompareLength);
```

- **How it works**: The method name `CompareLength` (without parentheses) is treated as a reference, converted into a delegate, and attached as an invocation target to `phrases.Sort()`.

---

### **Explicit Delegate Assignment**

Instead of passing the method directly, you can explicitly assign it to a delegate instance of type `Comparison<string>`:

```csharp
Comparison<string> comparer = CompareLength;
phrases.Sort(comparer);
```

- **Advantage**: Explicit assignment makes the code more readable, especially when working with multiple delegates or custom logic.

---

### **Using Lambda Expressions**

For small methods, lambda expressions offer a concise way to define and assign a delegate:

```csharp
Comparison<string> comparer = (left, right) => left.Length.CompareTo(right.Length);
phrases.Sort(comparer);
```

- **Advantages of Lambdas**:
  - No need for a separate method definition.
  - Useful for one-off logic.

---

### **Adding Methods to a Delegate's Invocation List**

Delegates support **multicast behavior**, where multiple methods are attached to a delegate and executed sequentially. Use the `+=` operator to add methods:

```csharp
Comparison<string> comparer = CompareLength;
comparer += (left, right) => string.Compare(left, right, StringComparison.OrdinalIgnoreCase);
phrases.Sort(comparer);
```

1. The first method (`CompareLength`) compares string lengths.
2. The second method compares strings lexicographically, ignoring case.

---

### **Removing Methods from a Delegate**

You can remove methods from a delegate's invocation list using the `-=` operator:

```csharp
comparer -= CompareLength;
```

- If `CompareLength` was attached, it will be removed.
- If it wasn’t in the list, the operation does nothing.

---

### **Key Points About Invocation Lists**

1. **Execution Order**:
   - Methods are invoked in the order they were added to the invocation list.
2. **Return Values**:
   - For delegates with a return type, only the return value of the **last method** in the invocation list is used.
3. **Exceptions**:
   - If a method in the invocation list throws an exception, subsequent methods are not executed.

---

### **Example: Multicast Delegate**

Here’s a simple demonstration of a multicast delegate:

```csharp
public delegate void Notify(string message);

private static void FirstListener(string message) => Console.WriteLine($"First: {message}");
private static void SecondListener(string message) => Console.WriteLine($"Second: {message}");

Notify notifier = FirstListener;
notifier += SecondListener;

notifier("Hello, Delegates!");
```

**Output**:
```
First: Hello, Delegates!
Second: Hello, Delegates!
```

- `FirstListener` and `SecondListener` are both executed in sequence.

---

### **Best Practices**

1. **Use Lambdas for Simple Logic**:
   - Prefer lambdas for concise, one-off operations.

2. **Explicit Assignment for Clarity**:
   - Explicitly assign methods to delegate instances when clarity is more important.

3. **Avoid Side Effects in Multicast Delegates**:
   - Multicast delegates are powerful but should be used cautiously, as they may cause unintended side effects if methods depend on execution order or shared state.

---

This understanding prepares you to use delegates effectively in scenarios such as custom sorting, event handling, and designing reusable components.