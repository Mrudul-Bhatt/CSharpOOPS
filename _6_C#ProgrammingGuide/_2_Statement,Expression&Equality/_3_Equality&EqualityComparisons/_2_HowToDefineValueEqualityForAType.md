### **Defining Value Equality for a Class or Struct**

When working with C# classes or structs, value equality allows objects to be compared based on their content (fields and properties), not their references in memory. This is particularly useful for objects used in collections, where you want to determine equivalence by values rather than memory location.

---

### **Records and Value Equality**
- **Records** automatically implement value equality, making them ideal for modeling data where equality is determined by field values. 
- Example:
    ```csharp
    record TwoDPoint(int X, int Y);
    ```

For classes or structs, you must explicitly define value equality.

---

### **Implementing Value Equality**
To implement value equality for a class or struct, follow these steps:

1. **Override `Object.Equals`**:
   - Ensure that the method checks for type compatibility and compares relevant fields.

2. **Implement `IEquatable<T>`**:
   - This provides a type-specific `Equals` method to compare the actual fields of the object.

3. **Overload `==` and `!=` operators** (optional but recommended for readability):
   - Define operator overloads to reflect value equality.

4. **Override `Object.GetHashCode`**:
   - Ensure that two objects considered equal produce the same hash code. Use all fields involved in equality comparisons.

5. **Ensure the Five Guarantees of Equivalence**:
   - Reflexive: `x.Equals(x)` is `true`.
   - Symmetric: `x.Equals(y)` gives the same result as `y.Equals(x)`.
   - Transitive: If `x.Equals(y)` and `y.Equals(z)` are `true`, then `x.Equals(z)` is `true`.
   - Consistency: The result of `Equals` doesn’t change unless the objects are modified.
   - Null Handling: Comparing with `null` returns `false` without throwing an exception.

---

### **Class Example: TwoDPoint**
#### Code:
```csharp
class TwoDPoint : IEquatable<TwoDPoint>
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public TwoDPoint(int x, int y)
    {
        if (x is < 1 or > 2000 || y is < 1 or > 2000)
        {
            throw new ArgumentException("Values must be between 1 and 2000.");
        }
        X = x;
        Y = y;
    }

    public override bool Equals(object obj) => Equals(obj as TwoDPoint);

    public bool Equals(TwoDPoint other) =>
        other != null && X == other.X && Y == other.Y;

    public override int GetHashCode() => (X, Y).GetHashCode();

    public static bool operator ==(TwoDPoint lhs, TwoDPoint rhs) =>
        lhs?.Equals(rhs) ?? rhs is null;

    public static bool operator !=(TwoDPoint lhs, TwoDPoint rhs) => !(lhs == rhs);
}
```

#### Explanation:
1. The `Equals` method uses type checking and compares the `X` and `Y` properties.
2. `GetHashCode` computes a hash based on `X` and `Y`.
3. `==` and `!=` operators provide intuitive equality comparison.

---

### **Struct Example: TwoDPoint**
Structs inherently support value equality through reflection. However, defining a custom implementation improves performance.

#### Code:
```csharp
struct TwoDPoint : IEquatable<TwoDPoint>
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public TwoDPoint(int x, int y) : this()
    {
        if (x is < 1 or > 2000 || y is < 1 or > 2000)
        {
            throw new ArgumentException("Values must be between 1 and 2000.");
        }
        X = x;
        Y = y;
    }

    public override bool Equals(object obj) => obj is TwoDPoint other && Equals(other);

    public bool Equals(TwoDPoint other) => X == other.X && Y == other.Y;

    public override int GetHashCode() => (X, Y).GetHashCode();

    public static bool operator ==(TwoDPoint lhs, TwoDPoint rhs) => lhs.Equals(rhs);

    public static bool operator !=(TwoDPoint lhs, TwoDPoint rhs) => !(lhs == rhs);
}
```

---

### **Why Override `Equals` and `GetHashCode`?**
- The default implementation of `Object.Equals` for classes checks reference equality.
- The default `Object.GetHashCode` implementation is based on the object's memory location, unsuitable for value equality.

---

### **Inheritance Caveat**
For inheritance scenarios:
- If `TwoDPoint` is a base class and `ThreeDPoint` extends it, the equality check may ignore `ThreeDPoint`-specific fields when comparing as `TwoDPoint`.

#### Problem:
```csharp
TwoDPoint p1 = new ThreeDPoint(1, 2, 3);
TwoDPoint p2 = new ThreeDPoint(1, 2, 4);
Console.WriteLine(p1.Equals(p2)); // True (ignores Z)
```

#### Solution:
Use **records** for better type-specific equality handling.

---

### **Key Takeaways**
- Value equality ensures objects are compared based on field values, not memory locations.
- Implement `IEquatable<T>` for efficient and type-specific comparisons.
- Override `Object.GetHashCode` to maintain consistency with `Equals`.
- Prefer records for data-centric types, as they offer built-in value equality without boilerplate code.