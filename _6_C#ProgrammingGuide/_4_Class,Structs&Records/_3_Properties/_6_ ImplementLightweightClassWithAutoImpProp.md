### **How to Implement a Lightweight Class with Automatically Implemented Properties**

A lightweight class with automatically implemented properties is designed to encapsulate simple data using minimal code. When immutability is required, such a class can prevent its data from being modified after the object is created. This approach is useful when you need reference type semantics (unlike structs) for small, simple data containers.

---

### **Key Characteristics of a Lightweight Immutable Class**

1. **Immutable Properties**:
   - Properties can only be set during object creation (constructor or object initializer).
   - Modifications to properties are restricted after the object is created.

2. **Options to Make Properties Immutable**:
   - **Get-Only Accessor**:
     The property can only be initialized in the constructor:
     ```csharp
     public string Name { get; }
     ```
   - **Init-Only Accessor**:
     The property can be set during object initialization:
     ```csharp
     public string Name { get; init; }
     ```
   - **Private Setter**:
     The property can only be modified within the class:
     ```csharp
     public string Address { get; private set; }
     ```

3. **Factory Methods for Object Creation**:
   - Static factory methods can be used to initialize objects, ensuring immutability.

---

### **Example: Lightweight Immutable Class**

#### **Class Implementation Using a Constructor**

This class demonstrates immutability using a public constructor:
```csharp
public class Contact
{
    // Immutable property with a get-only accessor.
    public string Name { get; }

    // Mutable within the class, immutable to external code.
    public string Address { get; private set; }

    // Constructor to initialize properties.
    public Contact(string name, string address)
    {
        Name = name;
        Address = address;
    }

    // Method to update the address (can only be done within the class).
    public void ChangeAddress(string newAddress)
    {
        Address = newAddress;
    }
}
```

---

#### **Class Implementation Using a Factory Method**

This approach uses a private constructor and a static factory method:
```csharp
public class Contact2
{
    // Mutable within the class, immutable to external code.
    public string Name { get; private set; }

    // Immutable property with a get-only accessor.
    public string Address { get; }

    // Private constructor.
    private Contact2(string name, string address)
    {
        Name = name;
        Address = address;
    }

    // Static factory method for object creation.
    public static Contact2 CreateContact(string name, string address)
    {
        return new Contact2(name, address);
    }
}
```

---

### **Example Usage**

#### **Creating Immutable Objects**

1. **Using Constructor**:
   ```csharp
   var contact = new Contact("John Doe", "123 Main St.");
   Console.WriteLine($"{contact.Name}, {contact.Address}");

   // The following would result in a compile error:
   // contact.Name = "New Name";
   ```

2. **Using Factory Method**:
   ```csharp
   var contact2 = Contact2.CreateContact("Jane Smith", "456 Elm St.");
   Console.WriteLine($"{contact2.Name}, {contact2.Address}");

   // The following would result in a compile error:
   // contact2.Name = "Another Name";
   ```

---

### **LINQ Example**

You can use these classes in LINQ queries to create immutable objects:

```csharp
string[] names = { "Alice", "Bob", "Charlie" };
string[] addresses = { "1st Street", "2nd Street", "3rd Street" };

// Using the Contact class (constructor approach).
var contacts = from i in Enumerable.Range(0, 3)
               select new Contact(names[i], addresses[i]);

// Using the Contact2 class (factory method approach).
var contacts2 = from i in Enumerable.Range(0, 3)
                select Contact2.CreateContact(names[i], addresses[i]);

// Display results.
foreach (var contact in contacts)
    Console.WriteLine($"{contact.Name}, {contact.Address}");

foreach (var contact2 in contacts2)
    Console.WriteLine($"{contact2.Name}, {contact2.Address}");
```

---

### **Output**
```
Alice, 1st Street
Bob, 2nd Street
Charlie, 3rd Street
Alice, 1st Street
Bob, 2nd Street
Charlie, 3rd Street
```

---

### **Key Notes**

1. **Compiler-Generated Backing Fields**:
   - Automatically implemented properties create private backing fields that cannot be accessed directly.
   - These fields are entirely managed by the compiler.

2. **Differences Between Get-Only and Private Set**:
   - **Get-Only**: The property value is immutable everywhere except during initialization in the constructor.
   - **Private Set**: The property value is mutable within the class but immutable to consumers.

3. **Why Use Reference Types Instead of Structs?**:
   - Use lightweight classes when reference type semantics are required (e.g., to pass references to the same object).

4. **Advantages of Factory Methods**:
   - Factory methods allow additional logic during object creation and can abstract complex initialization.

---

### **Summary**

To implement a lightweight class with automatically implemented properties:
- Use `get`-only or `private set` for immutability.
- Use constructors or factory methods for initialization.
- Leverage auto-properties to simplify code while ensuring encapsulation.