### **Introduction to Anonymous Types in C#**

Anonymous types provide a concise way to encapsulate data with **read-only properties** into a single object without
explicitly defining a class or struct. They are useful in scenarios like LINQ queries or temporary data grouping where
defining a separate class might be unnecessary.

* * * * *

### **Key Features of Anonymous Types**

1. **Read-only Properties**:
    - Properties are **read-only**; you cannot modify them after initialization.
2. **Compiler-Generated Type**:
    - The compiler creates a unique, internal class for the anonymous type at runtime.
    - The type name is unavailable to the programmer.
3. **Type Inference**:
    - The `var` keyword is used to infer the type of the anonymous type.
4. **Property Type Inference**:
    - The compiler determines property types from the assigned values.
5. **Equality**:
    - Two anonymous objects are considered equal if they have the same property names, types, and values **within the
      same assembly**.

* * * * *

### **Creating an Anonymous Type**

### **Example 1: Simple Anonymous Type**

```
var product = new { Name = "Laptop", Price = 999.99 };

Console.WriteLine($"Product Name: {product.Name}");
Console.WriteLine($"Product Price: ${product.Price}");

```

### **Output**

```
Product Name: Laptop
Product Price: $999.99

```

* * * * *

### **Using Anonymous Types in LINQ Queries**

Anonymous types are often used in LINQ to create objects with only the relevant data.

### **Example 2: LINQ with Anonymous Types**

```
class Product
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
}

class Program
{
    static void Main()
    {
        var products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 1200, Category = "Electronics" },
            new Product { Name = "Table", Price = 150, Category = "Furniture" },
            new Product { Name = "Phone", Price = 800, Category = "Electronics" }
        };

        // Select specific properties using an anonymous type
        var electronics = from product in products
                          where product.Category == "Electronics"
                          select new { product.Name, product.Price };

        foreach (var item in electronics)
        {
            Console.WriteLine($"Name: {item.Name}, Price: ${item.Price}");
        }
    }
}

```

### **Output**

```
Name: Laptop, Price: $1200
Name: Phone, Price: $800

```

* * * * *

### **Nested Anonymous Types**

You can include another object or anonymous type as a property of an anonymous type.

### **Example 3: Nested Anonymous Types**

```
var product = new { Name = "Laptop", Price = 1200 };
var shipment = new { Address = "123 Street", Product = product };

Console.WriteLine($"Shipment Address: {shipment.Address}");
Console.WriteLine($"Product Name: {shipment.Product.Name}");

```

### **Output**

```
Shipment Address: 123 Street
Product Name: Laptop

```

* * * * *

### **Non-Destructive Mutation with `with` Expressions**

C# allows you to create a new anonymous object based on an existing one, with modified properties, using the `with`
expression.

### **Example 4: `with` Expression**

```
var original = new { Item = "Laptop", Price = 1200.00 };
var discounted = original with { Price = 999.99 };

Console.WriteLine($"Original: {original}");
Console.WriteLine($"Discounted: {discounted}");

```

### **Output**

```
Original: { Item = Laptop, Price = 1200 }
Discounted: { Item = Laptop, Price = 999.99 }

```

* * * * *

### **Array of Anonymous Types**

You can create an array of anonymous types, and the compiler ensures the type consistency across the array.

### **Example 5: Anonymous Type Array**

```
var products = new[]
{
    new { Name = "Laptop", Price = 1200 },
    new { Name = "Phone", Price = 800 },
    new { Name = "Tablet", Price = 600 }
};

foreach (var product in products)
{
    Console.WriteLine($"Name: {product.Name}, Price: ${product.Price}");
}

```

### **Output**

```
Name: Laptop, Price: $1200
Name: Phone, Price: $800
Name: Tablet, Price: $600

```

* * * * *

### **Limitations of Anonymous Types**

1. **Internal Accessibility**:
    - Anonymous types are **internal**, so they are not accessible across assemblies.
2. **Cannot Be Passed as Strongly Typed Parameters**:
    - You can only pass them as `object`, which loses type safety.
3. **Immutability**:
    - Properties cannot be modified after initialization.
4. **No Methods or Events**:
    - Only properties are allowed in anonymous types.

* * * * *

### **Comparison with Named Types**

If you need to:

- Pass the data across method boundaries.
- Modify properties.
- Define additional logic.

**Use a named class or struct instead of an anonymous type.**

### **Example: Using a Named Class**

```
class Product
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
}

```

* * * * *

### **Conclusion**

Anonymous types are a convenient way to encapsulate temporary data with minimal boilerplate code. They are particularly
useful in LINQ queries and scenarios where a lightweight, immutable structure is sufficient. However, for more complex
scenarios, named types provide more flexibility and extensibility.