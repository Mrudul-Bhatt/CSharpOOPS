### **Defining Constants in C#**

#### **What Are Constants?**
- Constants are **immutable fields** whose values are set at compile time and cannot be changed afterward.
- Used to define **fixed values** that remain the same throughout the program, avoiding "magic numbers" (hardcoded numeric values) in the code.
- Declared using the `const` keyword.

---

### **Key Features of Constants**
1. **Set at Compile Time**:
   - Constant values are determined when the program is compiled and cannot change at runtime.
   - Example:
     ```csharp
     public const int DaysInWeek = 7;
     ```

2. **Supported Types**:
   - Constants support only **C# built-in types** (`int`, `double`, `string`, etc.).
   - Reference types, user-defined types, and runtime-initialized values cannot be constants.

3. **Scoped Accessibility**:
   - Can be declared with access modifiers (`public`, `private`, `internal`, etc.) to control visibility.
   - Example:
     ```csharp
     public const double Pi = 3.14159;
     private const string DefaultUser = "Admin";
     ```

4. **Usage with Class Name**:
   - Constants are **static by nature** and associated with the class, not an instance.
   - Access them using the class name as a qualifier:
     ```csharp
     double area = Constants.Pi * (radius * radius);
     ```

5. **Cannot Use `#define` for Constants**:
   - Unlike in C and C++, C# does not allow the `#define` directive to create constants.

---

### **Organizing Constants in C#**

#### **Using Enumerations for Integral Constants**
For constants of integral types (`int`, `byte`, etc.), use an `enum` for better readability:
```csharp
enum Days
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}
```
- Usage:
  ```csharp
  Days today = Days.Monday;
  Console.WriteLine(today); // Output: Monday
  ```

#### **Using a Static Class for Non-Integral Constants**
For non-integral constants (e.g., `double`, `string`), group them in a **static class**:
```csharp
static class Constants
{
    public const double Pi = 3.14159;
    public const int SpeedOfLight = 300000; // km/s
    public const string AppName = "My Application";
}
```
- Advantages:
  - Logical grouping of constants.
  - Readability: Access constants using the class name as a qualifier (`Constants.Pi`).

---

### **Example: Using Constants**

#### **Code Example:**
```csharp
using System;

static class Constants
{
    public const double Pi = 3.14159;
    public const int SpeedOfLight = 300000; // km/s
    public const string WelcomeMessage = "Welcome to C# Programming!";
}

class Program
{
    static void Main()
    {
        double radius = 5.3;
        double area = Constants.Pi * (radius * radius); // Circle area calculation
        int timeFromSun = 149476000 / Constants.SpeedOfLight; // Time in seconds

        Console.WriteLine("Circle Area: " + area);
        Console.WriteLine("Time from Sun to Earth: " + timeFromSun + " seconds");
        Console.WriteLine(Constants.WelcomeMessage);
    }
}
```

#### **Output**:
```
Circle Area: 88.247629
Time from Sun to Earth: 498 seconds
Welcome to C# Programming!
```

---

### **Summary**
1. **Define Constants with `const`**:
   - Use `const` for values known at compile time.
   - Group related constants in a static class for clarity.
   
2. **Use Enumerations for Integral Constants**:
   - Define named constants for integers for better readability and code maintenance.

3. **Access Constants via Class Name**:
   - Prefix constant names with the containing class for clarity (`Constants.Pi`).

4. **Avoid `#define` for Constants**:
   - In C#, the `#define` directive cannot define constants as in C/C++. Use `const` or `readonly` instead.