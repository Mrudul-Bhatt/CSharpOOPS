### **Example: Mixed Query and Method Syntax**

In LINQ, **query syntax** and **method syntax** can be combined within the same expression. You can use method syntax on
the results of a query expression by enclosing the query in parentheses and then applying the dot operator to call a
method.

* * * * *

### **1\. Using Mixed Syntax**

In this example, a **query expression** filters the numbers, and the `Count()` method is used to get the number of
matching elements.

### Example Query #7:

```
List<int> numbers1 = new List<int> { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

// Using a query expression with method syntax
var numCount1 = (
    from num in numbers1
    where num > 3 && num < 7
    select num
).Count();

Console.WriteLine(numCount1); // Output: 2

```

* * * * *

### **2\. Using a Separate Variable**

Separating the query and the method call improves clarity and reduces potential confusion. The query is stored in one
variable, and the method call operates on the query's results.

### Example with Separate Variables:

```
IEnumerable<int> numbersQuery =
    from num in numbers1
    where num > 3 && num < 7
    select num;

var numCount2 = numbersQuery.Count();

Console.WriteLine(numCount2); // Output: 2

```

This approach makes it easier to reuse the query elsewhere or apply other methods without re-writing the filtering
logic.

* * * * *

### **3\. Using Method Syntax Only**

The equivalent operation can also be written purely in method syntax. Here, a lambda expression filters the numbers, and
`Count()` is directly applied.

### Example with Method Syntax:

```
var numCount = numbers1.Count(n => n > 3 && n < 7);

Console.WriteLine(numCount); // Output: 2

```

This approach is concise but less readable for complex operations.

* * * * *

### **4\. Using Explicit Typing**

If you want to explicitly specify the return type of the method call (e.g., `int`), you can do so:

### Example with Explicit Typing:

```
int numCount = numbers1.Count(n => n > 3 && n < 7);

Console.WriteLine(numCount); // Output: 2

```

Explicit typing can be useful when the type is not obvious or when you want to ensure type safety.

* * * * *

### **Key Takeaways**

1. **Mixed Syntax:** Combine query expressions and method calls for flexibility.
    - Enclose the query expression in parentheses before applying a method.
    - Example: `(from num in numbers where ... select num).Count()`.
2. **Separate Variables:** For clarity, store the query in one variable and apply methods on it later.
3. **Immediate Execution:** Methods like `Count`, `Sum`, and `Average` **execute immediately** because they return a
   single value, not a collection.
4. **Method Syntax:** Pure method syntax is concise and uses lambda expressions but may be harder to read for complex
   queries.
5. **Var vs. Explicit Typing:**
    - `var` is convenient for inferred types.
    - Explicit typing ensures clarity and safety for return types.