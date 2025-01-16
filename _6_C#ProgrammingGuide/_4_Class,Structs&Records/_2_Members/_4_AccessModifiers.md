### **Access Modifiers in C#**

In C#, **access modifiers** are used to specify the visibility and accessibility of types and members (methods, fields, properties, etc.) to other code. The accessibility level controls whether other parts of the program (within the same assembly or different ones) can access the type or member. 

Here are the primary **access modifiers** in C#:

### **1. Public**
- **Accessibility**: Code in any assembly can access this type or member.
- **Usage**: This modifier makes the class, method, or field available to all other code, regardless of whether it is in the same assembly or another assembly.
  
  ```csharp
  public class Bicycle
  {
      public void Pedal() { }
  }
  ```

### **2. Private**
- **Accessibility**: Only code declared within the same class or struct can access this member.
- **Usage**: This modifier restricts access to the member to only within the defining class or struct. It's the most restrictive access level.

  ```csharp
  public class Bicycle
  {
      private int _wheels;  // Accessible only within Bicycle class
  }
  ```

### **3. Protected**
- **Accessibility**: Code in the same class or in a derived class can access this member.
- **Usage**: This modifier allows access from the defining class and any derived classes, even if those derived classes are in different assemblies.

  ```csharp
  public class Bicycle
  {
      protected void Pedal() { }  // Accessible within this class and derived classes
  }
  ```

### **4. Internal**
- **Accessibility**: Code within the same assembly can access this member.
- **Usage**: The `internal` modifier limits access to the current assembly, meaning any class or code within the same assembly can access the member.

  ```csharp
  public class Bicycle
  {
      internal int Wheels { get; set; }  // Accessible within the same assembly
  }
  ```

### **5. Protected Internal**
- **Accessibility**: Code in the same assembly or in a derived class in another assembly can access this member.
- **Usage**: This modifier combines the accessibility of `protected` and `internal`, allowing access within the same assembly and by derived classes in any assembly.

  ```csharp
  public class Bicycle
  {
      protected internal void Pedal() { }  // Accessible within the assembly or in derived classes
  }
  ```

### **6. Private Protected**
- **Accessibility**: Only code in the same assembly and in the same class or derived classes can access this member.
- **Usage**: This is a more restrictive form of `protected internal`, allowing access only to derived classes within the same assembly.

  ```csharp
  public class Bicycle
  {
      private protected void Pedal() { }  // Accessible within the assembly and in derived classes
  }
  ```

### **7. File**
- **Accessibility**: Code in the same file can access this member.
- **Usage**: The `file` modifier is used to restrict access to a specific file. It’s a very localized access level, often used in specialized scenarios.

  ```csharp
  file class Bicycle
  {
      // Only accessible within the same file
  }
  ```

---

### **Accessibility Rules Summary Table**

| **Access Modifier**   | **Within the File** | **Within the Class** | **Derived Class (Same Assembly)** | **Non-Derived Class (Same Assembly)** | **Derived Class (Different Assembly)** | **Non-Derived Class (Different Assembly)** |
|-----------------------|---------------------|----------------------|----------------------------------|--------------------------------------|----------------------------------------|------------------------------------------|
| **Public**            | ✔️️                | ✔️                   | ✔️                               | ✔️                                    | ✔️                                      | ✔️                                        |
| **Protected Internal**| ✔️                 | ✔️                   | ✔️                               | ✔️                                    | ✔️                                      | ❌                                        |
| **Protected**         | ✔️                 | ✔️                   | ✔️                               | ❌                                    | ✔️                                      | ❌                                        |
| **Internal**          | ✔️                 | ✔️                   | ❌                               | ✔️                                    | ❌                                      | ❌                                        |
| **Private Protected** | ✔️                 | ✔️                   | ✔️                               | ✔️                                    | ❌                                      | ❌                                        |
| **Private**           | ✔️                 | ✔️                   | ❌                               | ❌                                    | ❌                                      | ❌                                        |
| **File**              | ✔️                 | ✔️                   | ✔️                               | ✔️                                    | ✔️                                      | ✔️                                        |

---

### **Example Usage of Access Modifiers:**

```csharp
public class Tricycle
{
    // Public method: Accessible from anywhere
    public void Pedal() { }
    
    // Protected method: Accessible within this class and derived classes
    protected void PedalFaster() { }
    
    // Private field: Accessible only within this class
    private int _wheels = 3;
    
    // Protected Internal Property: Accessible within this assembly or from derived classes
    protected internal int Wheels
    {
        get { return _wheels; }
    }
}
```

### **Access Modifiers for Different Types:**
1. **Classes and Structs**:
   - Can be `public`, `internal`, or `file`. By default, they are `internal` if no access modifier is specified.
   
2. **Methods, Fields, Properties, and Events**:
   - Can be `public`, `protected`, `internal`, `protected internal`, `private`, or `private protected`. The accessibility of a member is generally no greater than the containing type.

3. **Interfaces**:
   - Can be `public` or `internal`, with `internal` being the default. Interface members are `public` by default, as they define the contract that must be implemented by other classes or structs.

4. **Delegates**:
   - Can have `internal` access by default.

---

### **Important Notes:**
- **Partial Classes**: If you are using partial classes (when a class is split across multiple files), all parts of the class must have the same access modifier. You cannot specify different access modifiers for different parts.
- **Member Accessibility**: A member’s type must be at least as accessible as the member itself. For example, you cannot have a `public` method returning a `private` class.
- **Finalizers**: Finalizers (or destructors) do not allow access modifiers.

### **Summary:**
Access modifiers in C# are essential for controlling the visibility and accessibility of types and members. They allow you to encapsulate and protect data while defining how other code can interact with your classes and their members. By carefully selecting appropriate access levels, you can design robust, secure, and maintainable code.