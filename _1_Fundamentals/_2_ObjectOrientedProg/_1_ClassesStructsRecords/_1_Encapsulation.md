### **Encapsulation in Object-Oriented Programming (OOP)**

**Encapsulation** is the principle of bundling data (fields) and methods (functions) that operate on that data into a
single unit, typically a class. It also involves restricting access to certain components of an object to ensure
controlled interaction and to hide implementation details from the outside world.

Encapsulation is one of the core principles of object-oriented programming (OOP), alongside inheritance and
polymorphism.

* * * * *

### **Key Benefits of Encapsulation**

1. **Data Hiding**:
    - Sensitive information (fields) is hidden from external code by using access modifiers like `private` or
      `protected`.
2. **Controlled Access**:
    - Access to the hidden data is provided through **getters** and **setters** (or properties in C#).
3. **Modularity**:
    - Code changes in one part of a class do not affect other parts of the application as long as the public interface
      remains the same.
4. **Improved Security**:
    - Prevents unauthorized or accidental modification of the internal state.

* * * * *

### **Access Modifiers in C#**

C# provides several access modifiers to control the visibility of class members:

- **`public`**: Accessible from anywhere.
- **`private`**: Accessible only within the same class.
- **`protected`**: Accessible within the same class and by derived classes.
- **`internal`**: Accessible within the same assembly.
- **`protected internal`**: Accessible within the same assembly and by derived classes.

* * * * *

### **Encapsulation Example**

### **Scenario**: Bank Account Class

You want to implement a `BankAccount` class where:

- The `Balance` field is hidden to prevent direct modification.
- A `Deposit` method allows controlled deposits.
- A `Withdraw` method allows controlled withdrawals.

* * * * *

### **Encapsulation in Practice**

### **Advantages in the Example**

1. **Data Hiding**:
    - The `_balance` field is private, so no external code can directly change its value.
2. **Controlled Access**:
    - Deposits and withdrawals are performed through methods with validation logic.
3. **Security**:
    - Ensures that only valid operations are performed on the `BankAccount` object.

* * * * *

### **When to Use Encapsulation**

Encapsulation should be applied when:

- You want to protect sensitive data or business logic.
- You need a clean separation of **what an object does** (public interface) from **how it does it** (internal
  implementation).
- You aim to minimize dependencies and simplify future changes.

* * * * *

### **Advanced Usage: Automatic Properties**

C# also provides a shorthand for encapsulation using automatic properties, which combine fields and accessors:

```
class Person
{
    // Automatic property with private set
    public string Name { get; private set; }

    // Constructor to initialize
    public Person(string name)
    {
        Name = name;
    }
}

```

This approach simplifies code while still enforcing encapsulation.