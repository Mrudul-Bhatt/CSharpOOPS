**Default Interface Methods in C#: A Tutorial Overview**

Default interface methods, introduced in C# 8.0, allow interfaces to include method implementations. This feature provides flexibility when updating interfaces, especially in libraries used by numerous clients. Here’s an explanation of key concepts, scenarios, and implementation details from the tutorial.

---

### **Purpose of Default Interface Methods**
Traditionally, interfaces in C# were immutable once released. Adding a new method to an interface required every implementer to update their code, creating breaking changes. Default interface methods solve this problem by enabling:

1. **Adding Methods with Default Implementations**: New functionality can be introduced without breaking existing implementations.
2. **Custom Overrides**: Implementers can override default methods if custom logic is needed.
3. **Parameterized Implementations**: Default methods can support configurable behavior, making them versatile for various scenarios.

---

### **Scenario: Customer Relationship Library**

#### **Initial Setup**
The tutorial starts with version 1 of a library featuring two interfaces:
1. **`ICustomer`**: Represents a customer with properties like `DateJoined`, `PreviousOrders`, etc.
2. **`IOrder`**: Represents an order with `Purchased` date and `Cost`.

The library aims to enhance customer relationships by adding a **loyalty discount** feature.

---

### **Enhancing Interfaces with Default Methods**

#### **Step 1: Adding a Default Method**
A new method, `ComputeLoyaltyDiscount`, is added to the `ICustomer` interface with a default implementation:

```csharp
public decimal ComputeLoyaltyDiscount()
{
    DateTime TwoYearsAgo = DateTime.Now.AddYears(-2);
    if ((DateJoined < TwoYearsAgo) && (PreviousOrders.Count() > 10))
    {
        return 0.10m;
    }
    return 0;
}
```

- **Behavior**: Customers with more than 10 orders and membership longer than 2 years receive a 10% discount.
- **Usage**: Existing implementations of `ICustomer` automatically inherit this functionality without modification.

#### **Step 2: Accessing Default Methods**
To call a default method, the object must be cast to the interface type:

```csharp
ICustomer theCustomer = new SampleCustomer(...);
Console.WriteLine($"Discount: {theCustomer.ComputeLoyaltyDiscount()}");
```

---

### **Enhancing Flexibility with Parameterized Defaults**

#### **Adding Parameters**
Default behavior can be made configurable by introducing parameters:

```csharp
public static void SetLoyaltyThresholds(
    TimeSpan ago,
    int minimumOrders = 10,
    decimal percentageDiscount = 0.10m)
{
    length = ago;
    orderCount = minimumOrders;
    discountPercent = percentageDiscount;
}

private static TimeSpan length = TimeSpan.FromDays(365 * 2);
private static int orderCount = 10;
private static decimal discountPercent = 0.10m;

public decimal ComputeLoyaltyDiscount()
{
    DateTime start = DateTime.Now - length;
    if ((DateJoined < start) && (PreviousOrders.Count() > orderCount))
    {
        return discountPercent;
    }
    return 0;
}
```

- **Static Method**: `SetLoyaltyThresholds` allows consumers to adjust:
  - Membership duration (`ago`)
  - Minimum orders (`minimumOrders`)
  - Discount percentage (`percentageDiscount`)

Example:

```csharp
ICustomer.SetLoyaltyThresholds(TimeSpan.FromDays(30), 1, 0.25m);
```

---

### **Extending Default Implementations**

#### **Refactoring for Reusability**
The default logic is moved to a protected static helper method, allowing implementers to reuse or extend it:

```csharp
protected static decimal DefaultLoyaltyDiscount(ICustomer c)
{
    DateTime start = DateTime.Now - length;
    if ((c.DateJoined < start) && (c.PreviousOrders.Count() > orderCount))
    {
        return discountPercent;
    }
    return 0;
}

public decimal ComputeLoyaltyDiscount() => DefaultLoyaltyDiscount(this);
```

#### **Customizing Default Behavior**
Classes implementing the interface can override the default method and optionally reuse the helper method:

```csharp
public decimal ComputeLoyaltyDiscount()
{
    if (!PreviousOrders.Any())  // New customers get 50% discount.
        return 0.50m;

    return ICustomer.DefaultLoyaltyDiscount(this);  // Existing logic for others.
}
```

---

### **Key Features of Default Interface Methods**
1. **Backward Compatibility**: Non-breaking updates to interfaces.
2. **Static Members**: Interfaces can include static fields and methods.
3. **Access Modifiers**: Support for `public`, `protected`, `private`, etc.
4. **Parameterization**: Configurable defaults for flexible use cases.

---

### **Best Practices**
1. **Use Sparingly**: Keep interfaces focused and single-purpose.
2. **Maintain Defaults**: Provide robust and reasonable default implementations.
3. **Facilitate Overrides**: Design interfaces to support custom implementations.

---

### **Conclusion**
Default interface methods make it possible to evolve interfaces while preserving backward compatibility. They offer a powerful way to implement new functionality without breaking existing code, making them a valuable addition to C#.