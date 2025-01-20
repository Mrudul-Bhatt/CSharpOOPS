### Strings and String Literals in C#

A **string** in C# is an object of type `String` used to store text. Internally, it is represented as a sequential, read-only collection of `Char` objects. Strings in C# are immutable, meaning their values cannot be modified once created. Any "modification" results in the creation of a new string object.

---

### **1. `string` vs. `System.String`**

- **`string`**: A C# keyword and an alias for the `System.String` class. Typically used for simplicity and readability.
- **`System.String`**: The actual .NET type that represents strings. Use it if you prefer explicitly referencing the type.
  
Both are interchangeable:
```csharp
string message = "Hello, World!";
System.String greeting = "Hi!";
```

---

### **2. Declaring and Initializing Strings**

Strings can be declared and initialized in various ways:

1. **Without initialization**:
   ```csharp
   string message1;
   ```

2. **With `null`**:
   ```csharp
   string? message2 = null;
   ```

3. **As an empty string**:
   ```csharp
   string message3 = string.Empty;  // Recommended over ""
   ```

4. **Using string literals**:
   ```csharp
   string path = "c:\\Program Files\\App";
   ```

5. **Using verbatim string literals** (preserve escape characters like backslashes and newlines):
   ```csharp
   string verbatimPath = @"c:\Program Files\App";
   ```

6. **Using `new` with a character array**:
   ```csharp
   char[] letters = { 'A', 'B', 'C' };
   string alphabet = new string(letters);
   ```

7. **Using `const` for compile-time constant strings**:
   ```csharp
   const string message = "I cannot change!";
   ```

---

### **3. String Immutability**

- Strings are immutable. Modifying a string creates a new object, leaving the original unchanged.
- Example:
  ```csharp
  string s1 = "Hello";
  s1 += " World";
  Console.WriteLine(s1);  // Output: Hello World
  ```

- References to original strings remain unchanged:
  ```csharp
  string str1 = "Hello ";
  string str2 = str1;  // Reference to str1
  str1 += "World";     // Creates a new string
  Console.WriteLine(str2);  // Output: Hello
  ```

---

### **4. Quoted String Literals**

- Quoted strings are enclosed in `"` and support escape sequences:
  ```csharp
  string text = "Column 1\tColumn 2";
  string title = "\"The Æolean Harp\" by Samuel Taylor Coleridge";
  ```

- Escape sequences:
  - `\t`: Tab
  - `\n`: New line
  - `\"`: Double quote

---

### **5. Verbatim String Literals**

- Verbatim strings start with `@` and preserve backslashes and newlines.
- Example:
  ```csharp
  string filePath = @"C:\Users\John\Documents";
  string multiLine = @"First line
  Second line";
  ```

- To include quotes:
  ```csharp
  string quote = @"She said, ""Hello!""";
  ```

---

### **6. Raw String Literals (C# 11)**

- Raw string literals are enclosed with `"""` and preserve formatting and characters without needing escape sequences.
- Single-line raw string:
  ```csharp
  string singleLine = """This is a "raw" string!""";
  ```

- Multi-line raw string:
  ```csharp
  string multiLine = """
      {
          "key": "value",
          "nested": {
              "key": "value"
          }
      }
      """;
  ```

- Benefits:
  - Eliminates the need for escape characters.
  - Matches the output's format exactly.

---

### **7. Common String Operations**

- Concatenation:
  ```csharp
  string s1 = "Hello";
  string s2 = "World";
  string result = s1 + " " + s2;
  ```

- Checking for empty or null:
  ```csharp
  if (string.IsNullOrEmpty(message)) { ... }
  ```

- Getting string length:
  ```csharp
  int length = message.Length;
  ```

- Accessing characters:
  ```csharp
  char firstChar = message[0];
  ```

- Substrings:
  ```csharp
  string sub = message.Substring(0, 5);
  ```

---

### **Conclusion**

Understanding strings in C# is critical since they are widely used. Key takeaways:
1. Strings are immutable.
2. Use `string.Empty` for empty strings to avoid null reference exceptions.
3. Use verbatim (`@`) and raw (`"""`) string literals for readability and managing special characters or multi-line strings.