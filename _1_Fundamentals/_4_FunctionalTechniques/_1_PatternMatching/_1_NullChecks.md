### **Pattern Matching in C#: Overview and Examples**

**Pattern matching** in C# allows you to test the characteristics of an expression (such as its type, value, or structure) and take specific actions based on the results. It provides concise and readable syntax for conditional logic.

---

### **Key Concepts of Pattern Matching**

1.  **`is` Expression**:
    - Tests the type or characteristics of an expression and, if matched, assigns it to a new variable.
2.  **`switch` Expression**:
    - Evaluates an expression against a series of patterns and executes code for the first matching pattern.
3.  **Rich Vocabulary of Patterns**:
    - **Constant Pattern**: Matches a specific constant value.
    - **Type Pattern**: Matches when the expression is of a specified type.
    - **Declaration Pattern**: Combines a type of check with a variable declaration.
    - **Relational Patterns**: Match values based on relational conditions (`<`, `>`, etc.).
    - **Logical Patterns**: Combine patterns using `and`, `or`, and `not`.

---

### **Example Scenarios**

### **1\. Null Checks**

You can use pattern matching to check if a nullable value type holds a value and convert it to its underlying type.

```
int? maybe = 12;

if (maybe is int number)
{
    Console.WriteLine($"The nullable int 'maybe' has the value {number}");
}
else
{
    Console.WriteLine("The nullable int 'maybe' doesn't hold a value");
}

```

- **Explanation**:
  - The `is int number` pattern checks if `maybe` is of type `int`.
  - If true, `number` is declared and assigned the value of `maybe`.
  - The variable `number` is accessible **only** within the `if` block, ensuring safe usage.

### **2\. Null Reference Check Using `not`**

```
string? message = "Hello, World!";

if (message is not null)
{
    Console.WriteLine(message);
}
else
{
    Console.WriteLine("Message is null.");
}

```

- **Explanation**:
  - The `is not null` pattern ensures that `message` is not null before accessing it.
  - The `not` pattern is a logical pattern that matches when the negated pattern (`null`) does not match.

---

### **3\. Type Pattern with `switch` Expression**

Pattern matching simplifies complex type-based logic using `switch` expressions.

```
object shape = new Circle { Radius = 5 };

string description = shape switch
{
    Circle c => $"Circle with radius {c.Radius}",
    Rectangle r => $"Rectangle with width {r.Width} and height {r.Height}",
    null => "Shape is null",
    _ => "Unknown shape"
};

Console.WriteLine(description);

```

### **Example Classes**

```
public class Circle
{
    public double Radius { get; set; }
}

public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}

```

- **Explanation**:
  - `shape switch` evaluates the type of `shape`.
  - Matches the first pattern that fits:
    - `Circle c` assigns `shape` to a variable `c` of type `Circle`.
    - `Rectangle r` matches `Rectangle` and binds it to `r`.
    - `null` matches if `shape` is null.
    - `_` is a wildcard pattern that matches any remaining cases.

---

### **4\. Relational and Logical Patterns**

You can combine conditions using relational and logical patterns.

```
int age = 25;

string category = age switch
{
    < 13 => "Child",
    >= 13 and < 20 => "Teenager",
    >= 20 and < 60 => "Adult",
    >= 60 => "Senior",
    _ => "Unknown age"
};

Console.WriteLine($"Age category: {category}");

```

- **Explanation**:
  - `< 13` matches ages less than 13.
  - `>= 13 and < 20` uses the logical `and` to combine two relational patterns.
  - `_` serves as a fallback for cases not explicitly handled.

---

### **5\. Using Recursive Patterns**

For complex types, recursive patterns enable deep matching of properties or fields.

```
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

object location = new Point { X = 3, Y = 4 };

string description = location switch
{
    Point { X: 0, Y: 0 } => "Origin",
    Point { X: var x, Y: var y } => $"Point at ({x}, {y})",
    _ => "Unknown location"
};

Console.WriteLine(description);

```

- **Explanation**:
  - `Point { X: 0, Y: 0 }` matches a `Point` object with `X = 0` and `Y = 0`.
  - `Point { X: var x, Y: var y }` binds `X` and `Y` to variables `x` and `y`.

---

### **6\. Combining Patterns**

Patterns can be combined for more expressive logic.

```
object value = 42;

string result = value switch
{
    int i and > 0 => "Positive integer",
    int i and < 0 => "Negative integer",
    string s and not "" => "Non-empty string",
    _ => "Unknown"
};

Console.WriteLine(result);

```

- **Explanation**:
  - `int i and > 0` matches positive integers and assigns the value to `i`.
  - `string s and not ""` matches non-empty strings and assigns the value to `s`.

---

### **Benefits of Pattern Matching**

1.  **Improved Readability**: Clear and concise code for conditional logic.
2.  **Type Safety**: Automatically ensures type checks and conversions.
3.  **Reduced Boilerplate**: Eliminates repetitive casting or `if-else` chains.
4.  **Enhanced Expressiveness**: Supports complex scenarios through patterns like `or`, `and`, `not`, and recursive patterns.

---

### **Summary**

Pattern matching in C# provides powerful tools to simplify conditional logic and improve code readability. With features like `is` expressions, `switch` expressions, logical patterns, and recursive patterns, you can handle complex scenarios concisely and safely.

By leveraging these techniques, developers can write more robust and maintainable code.
