### **Raising Base Class Events in Derived Classes in C#**

In C#, events are a type of delegate that allow objects (event publishers) to notify other objects (subscribers) when something of interest occurs. However, a key rule in C# is that **events can only be raised from within the class that declares them**. This means that if a base class defines an event, a derived class cannot directly raise the event. To allow derived classes to raise base class events, a **protected method** can be provided in the base class to invoke the event. 

This pattern is commonly used in applications like Windows Forms, where controls (base class) may have events that need to be raised by derived controls.

---

### **Pattern for Raising Base Class Events in Derived Classes**

The solution involves defining a **protected** method in the base class that wraps the event invocation. Derived classes can then call this method, either directly or via overriding it, to raise the event.

### **Steps and Example**

Consider the following example where we have an abstract `Shape` class with an event, and two derived classes (`Circle` and `Rectangle`). Both derived classes raise the event when their properties change.

#### 1. **Base Class with Event Declaration**

In the base class (`Shape`), we declare an event and a protected method (`OnShapeChanged`) that raises the event:

```csharp
public abstract class Shape
{
    protected double _area;

    public double Area
    {
        get => _area;
        set => _area = value;
    }

    // Declare the event
    public event EventHandler<ShapeEventArgs> ShapeChanged;

    public abstract void Draw();

    // Protected method to safely raise the event
    protected virtual void OnShapeChanged(ShapeEventArgs e)
    {
        ShapeChanged?.Invoke(this, e);  // Raise the event for all subscribers
    }
}
```

- The event `ShapeChanged` is declared using the generic `EventHandler<T>` delegate type, which simplifies the event declaration by automatically using the `EventArgs` class (or derived class).
- The `OnShapeChanged` method raises the event when it's called.

#### 2. **Derived Classes that Raise the Event**

The derived classes (`Circle` and `Rectangle`) override the `OnShapeChanged` method to raise the event whenever their properties are updated. 

- In the `Circle` class, when the radius changes, the `Update` method is called, which recalculates the area and raises the `ShapeChanged` event.

```csharp
public class Circle : Shape
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
        _area = 3.14 * _radius * _radius;
    }

    public void Update(double d)
    {
        _radius = d;
        _area = 3.14 * _radius * _radius;
        OnShapeChanged(new ShapeEventArgs(_area));  // Raise the event
    }

    protected override void OnShapeChanged(ShapeEventArgs e)
    {
        // Additional circle-specific processing can go here

        // Call the base class method to raise the event
        base.OnShapeChanged(e);
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing a circle");
    }
}
```

- In the `Rectangle` class, the process is similar: it updates its area and raises the `ShapeChanged` event.

```csharp
public class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(double length, double width)
    {
        _length = length;
        _width = width;
        _area = _length * _width;
    }

    public void Update(double length, double width)
    {
        _length = length;
        _width = width;
        _area = _length * _width;
        OnShapeChanged(new ShapeEventArgs(_area));  // Raise the event
    }

    protected override void OnShapeChanged(ShapeEventArgs e)
    {
        // Additional rectangle-specific processing can go here

        // Call the base class method to raise the event
        base.OnShapeChanged(e);
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle");
    }
}
```

#### 3. **Event Subscriber (ShapeContainer)**

The `ShapeContainer` class subscribes to the `ShapeChanged` event and performs actions when the event is raised. For example, it can redraw the shape:

```csharp
public class ShapeContainer
{
    private readonly List<Shape> _list;

    public ShapeContainer()
    {
        _list = new List<Shape>();
    }

    public void AddShape(Shape shape)
    {
        _list.Add(shape);

        // Subscribe to the event in the base class
        shape.ShapeChanged += HandleShapeChanged;
    }

    private void HandleShapeChanged(object sender, ShapeEventArgs e)
    {
        if (sender is Shape shape)
        {
            Console.WriteLine($"Received event. Shape area is now {e.NewArea}");
            shape.Draw();  // Redraw the shape
        }
    }
}
```

- When the `ShapeChanged` event is raised in any of the shapes (`Circle` or `Rectangle`), the `HandleShapeChanged` method in `ShapeContainer` will be called, which will display the new area and redraw the shape.

#### 4. **Example Usage (Test)**

In the `Test` class, we create instances of `Circle` and `Rectangle`, subscribe to their events, and update their properties to raise events.

```csharp
class Test
{
    static void Main()
    {
        var circle = new Circle(54);
        var rectangle = new Rectangle(12, 9);
        var container = new ShapeContainer();

        container.AddShape(circle);
        container.AddShape(rectangle);

        // Update shapes to raise events
        circle.Update(57);
        rectangle.Update(7, 7);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
```

**Output**:
```
Received event. Shape area is now 10201.86
Drawing a circle
Received event. Shape area is now 49
Drawing a rectangle
```

---

### **Key Points**

- **Event Declaration**: Events are declared in the base class using the `event` keyword and are typically handled by a delegate type like `EventHandler<T>`.
- **Raising Events in Derived Classes**: Derived classes cannot directly raise base class events. Instead, a **protected method** (`OnShapeChanged`) is provided in the base class to invoke the event.
- **Event Invoking**: Derived classes override this method (or call it directly) to raise the event in a safe manner.
- **Event Handlers**: The event can have multiple subscribers, and when the event is raised, all subscribed handlers are invoked.
