### **Inherited and Overridden Methods in C#**

In C#, all classes indirectly inherit from the base `Object` class, meaning that even if you don't explicitly define methods in your class, it will inherit methods such as `Equals`, `GetHashCode`, `ToString`, and `GetType`.

---

### **1. Inherited Methods**
- **Definition**: Methods that a class gains from its base class without explicitly defining them.
- **Example**: The `Person` class in your example inherits the `Equals(Object)` method from the `Object` class.
- **Behavior**:
  - By default, the `Equals(Object)` method performs **reference equality** for reference types. It checks if two variables refer to the same memory location.
- **Example Code**:
  ```csharp
  public class Person
  {
      public string FirstName = default!;
  }

  public static class ClassTypeExample
  {
      public static void Main()
      {
          Person p1 = new() { FirstName = "John" };
          Person p2 = new() { FirstName = "John" };

          // Uses Object.Equals for reference equality
          Console.WriteLine("p1 = p2: {0}", p1.Equals(p2));  // False
      }
  }
  ```
  **Output**:
  ```
  p1 = p2: False
  ```
  Here, `Equals` returns `false` because `p1` and `p2` reference different objects, even though their `FirstName` fields are identical.

---

### **2. Overridden Methods**
- **Definition**: A class can override methods inherited from its base class to provide a custom implementation.
- **How to Override**:
  - Use the `override` keyword.
  - Ensure the method signature matches the base class method exactly.
  - Mark the base class method as `virtual` or `abstract` to allow overriding.
- **Why Override?**:
  - To change the behavior of a method (e.g., provide a custom equality check or string representation).

---

### **3. Example: Overriding `Equals`**
- **Custom Equality Logic**:
  - In the overridden `Equals` method, you define equality based on the `FirstName` field instead of reference equality.
  - To maintain consistency, you also override `GetHashCode` to ensure objects that are equal have the same hash code.
- **Code Example**:
  ```csharp
  namespace methods;

  public class Person
  {
      public string FirstName = default!;

      public override bool Equals(object? obj) =>
          obj is Person p2 &&
          FirstName.Equals(p2.FirstName);

      public override int GetHashCode() => FirstName.GetHashCode();
  }

  public static class Example
  {
      public static void Main()
      {
          Person p1 = new() { FirstName = "John" };
          Person p2 = new() { FirstName = "John" };

          // Uses the overridden Equals method
          Console.WriteLine("p1 = p2: {0}", p1.Equals(p2));  // True
      }
  }
  ```
  **Output**:
  ```
  p1 = p2: True
  ```
  - The overridden `Equals` method compares the `FirstName` values of `p1` and `p2`.
  - If the `FirstName` values match, the method returns `true`.

---

### **4. Why Override `GetHashCode`?**
- **Hash Codes and Equality**:
  - When overriding `Equals`, it’s crucial to also override `GetHashCode` to ensure objects considered equal produce the same hash code.
  - This is particularly important for collections like `HashSet` or as keys in a `Dictionary`.

- **Custom Implementation**:
  ```csharp
  public override int GetHashCode() => FirstName.GetHashCode();
  ```
  - This ensures that two `Person` objects with the same `FirstName` will have identical hash codes.

---

### **5. Key Points**
1. **Virtual and Override**:
   - A method must be marked as `virtual` or `abstract` in the base class to allow overriding in derived classes.
   - Example:
     ```csharp
     public class BaseClass
     {
         public virtual void Print() => Console.WriteLine("Base Print");
     }

     public class DerivedClass : BaseClass
     {
         public override void Print() => Console.WriteLine("Derived Print");
     }
     ```
     **Output**:
     ```
     Derived Print
     ```

2. **sealed**:
   - Prevents further overriding in derived classes:
     ```csharp
     public override sealed void Print() => Console.WriteLine("Cannot be overridden further");
     ```

3. **Polymorphism**:
   - Overridden methods allow polymorphism, meaning a derived class object can invoke its custom implementation through a base class reference.

---

### **6. Summary**
- **Inheritance** allows a class to reuse the functionality of its base class.
- **Override** lets a class provide its own implementation for an inherited method.
- **Custom Logic**: Overriding methods like `Equals` and `GetHashCode` enables meaningful comparisons and ensures consistency in hash-based collections.
- Proper use of inheritance and method overriding enhances code reusability, clarity, and flexibility.