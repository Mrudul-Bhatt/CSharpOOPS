### **Static Constructors in C#**

A **static constructor** is a special constructor used to initialize static members of a class or to perform actions that need to happen only once for the class. Static constructors are called automatically by the runtime before any static members of the class are accessed or any instance of the class is created.

---

### **Key Properties of Static Constructors**

1. **Automatic Invocation**:
   - Static constructors are invoked automatically by the **Common Language Runtime (CLR)**.
   - They are executed only **once per application domain**, even if multiple instances of the class are created.

2. **No Parameters or Access Modifiers**:
   - A static constructor **cannot take parameters**.
   - It does not use access modifiers (e.g., `public`, `private`).

3. **Execution Order**:
   - Static field initializers are executed before the static constructor.
   - The static constructor runs before any instance constructor or any static members are accessed.

4. **Single Execution**:
   - The runtime guarantees that the static constructor is executed **exactly once** for the class.

5. **Implicitly Defined**:
   - If a class defines static fields but no static constructor, the runtime still performs static initialization for those fields.

6. **Error Handling**:
   - If a static constructor throws an exception, it is not re-executed. The class remains uninitialized, and a `TypeInitializationException` is thrown.

---

### **Basic Example: Static Initialization**

```csharp
class SimpleClass
{
    // Static readonly variable initialized by the static constructor
    static readonly long baseline;

    // Static constructor
    static SimpleClass()
    {
        baseline = DateTime.Now.Ticks;
        Console.WriteLine("Static constructor called. Baseline set to: " + baseline);
    }

    public static void DisplayBaseline()
    {
        Console.WriteLine("Baseline value: " + baseline);
    }
}

class TestSimpleClass
{
    static void Main()
    {
        // Accessing a static member triggers the static constructor
        SimpleClass.DisplayBaseline();
        // Output:
        // Static constructor called. Baseline set to: <value>
        // Baseline value: <value>
    }
}
```

---

### **Execution Flow**

1. **Static Field Initialization**:
   - Static fields are initialized to their default values (e.g., `0` for integers, `null` for references).

2. **Static Constructor Execution**:
   - Any static field initializers and the static constructor run sequentially.

3. **Instance Initialization**:
   - Instance constructors run after the static initialization.

---

### **Advanced Example: Singleton Pattern**

A common use case for static constructors is in implementing the **Singleton Pattern**.

```csharp
public class Singleton
{
    // Static field initialized with an instance of Singleton
    private static readonly Singleton instance = new Singleton();

    // Private constructor prevents external instantiation
    private Singleton()
    {
        Console.WriteLine("Instance constructor called.");
    }

    // Static constructor
    static Singleton()
    {
        Console.WriteLine("Static constructor called.");
    }

    // Public property to provide access to the single instance
    public static Singleton Instance => instance;
}

class TestSingleton
{
    static void Main()
    {
        // Access the Singleton instance
        var singleton1 = Singleton.Instance;
        var singleton2 = Singleton.Instance;

        Console.WriteLine(ReferenceEquals(singleton1, singleton2)); // Output: True
    }
}
/* Output:
   Static constructor called.
   Instance constructor called.
   True
*/
```

---

### **Practical Use Case: Static Data Initialization**

```csharp
public class Bus
{
    // Static field to represent the start time of all buses
    protected static readonly DateTime globalStartTime;

    protected int RouteNumber { get; set; }

    // Static constructor to initialize the global start time
    static Bus()
    {
        globalStartTime = DateTime.Now;
        Console.WriteLine($"Static constructor sets global start time to {globalStartTime}");
    }

    // Instance constructor
    public Bus(int routeNum)
    {
        RouteNumber = routeNum;
        Console.WriteLine($"Bus #{RouteNumber} is created.");
    }

    public void Drive()
    {
        TimeSpan elapsedTime = DateTime.Now - globalStartTime;
        Console.WriteLine($"Bus #{RouteNumber} starts its route {elapsedTime.TotalMilliseconds:F2} milliseconds after global start time.");
    }
}

class TestBus
{
    static void Main()
    {
        Bus bus1 = new Bus(71); // Triggers the static constructor
        Bus bus2 = new Bus(72); // Does not trigger the static constructor

        bus1.Drive();
        bus2.Drive();
    }
}
```

**Output:**
```
Static constructor sets global start time to <time>
Bus #71 is created.
Bus #72 is created.
Bus #71 starts its route <elapsed ms> milliseconds after global start time.
Bus #72 starts its route <elapsed ms> milliseconds after global start time.
```

---

### **Static Constructor Behavior**

1. **Order of Execution**:
   - Static fields and initializers run in the order they are declared in the class.
   - The static constructor runs immediately after the field initializers.

2. **Exception Handling**:
   - If the static constructor fails (throws an exception), the class remains unusable, and the runtime throws a `TypeInitializationException`.

---

### **Key Points to Remember**

1. **When It Runs**:
   - A static constructor is invoked automatically:
     - The first time a static member of the class is accessed.
     - Before the first instance of the class is created.

2. **Best Practices**:
   - Use static constructors to initialize **readonly** static fields that require runtime information.
   - Avoid performing long or blocking operations to prevent initialization delays.

3. **Alternatives**:
   - In C# 9 and later, consider using **module initializers** for module-level initialization tasks.

By understanding static constructors, you can efficiently manage static initialization logic and ensure one-time setup for classes in your application.