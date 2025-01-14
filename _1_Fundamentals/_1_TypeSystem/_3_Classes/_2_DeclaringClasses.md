### **Declaring Classes in C#**

A **class** in C# is a blueprint for creating objects. It defines the **data** (fields and properties) and the **behavior** (methods and events) that the objects based on the class will have. Here's a detailed explanation of the syntax and structure of class declarations in C#, followed by examples.

* * * * *

### **Syntax**

```
[access modifier] class [identifier]
{
    // Fields, properties, methods, and events go here
}

```

### Key Components:

1.  **Access Modifier**:
    -   Specifies the visibility of the class.
    -   Common access modifiers include:
        -   `public`: The class is accessible from any other class.
        -   `internal`: The class is accessible only within the same assembly (default for classes).
        -   `private`: The class is accessible only within its containing type (rare for top-level classes).
        -   `protected`: Not valid for top-level classes but valid for nested ones.
2.  **`class` Keyword**:
    -   This keyword is used to define a class.
3.  **Identifier**:
    -   The name of the class. It must be a valid C# identifier (e.g., no spaces, cannot start with a number, etc.).
4.  **Class Body**:
    -   Encased in curly braces `{ }`, it contains the class **members**, such as:
        -   Fields
        -   Properties
        -   Methods
        -   Events

* * * * *

### **Example 1: Declaring a Simple Class**

```
public class Customer
{
    // Field
    private string name;

    // Property
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    // Method
    public void DisplayInfo()
    {
        Console.WriteLine($"Customer Name: {name}");
    }
}

```

### Explanation:

1.  **Access Modifier**:
    -   The `public` keyword allows the class to be accessed from anywhere.
2.  **Field**:
    -   A private variable `name` is declared to hold data.
3.  **Property**:
    -   `Name` is a public property that provides controlled access to the private field `name`.
4.  **Method**:
    -   `DisplayInfo` is a public method that prints the customer's name.

* * * * *

### **Example 2: Internal Class (Default Modifier)**

```
class Order
{
    public int OrderID { get; set; }
    public string ProductName { get; set; }
}

```

### Explanation:

1.  **Access Modifier**:
    -   The `Order` class does not specify an access modifier, so it is `internal` by default. It is accessible only within the same assembly.

* * * * *

### **Class Members: Overview**

| **Member** | **Description** | **Example** |
| --- | --- | --- |
| **Field** | Stores data in a class (usually private). | `private int age;` |
| **Property** | Encapsulates fields with get/set accessors. | `public int Age { get; set; }` |
| **Method** | Encapsulates behavior of the class. | `public void Greet() { ... }` |
| **Constructor** | Initializes an object when it is created. | `public Customer() { ... }` |
| **Event** | Provides a way to handle notifications or changes. | `public event Action OnOrder;` |

* * * * *

### **Example 3: Class with Constructor**

A **constructor** is a special method used to initialize objects when they are created.

```
public class Product
{
    // Fields
    private string name;
    private double price;

    // Constructor
    public Product(string name, double price)
    {
        this.name = name;
        this.price = price;
    }

    // Method
    public void ShowDetails()
    {
        Console.WriteLine($"Product: {name}, Price: ${price}");
    }
}

```

### Usage:

```
class Program
{
    static void Main()
    {
        Product product = new Product("Laptop", 999.99); // Constructor is called
        product.ShowDetails();
    }
}

```

* * * * *

### **Example 4: Nested Classes**

Classes can also be nested inside other classes.

```
public class OuterClass
{
    public class InnerClass
    {
        public void Display()
        {
            Console.WriteLine("I am a nested class.");
        }
    }
}

```

### Usage:

```
class Program
{
    static void Main()
    {
        OuterClass.InnerClass nested = new OuterClass.InnerClass();
        nested.Display();
    }
}

```

* * * * *

### **Access Modifiers Recap**

| Modifier | Accessibility |
| --- | --- |
| `public` | Accessible from any code. |
| `internal` | Accessible only within the same assembly (default for classes). |
| `private` | Accessible only within the containing class or struct (used for members). |
| `protected` | Accessible within the containing class and derived classes (not for top-level classes). |

* * * * *

### **Key Takeaways**

1.  Classes serve as templates for objects.
2.  Access modifiers control the visibility and usability of classes.
3.  Class members define the data and behavior associated with the class.
4.  Use constructors for object initialization and methods to encapsulate functionality.