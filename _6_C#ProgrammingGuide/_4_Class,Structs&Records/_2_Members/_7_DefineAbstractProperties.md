### Explanation: Abstract Properties in C#

#### What are Abstract Properties?
- Abstract properties are **properties** in an abstract class that do not have an implementation.
- They declare that a derived class must implement the property.
- Declared using the `abstract` modifier.

#### Key Features:
1. **Declaration**:
   - Abstract properties can only exist in **abstract classes**.
   - The `abstract` keyword is placed in the property declaration.
   - Abstract properties cannot have implementation in the base class; only the derived class provides the implementation.

2. **Accessors**:
   - Abstract properties define whether the property will have `get`, `set`, or both accessors.
   - In the base class, accessors are declared without a body.
   - Example:
     ```csharp
     public abstract double Area { get; }
     ```

3. **Derived Classes**:
   - Any class inheriting from an abstract class must **override** its abstract properties.
   - The `override` keyword is used to implement the property.

---

### Example Walkthrough

#### File 1: `abstractshape.cs`
This file defines an abstract base class, `Shape`, with the following features:
- **Abstract Property**: 
  - `Area` is a read-only abstract property (`get` accessor only).
  - Its implementation is left to derived classes.
- **Concrete Property**:
  - `Id` is a concrete property with both `get` and `set` accessors.
- **Constructor**:
  - Initializes the `Id` property.
- **Method**:
  - `ToString` returns a string representation of the shape, including its `Id` and `Area`.

```csharp
public abstract class Shape
{
    private string name;

    public Shape(string s)
    {
        Id = s;
    }

    public string Id
    {
        get { return name; }
        set { name = value; }
    }

    public abstract double Area { get; }

    public override string ToString()
    {
        return $"{Id} Area = {Area:F2}";
    }
}
```

---

#### File 2: `shapes.cs`
This file defines three classes (`Square`, `Circle`, `Rectangle`) that inherit from `Shape` and implement the `Area` property.

**Implementation Highlights**:
1. **Square**:
   - Constructor takes `side` and `id` as parameters.
   - Overrides `Area` to calculate the area of a square.
     ```csharp
     public override double Area
     {
         get { return side * side; }
     }
     ```

2. **Circle**:
   - Constructor takes `radius` and `id`.
   - Overrides `Area` to calculate the area of a circle:
     ```csharp
     public override double Area
     {
         get { return radius * radius * System.Math.PI; }
     }
     ```

3. **Rectangle**:
   - Constructor takes `width`, `height`, and `id`.
   - Overrides `Area` to calculate the area of a rectangle:
     ```csharp
     public override double Area
     {
         get { return width * height; }
     }
     ```

---

#### File 3: `shapetest.cs`
This file contains a test program that:
1. Creates an array of `Shape` objects (`Square`, `Circle`, `Rectangle`).
2. Iterates through the array and prints each shape’s details using the overridden `ToString` method from `Shape`.

```csharp
class TestClass
{
    static void Main()
    {
        Shape[] shapes =
        {
            new Square(5, "Square #1"),
            new Circle(3, "Circle #1"),
            new Rectangle(4, 5, "Rectangle #1")
        };

        System.Console.WriteLine("Shapes Collection");
        foreach (Shape s in shapes)
        {
            System.Console.WriteLine(s);
        }
    }
}
```

---

### Compilation and Output

1. Compile the files:
   ```bash
   csc -target:library abstractshape.cs
   csc -target:library -reference:abstractshape.dll shapes.cs
   csc -reference:abstractshape.dll;shapes.dll shapetest.cs
   ```

2. Run the `shapetest.exe`:
   ```bash
   shapetest.exe
   ```

**Output**:
```
Shapes Collection
Square #1 Area = 25.00
Circle #1 Area = 28.27
Rectangle #1 Area = 20.00
```

---

### Summary of Key Concepts:
1. **Abstract Classes**:
   - Used to define a base class with common functionality and abstract members that must be implemented by derived classes.

2. **Abstract Properties**:
   - Declared in abstract classes using the `abstract` keyword.
   - No implementation in the base class; derived classes provide the implementation.

3. **Polymorphism**:
   - The derived classes override the abstract property, enabling polymorphic behavior when using base class references.