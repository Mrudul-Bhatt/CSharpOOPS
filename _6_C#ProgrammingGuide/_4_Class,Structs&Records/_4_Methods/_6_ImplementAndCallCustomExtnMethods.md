### **How to Implement and Call a Custom Extension Method in C#**

Extension methods in C# allow you to add new methods to existing types without modifying their source code or creating a derived type. Below is a step-by-step guide to implementing and using a custom extension method.

---

### **1. Defining an Extension Method**

**Steps:**
1. **Create a static class**: The extension method must be in a static class. This class cannot be nested in another type and must be accessible to the client code.
2. **Define the method**:
   - The method should be `static`.
   - The **first parameter** must have the `this` modifier and specify the type you want to extend.
   - Additional parameters follow the first parameter as usual.
3. **Set accessibility**: Both the class and the method must have an appropriate access modifier to ensure visibility in client code.

---

### **2. Example: Word Count for Strings**

This example shows an extension method that counts the number of words in a string.

#### **Extension Method Implementation**
```csharp
namespace CustomExtensions
{
    // The class must be static
    public static class StringExtension
    {
        // The first parameter specifies the type being extended, with the 'this' modifier
        public static int WordCount(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            // Split the string into words and return the count
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
```

---

### **3. Using the Extension Method**

To use the `WordCount` extension method:
1. Add a reference to the assembly containing the extension method.
2. Add a `using` directive for the namespace where the extension method is defined.
3. Call the method as if it were a built-in method on the type.

#### **Client Code**
```csharp
using CustomExtensions;

class Program
{
    static void Main()
    {
        string sentence = "The quick brown fox jumped over the lazy dog.";
        
        // Call the extension method
        int count = sentence.WordCount();
        
        // Output the word count
        Console.WriteLine($"Word count: {count}");
    }
}
```

---

### **4. Key Notes on Extension Methods**

- **First Parameter**: The first parameter (`this string str` in the example) represents the type being extended. This parameter is **implicit** when calling the method.
  
  ```csharp
  // Explicit way (rarely used)
  int count = StringExtension.WordCount(sentence);
  ```

  ```csharp
  // Preferred implicit call
  int count = sentence.WordCount();
  ```

- **Access Modifiers**: The static class and the method must be accessible from the calling code.
  
  ```csharp
  // Example: `internal` visibility
  internal static class StringExtension
  {
      internal static int WordCount(this string str) { /* ... */ }
  }
  ```

- **Resolution Preference**: If a type already has an instance method or static method with the same signature, those take precedence over the extension method.

  ```csharp
  public static int WordCount(this string str) { /* ... */ }
  ```
  If `string` had a built-in `WordCount` method, it would always override the custom extension.

- **Cannot Access Private Members**: Extension methods can only access the public or internal members of the type they extend.

---

### **5. Common Use Cases**
- Adding utility methods for:
  - Built-in .NET types (`string`, `DateTime`, `List<T>`, etc.)
  - Custom types (for layer-specific functionality).
- Simplifying repeated logic:
  ```csharp
  public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
  ```

---

### **Summary**
1. Use a static class to define extension methods.
2. Make the first parameter the extended type with the `this` keyword.
3. Import the namespace and call the method like an instance method.
4. Ensure that the method and class have appropriate visibility.

This flexibility makes extension methods a powerful feature in C#.