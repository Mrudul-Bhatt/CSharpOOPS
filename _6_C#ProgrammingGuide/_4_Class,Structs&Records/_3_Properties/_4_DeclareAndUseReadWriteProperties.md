### **Explanation: Declaring and Using Read/Write Properties in C#**

In C#, **properties** provide a way to encapsulate data members of a class. They allow controlled access to private fields using `get` and `set` methods, while maintaining the convenience of public field syntax. This approach ensures data is **protected, controlled, and validated**, reducing the risks of improper usage.

---

### **Understanding Properties**

1. **Definition**:
   A property in C# is a member that combines:
   - A **getter** (`get`): Retrieves the value of the underlying private field.
   - A **setter** (`set`): Assigns a value to the private field.

   Example:
   ```csharp
   private string _name; // Private backing field

   public string Name
   {
       get { return _name; } // Getter
       set { _name = value; } // Setter
   }
   ```

2. **Advantages**:
   - **Encapsulation**: Direct access to private fields is restricted.
   - **Validation**: You can add logic inside the `get` or `set` methods.
   - **Control**: Access to properties can be controlled using access modifiers.

3. **Syntax**:
   ```csharp
   public [Type] PropertyName
   {
       get { return [field]; }
       set { [field] = value; }
   }
   ```

---

### **Example: Read/Write Properties**

Here is a class with two read/write properties:

```csharp
class Person
{
    private string _name = "N/A"; // Backing field
    private int _age = 0;         // Backing field

    // Read/write property for Name
    public string Name
    {
        get { return _name; } // Getter
        set { _name = value; } // Setter
    }

    // Read/write property for Age
    public int Age
    {
        get { return _age; } // Getter
        set { _age = value; } // Setter
    }

    // Override ToString for a readable representation
    public override string ToString()
    {
        return $"Name = {Name}, Age = {Age}";
    }
}
```

---

### **Using the Properties**

#### **Main Program**
```csharp
class TestPerson
{
    static void Main()
    {
        // Create a new Person object
        Person person = new Person();

        // Default values from the constructor
        Console.WriteLine("Person details - {0}", person);

        // Set properties
        person.Name = "Joe";
        person.Age = 99;
        Console.WriteLine("Person details - {0}", person);

        // Increment the Age property
        person.Age += 1;
        Console.WriteLine("Person details - {0}", person);

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
```

#### **Output**
```
Person details - Name = N/A, Age = 0
Person details - Name = Joe, Age = 99
Person details - Name = Joe, Age = 100
```

---

### **Key Features of the Example**

1. **Read/Write Access**:
   Both the `Name` and `Age` properties allow reading and writing using the `get` and `set` accessors.

   Example usage:
   ```csharp
   person.Name = "Joe"; // Writing
   Console.WriteLine(person.Name); // Reading
   ```

2. **Automatic Access to the `value` Keyword**:
   The `set` accessor uses a special keyword `value`, which represents the value being assigned.

   Example:
   ```csharp
   set { _name = value; }
   ```

3. **Natural Syntax**:
   The properties provide a clean and intuitive way to manipulate data.
   ```csharp
   person.Age += 1; // Increment Age property
   ```

   Without properties, this would require explicit methods:
   ```csharp
   person.SetAge(person.GetAge() + 1);
   ```

4. **Overridden `ToString`**:
   The `ToString` method provides a readable representation of the object.
   - Invoked implicitly in `Console.WriteLine`:
     ```csharp
     Console.WriteLine("Person details - {0}", person);
     ```

---

### **Making Properties Read-Only or Write-Only**

#### **Read-Only Properties**
A property without a `set` accessor is read-only. This ensures the value can only be retrieved and not modified.

Example:
```csharp
private string _name = "N/A";

public string Name
{
    get { return _name; } // Read-only
}
```

#### **Write-Only Properties**
A property without a `get` accessor is write-only. This is less common but may be useful in specific scenarios.

Example:
```csharp
private string _password;

public string Password
{
    set { _password = value; } // Write-only
}
```

---

### **Asymmetric Accessibility**

You can have different access levels for `get` and `set`:
- Example:
  ```csharp
  public string Name
  {
      get { return _name; }       // Public getter
      private set { _name = value; } // Private setter
  }
  ```

This ensures:
- External code can **read** the value of `Name`.
- Only the class can **modify** the value.

---

### **Benefits of Properties**

1. **Encapsulation**:
   Properties provide a controlled way to access private fields.
2. **Validation**:
   You can include logic inside accessors to validate data.

   Example:
   ```csharp
   set
   {
       if (value < 0)
           throw new ArgumentException("Age cannot be negative.");
       _age = value;
   }
   ```

3. **Flexible Accessibility**:
   Properties allow fine-grained control over read/write access using asymmetric accessors.

---

### **Summary**

- Properties in C# are a powerful tool for encapsulating data members.
- Read/write properties have both `get` and `set` accessors.
- Asymmetric accessibility allows flexibility, like making properties read-only or write-only.
- They provide clean syntax and prevent unverified access to object data.
- `value` in the `set` accessor represents the value being assigned.