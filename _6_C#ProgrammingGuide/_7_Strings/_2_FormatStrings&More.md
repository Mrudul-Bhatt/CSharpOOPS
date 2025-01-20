### **Explanation:**

#### **Format Strings**
A **format string** dynamically determines its content at runtime by embedding expressions or placeholders enclosed in braces `{}`. These placeholders resolve to values or calculations when executed. You can create format strings using:

1. **String Interpolation**
2. **Composite Formatting**

---

### **1. String Interpolation**

**String interpolation** uses the `$` special character to embed variables or expressions directly in a string, improving readability and maintainability. For example:

```csharp
var person = (name: "Alice", age: 30);
Console.WriteLine($"{person.name} is {person.age} years old.");
// Output: Alice is 30 years old.
```

#### **Key Features:**
- Braces `{}` contain expressions evaluated at runtime.
- Supports inline expressions like calculations:
  ```csharp
  Console.WriteLine($"Next year, {person.name} will be {person.age + 1}.");
  ```

#### **Advanced Use with C# 11 Raw String Literals:**
String interpolation can also be combined with **raw string literals** (`"""`) to handle complex text or strings requiring `{}` characters.

```csharp
int x = 2, y = 3;
var message = $$"""The point {{{x}}, {{y}}} is {{Math.Sqrt(x * x + y * y)}} from the origin.""";
Console.WriteLine(message);
// Output: The point {2, 3} is 3.605551275463989 from the origin.
```

#### **Verbatim String Interpolation:**
Use `$@` or `@$` for **verbatim interpolated strings**, preserving line breaks and escaping rules:
```csharp
var path = "C:\\Program Files";
Console.WriteLine($@"The path is {path}.");
// Output: The path is C:\Program Files.
```

---

### **2. Composite Formatting**

Composite formatting uses placeholders in braces `{0}`, `{1}`, etc., to represent the values passed as arguments. Example:

```csharp
var author = (firstName: "Phillis", lastName: "Wheatley", year: 1753);
Console.WriteLine("{0} {1} was born in {2}.", author.firstName, author.lastName, author.year);
// Output: Phillis Wheatley was born in 1753.
```

#### **Key Features:**
- Each placeholder corresponds to a parameter in the `String.Format` method or `Console.WriteLine`.
- Useful for scenarios where formatting is separate from string creation.

---

### **Substrings**

A **substring** is any part of a string. To extract or manipulate substrings:
1. **`Substring`** method extracts a portion of a string:
   ```csharp
   string s = "Hello World";
   Console.WriteLine(s.Substring(0, 5)); // Output: Hello
   ```

2. **`Replace`** replaces occurrences of a substring:
   ```csharp
   Console.WriteLine(s.Replace("World", "C#")); // Output: Hello C#
   ```

3. **`IndexOf`** finds the position of a substring:
   ```csharp
   int index = s.IndexOf("World"); // index = 6
   ```

---

### **Accessing Individual Characters**
Use array notation to read characters at specific indices (0-based):
```csharp
string s = "Programming";
Console.WriteLine(s[0]); // Output: P
```

To reverse a string, iterate through its characters:
```csharp
for (int i = s.Length - 1; i >= 0; i--)
    Console.Write(s[i]);
// Output: gnimmargorP
```

---

### **Using `StringBuilder`**
The `StringBuilder` class is used for **mutable string manipulation**. Unlike `string`, it avoids creating new string objects during modifications, making it ideal for performance-critical operations.

1. **Construct and Modify Strings:**
   ```csharp
   var sb = new System.Text.StringBuilder("Cat");
   sb[0] = 'B';
   Console.WriteLine(sb); // Output: Bat
   ```

2. **Efficient String Creation in Loops:**
   ```csharp
   var sb = new StringBuilder();
   for (int i = 0; i < 10; i++)
       sb.Append(i.ToString());
   Console.WriteLine(sb); // Output: 0123456789
   ```

---

### **Null Strings vs. Empty Strings**
- **Empty String (`""`)**: Contains zero characters. Example:
  ```csharp
  string empty = String.Empty;
  Console.WriteLine(empty.Length); // Output: 0
  ```

- **Null String (`null`)**: Points to no instance of `String`. Example:
  ```csharp
  string? nullStr = null;
  Console.WriteLine(nullStr?.Length ?? 0); // Safe access with null coalescing: Output: 0
  ```

Avoid `NullReferenceException` by initializing strings to `String.Empty` or checking for null using:
```csharp
if (!string.IsNullOrEmpty(myString))
{
    // Safe operations
}
```

---

### **Using LINQ with Strings**
Since `String` implements `IEnumerable<char>`, you can use LINQ for advanced operations:

```csharp
string s = "hello";
var reversed = new string(s.Reverse().ToArray());
Console.WriteLine(reversed); // Output: olleh
```

This demonstrates the power of LINQ to manipulate strings effectively.