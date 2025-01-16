### **Expression-Bodied Members in C#**

Expression-bodied members allow you to write concise, readable implementations for class members when the logic can be expressed as a single statement or expression. These are often used for methods, properties, constructors, finalizers, and other members.

---

### **Syntax**
```csharp
member => expression;
```
- **`expression`**: A valid single statement or expression that implements the member.

---

### **Supported Members and Examples**

#### **1. Methods**
An **expression-bodied method** simplifies methods that consist of a single expression. The syntax eliminates the need for `{}` and a `return` keyword.

##### Example: Overriding `ToString` and Creating a Simple Method
```csharp
public override string ToString() => $"{fname} {lname}".Trim();
public void DisplayName() => Console.WriteLine(ToString());
```
- **Explanation**: 
  - `ToString` returns a formatted string using an expression.
  - `DisplayName` outputs the result of `ToString`.

---

#### **2. Read-Only Properties**
For read-only properties, the `get` accessor can be replaced with an expression body.

##### Example:
```csharp
public string Name => locationName;
```
- **Explanation**: 
  - The `Name` property returns the value of the private field `locationName`.

---

#### **3. Properties with Get/Set Accessors**
You can use expression bodies for both the `get` and `set` accessors of a property.

##### Example:
```csharp
public string Name
{
    get => locationName;
    set => locationName = value;
}
```
- **Explanation**:
  - `get` returns the value of `locationName`.
  - `set` assigns a new value to `locationName`.

---

#### **4. Events**
You can use expression-bodied definitions for the `add` and `remove` accessors of events.

##### Example:
```csharp
public event EventHandler Changed
{
    add => ChangedGeneric += (sender, args) => value(sender, args);
    remove => ChangedGeneric -= (sender, args) => value(sender, args);
}
```
- **Explanation**:
  - The `add` and `remove` accessors modify the underlying `ChangedGeneric` event handler.

---

#### **5. Constructors**
For constructors that initialize fields or properties with simple expressions, expression bodies are compact and elegant.

##### Example:
```csharp
public Location(string name) => Name = name;
```
- **Explanation**:
  - The `name` parameter is directly assigned to the `Name` property.

---

#### **6. Finalizers**
Finalizers, which are called when an object is being garbage collected, can also use expression bodies for simplicity.

##### Example:
```csharp
~Destroyer() => Console.WriteLine($"The {ToString()} finalizer is executing.");
```
- **Explanation**:
  - The finalizer outputs a message when executed.

---

#### **7. Indexers**
Indexers can use expression bodies for their `get` and `set` accessors.

##### Example:
```csharp
public string this[int i]
{
    get => types[i];
    set => types[i] = value;
}
```
- **Explanation**:
  - `get` returns the value at the specified index.
  - `set` assigns a value to the specified index in the `types` array.

---

### **Benefits of Expression-Bodied Members**
1. **Conciseness**: Reduces boilerplate code and improves readability.
2. **Clarity**: Focuses on the core logic without distractions from syntax.
3. **Consistency**: Ideal for simple operations like getters, setters, or one-line methods.

---

### **Complete Example with Various Expression-Bodied Members**
```csharp
using System;

namespace ExpressionBodiedDemo
{
    public class Person
    {
        private string fname;
        private string lname;

        public Person(string firstName, string lastName) => (fname, lname) = (firstName, lastName);

        public string FullName => $"{fname} {lname}";

        public override string ToString() => FullName;

        public void DisplayName() => Console.WriteLine(FullName);
    }

    public class Destroyer
    {
        ~Destroyer() => Console.WriteLine("Destroyer finalized!");
    }

    public class Sports
    {
        private string[] types = { "Baseball", "Soccer", "Tennis" };

        public string this[int index]
        {
            get => types[index];
            set => types[index] = value;
        }
    }

    class Program
    {
        static void Main()
        {
            var person = new Person("John", "Doe");
            Console.WriteLine(person);        // Output: John Doe
            person.DisplayName();             // Output: John Doe

            var sports = new Sports();
            Console.WriteLine(sports[1]);     // Output: Soccer
            sports[1] = "Basketball";
            Console.WriteLine(sports[1]);     // Output: Basketball

            _ = new Destroyer();
            GC.Collect();                    // Explicitly trigger finalizer (not recommended in real code)
        }
    }
}
```

---

### **Key Takeaways**
1. **Expression-bodied members** are a syntactic sugar for single-line logic.
2. They can be applied to methods, properties, constructors, finalizers, indexers, and events.
3. Use them to simplify code, but avoid overusing them for complex logic to maintain readability.