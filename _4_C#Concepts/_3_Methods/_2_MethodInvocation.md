### **Method Invocation in C#**

Methods in C# can be invoked as **instance methods** or **static methods**, depending on their definition. Here's a detailed explanation of the different invocation patterns, rules, and examples:

* * * * *

### **1\. Instance Methods**

-   **Definition**: Instance methods operate on the data of a specific object instance.

-   **Invocation**: You must first create an object of the class to call its instance method.

-   **Example**:

    ```
    class Calculator
    {
        public int Add(int a, int b) => a + b;
    }

    var calc = new Calculator();  // Create an instance
    int result = calc.Add(3, 5);  // Invoke the instance method

    ```

* * * * *

### **2\. Static Methods**

-   **Definition**: Static methods belong to the class itself and don't operate on instance data.

-   **Invocation**: They are invoked using the class name, not an instance.

-   **Example**:

    ```
    class MathUtils
    {
        public static int Square(int x) => x * x;
    }

    int result = MathUtils.Square(4);  // Invoke static method

    ```

-   **Important**: Attempting to call a static method via an instance (e.g., `new MathUtils().Square(4)`) will generate a **compiler error**.

* * * * *

### **3\. Passing Arguments**

When calling a method, the arguments provided must match the parameter list in the method signature.

### **a. Positional Arguments**

-   Arguments are passed in the same order as defined in the method's parameter list.

-   Example:

    ```
    int travelTime = moto.Drive(170, 60);  // Order: miles, speed

    ```

### **b. Named Arguments**

-   Each argument is prefixed with the parameter name and a colon (`:`), and arguments can appear in any order.

-   Example:

    ```
    int travelTime = moto.Drive(speed: 60, miles: 170);  // Reverse order

    ```

### **c. Mixed Arguments**

-   Positional arguments must precede named arguments, and named arguments must match their parameter names.

-   Example:

    ```
    int travelTime = moto.Drive(170, speed: 55);  // Positional (miles) + named (speed)

    ```

* * * * *

### **4\. Method Parameters vs. Arguments**

-   **Parameters**: Defined in the method signature and specify the expected input types.
-   **Arguments**: Actual values passed to the method during invocation.

**Example**:

```
static int Square(int i) => i * i;  // `i` is the parameter
int result = Square(4);            // `4` is the argument

```

* * * * *

### **5\. Examples of Invocation**

### **Calling with Variables**

```
int num = 4;
int result = Square(num);  // Pass a variable

```

### **Calling with Literals**

```
int result = Square(12);  // Pass a literal

```

### **Calling with Expressions**

```
int result = Square(num * 3);  // Pass an expression

```

* * * * *

### **6\. Overloading and Invocation**

When methods are overloaded, the method to invoke is determined by the **number, type, and order** of arguments.

-   Example:

    ```
    class Calculator
    {
        public int Add(int a, int b) => a + b;             // Method 1
        public double Add(double a, double b) => a + b;   // Method 2
    }

    var calc = new Calculator();
    int sumInt = calc.Add(3, 5);          // Calls Method 1
    double sumDouble = calc.Add(3.0, 5.0); // Calls Method 2

    ```

* * * * *

### **7\. Example: `TestMotorcycle` Class**

**Positional Arguments**:

```
_ = moto.Drive(5, 20);  // miles: 5, speed: 20

```

**Named Arguments**:

```
int travelTime = moto.Drive(miles: 170, speed: 60);  // miles and speed explicitly named

```

**Mixed Arguments**:

```
int travelTime = moto.Drive(170, speed: 55);  // miles (positional) + speed (named)

```

* * * * *

### **8\. Special Considerations**

-   **Order**: Named arguments allow flexibility, but if mixed, positional arguments must come first.

-   **Default Parameters**: When parameters have default values, you can omit them in the call:

    ```
    void PrintMessage(string message = "Hello") => Console.WriteLine(message);
    PrintMessage();  // Outputs: Hello
    PrintMessage("Hi!");  // Outputs: Hi!

    ```

* * * * *

### **Summary**

-   **Instance methods** require an object for invocation; **static methods** are called on the class itself.
-   Use **positional arguments** for simplicity or **named arguments** for clarity and order flexibility.
-   Mixed arguments are allowed but must follow the rules of order.
-   Overloaded methods resolve based on the arguments provided. Understanding these invocation patterns ensures robust and error-free method calls.