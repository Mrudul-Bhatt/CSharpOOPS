### **Explanation: Common Usage Patterns of Extension Methods**

Extension methods are a powerful feature in C# that allow you to extend existing types with new functionality, even if you don’t own the source code for the type. Below are the main usage patterns and considerations explained in detail:

---

### **1. Collection Functionality**
Instead of creating custom "collection classes" to encapsulate operations on collections, extension methods can extend any type that implements `System.Collections.Generic.IEnumerable<T>`. 

**Advantages:**
- Reusability: The functionality applies to all collections that implement `IEnumerable<T>`.
- Simplified Syntax: Methods can be called directly on arrays, lists, and other collections using instance method syntax.

**Example:**
```csharp
int[] numbers = { 1, 2, 3, 4, 5 };
// LINQ methods like OrderBy are extension methods
var sorted = numbers.OrderBy(x => x);
```

By adding a custom extension method to `IEnumerable<T>`, you can perform specialized operations without creating a new collection type.

---

### **2. Layer-Specific Functionality**
In layered architectures, like Onion Architecture, domain entities are typically minimalistic and contain no or little business logic. Extension methods can add functionality relevant to specific layers (e.g., data formatting in the presentation layer) without modifying or cluttering the core entities.

**Example:**
```csharp
public class DomainEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public static class DomainEntityExtensions
{
    public static string FullName(this DomainEntity entity)
        => $"{entity.FirstName} {entity.LastName}";
}

// Usage
var entity = new DomainEntity { FirstName = "John", LastName = "Doe" };
Console.WriteLine(entity.FullName()); // Output: "John Doe"
```

This pattern keeps core entities clean while providing flexibility for application-specific logic.

---

### **3. Extending Predefined Types**
Instead of creating utility classes, you can add functionality directly to existing .NET types, like `System.String` or `System.Data.SqlClient.SqlConnection`.

**Use Cases:**
- Add domain-specific operations to types like `string` or `Stream`.
- Enhance functionality of types like `Exception` for custom error handling.

**Example: Extending SqlConnection**
```csharp
public static class SqlConnectionExtensions
{
    public static void ExecuteQuery(this SqlConnection connection, string query)
    {
        using var command = connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
    }
}

// Usage
using var connection = new SqlConnection("your_connection_string");
connection.ExecuteQuery("CREATE TABLE Example (Id INT)");
```

---

### **4. Ref Extension Methods**
For value types (like `struct`), extension methods typically operate on a copy. Adding the `ref` modifier allows you to modify the actual value.

**Key Points:**
- The `ref` modifier must be used both in the method definition and at the call site.
- Useful for modifying the state of value types without reassigning them explicitly.

**Example:**
```csharp
public static class IntExtensions
{
    public static void RefIncrement(this ref int number)
        => number++;
}

// Usage
int x = 5;
x.RefIncrement();
Console.WriteLine(x); // Output: 6
```

---

### **5. Guidelines and Best Practices**
- **Avoid Name Collisions:** If a type has an instance method with the same signature as an extension method, the instance method takes precedence.
- **Namespace Scope:** Extension methods are brought into scope through `using` directives. Keep them well-organized in specific namespaces.
- **Versioning:** Don’t use extension methods as a way to avoid incrementing a library’s version. Follow proper versioning guidelines for significant changes.
- **Type-Specific Behavior:** Be cautious when extending third-party types as changes to the type’s implementation might break your extension methods.

---

### **Cautions When Extending Structs**
- Structs are value types, so passing them to methods typically creates a copy.
- Use `ref` to modify the original struct.
- Extension methods cannot access private fields of structs.

**Example: Modifying a Struct**
```csharp
public struct Account
{
    public float Balance { get; set; }
}

public static class AccountExtensions
{
    public static void Deposit(ref this Account account, float amount)
    {
        account.Balance += amount;
    }
}

// Usage
Account acc = new Account { Balance = 100 };
acc.Deposit(50);
Console.WriteLine(acc.Balance); // Output: 150
```

---

### **Summary**
Extension methods:
- Provide a way to extend existing types without modifying them.
- Are ideal for reusable collection utilities, layer-specific logic, and enhancing predefined types.
- Allow for `ref`-based modifications of value types.
- Should be used thoughtfully to avoid conflicts and ensure maintainability.