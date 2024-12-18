### **Explicit and Implicit Typing of Query Variables**

When declaring a LINQ query variable, you have two options:

1. **Explicit Typing**: Declare the query variable with its specific type explicitly.
2. **Implicit Typing**: Use the `var` keyword, allowing the compiler to infer the variable type at compile time.

Both approaches produce the same query results. The choice between them often depends on readability, clarity, and your
personal or team coding style preferences.

* * * * *

### **Explicit Typing**

With explicit typing, you define the exact type of the query variable. This approach is beneficial when you want to make
the type clear to other developers or when working with complex types.

**Example:**

```
IEnumerable<City> queryCities =
    from city in cities
    where city.Population > 100_000
    select city;

```

Here, the type of `queryCities` is explicitly declared as `IEnumerable<City>`.

* * * * *

### **Implicit Typing**

With implicit typing, you use the `var` keyword to let the compiler infer the type of the query variable. The compiler
determines the type based on the return type of the query.

**Example:**

```
var queryCities =
    from city in cities
    where city.Population > 100_000
    select city;

```

In this case, the type of `queryCities` is still `IEnumerable<City>`, but the `var` keyword hides the specific type in
the declaration.

* * * * *

### **When to Use Each**

| **Aspect**        | **Explicit Typing**                                                            | **Implicit Typing (var)**                    |
|-------------------|--------------------------------------------------------------------------------|----------------------------------------------|
| **Readability**   | Makes the type clear, improving readability for complex queries.               | Reduces verbosity for straightforward types. |
| **Simplicity**    | Requires writing the type explicitly, which can be verbose.                    | Cleaner and shorter syntax.                  |
| **Flexibility**   | Useful when you want to clarify the exact type for debugging or documentation. | Lets the compiler handle type inference.     |
| **Best Use Case** | When the type is non-obvious or for educational purposes.                      | When the type is straightforward or obvious. |

* * * * *

### **Example: Comparison**

**Explicit Typing:**

```
IEnumerable<string> cityDescriptions =
    from city in cities
    where city.Population > 100_000
    select $"{city.Name} has a population of {city.Population}.";

foreach (string description in cityDescriptions)
{
    Console.WriteLine(description);
}

```

**Implicit Typing:**

```
var cityDescriptions =
    from city in cities
    where city.Population > 100_000
    select $"{city.Name} has a population of {city.Population}.";

foreach (var description in cityDescriptions)
{
    Console.WriteLine(description);
}

```

In both cases, the result and functionality are identical. The only difference is in how the query variable and the
iteration variable are typed.

* * * * *

### **Key Considerations**

- Use **explicit typing** if:
    - The type isn't immediately obvious.
    - You're working in a team where explicit types improve clarity.
    - You want to emphasize the type for educational or documentation purposes.
- Use **implicit typing (`var`)** if:
    - The type is evident from the context.
    - You want to reduce boilerplate code for simpler queries.
    - You're following a concise coding style.

* * * * *

### **Conclusion**

Both explicit and implicit typing have their use cases in LINQ queries. The choice depends on your coding style, the
complexity of the query, and whether type clarity is necessary for the reader. In most cases, implicit typing with `var`
is sufficient and preferred for its brevity, but explicit typing can be valuable for readability in complex scenarios.