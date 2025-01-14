### **Multiple Inputs in Pattern Matching: Explanation and Examples**

In C#, you can create patterns that examine multiple properties or inputs of an object simultaneously. This feature allows you to concisely express complex decision-making logic based on multiple conditions. Multiple input pattern matching works seamlessly with **records** (or classes/structs with deconstruction) and supports **property patterns** and **positional patterns**.

---

### **Example 1: Property Patterns**

### **Record Definition**

```
public record Order(int Items, decimal Cost);

```

This record has two properties:

1.  **Items**: Number of items in the order.
2.  **Cost**: Total cost of the order.

Using property patterns, you can match specific conditions for these properties in a **switch expression**.

### **Calculate Discount Using Property Patterns**

```
public decimal CalculateDiscount(Order order) =>
    order switch
    {
        { Items: > 10, Cost: > 1000.00m } => 0.10m, // High quantity and high cost
        { Items: > 5, Cost: > 500.00m } => 0.05m,  // Moderate quantity and cost
        { Cost: > 250.00m } => 0.02m,              // Moderate cost
        null => throw new ArgumentNullException(nameof(order), "Order cannot be null"), // Null check
        _ => 0m                                    // Default: no discount
    };

// Usage:
var discount1 = CalculateDiscount(new Order(15, 1500.00m)); // Output: 0.10m
var discount2 = CalculateDiscount(new Order(7, 600.00m));   // Output: 0.05m
var discount3 = CalculateDiscount(new Order(3, 300.00m));   // Output: 0.02m
var discount4 = CalculateDiscount(new Order(1, 50.00m));    // Output: 0m

```

---

### **Key Points in Property Patterns**

- **Pattern**: `{ Property: Condition }` checks specific property values.
- **Multiple Conditions**: Combine multiple properties using a comma.
- **Null Handling**: The `null` arm ensures that null references are handled explicitly.
- **Default Case**: `_` matches all remaining cases, ensuring exhaustive handling.

---

### **Example 2: Positional Patterns**

If the **Order** record defines a `Deconstruct` method (automatically provided for records), you can use **positional patterns**. Instead of naming properties, the pattern matches values in the order they are declared in the record.

### **Calculate Discount Using Positional Patterns**

```
public decimal CalculateDiscount(Order order) =>
    order switch
    {
        (> 10, > 1000.00m) => 0.10m, // High quantity and high cost
        (> 5, > 500.00m) => 0.05m,   // Moderate quantity and cost
        (_, > 250.00m) => 0.02m,     // Ignore Items, check Cost
        null => throw new ArgumentNullException(nameof(order), "Order cannot be null"),
        _ => 0m                      // Default: no discount
    };

// Usage:
var discount1 = CalculateDiscount(new Order(15, 1500.00m)); // Output: 0.10m
var discount2 = CalculateDiscount(new Order(7, 600.00m));   // Output: 0.05m
var discount3 = CalculateDiscount(new Order(3, 300.00m));   // Output: 0.02m
var discount4 = CalculateDiscount(new Order(1, 50.00m));    // Output: 0m

```

---

### **Key Points in Positional Patterns**

1.  **Simplifies Syntax**: Matches properties by their order without explicitly naming them.
2.  **Underscore Placeholder (`_`)**:
    - Represents "don't care" values.
    - Useful when some properties are irrelevant to the condition.
3.  **Null Handling**: As with property patterns, a `null` check ensures safe execution.
4.  **Default Case**: The `_` ensures all cases are covered.

---

### **Example 3: Combining Property and Positional Patterns**

You can mix both styles to handle complex scenarios. For instance, if some properties require explicit names while others can be matched positionally:

```
public decimal CalculateDiscount(Order order) =>
    order switch
    {
        { Items: > 10, Cost: > 1000.00m } => 0.10m,
        ( > 5, > 500.00m) => 0.05m, // Positional pattern
        { Cost: > 250.00m } => 0.02m,
        null => throw new ArgumentNullException(nameof(order), "Order cannot be null"),
        _ => 0m
    };

```

---

### **Advantages of Pattern Matching with Multiple Inputs**

1.  **Readability**: Express conditions concisely without verbose `if-else` chains.
2.  **Safety**: The compiler ensures exhaustive case handling and warns about missing cases.
3.  **Flexibility**: Works seamlessly with property-based records and positional patterns.
4.  **Reuse**: Pattern matching can simplify business logic in areas like discount calculations, order processing, or conditional behavior.

By leveraging multiple input patterns in C#, you can write clearer, safer, and more maintainable code.
