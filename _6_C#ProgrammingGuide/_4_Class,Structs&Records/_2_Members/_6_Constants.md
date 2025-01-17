### Explanation: **Constants in C#**

#### What are Constants?
- **Definition**: Constants are immutable values known at **compile time** and cannot change during the lifetime of the program.
- Declared using the `const` keyword.
- **Examples**: `int`, `float`, `double`, `string`, etc.
- Once defined, their values are substituted directly into the code at compile time.

---

#### Key Features:
1. **Immutable and Compile-Time Known**:
   - Constants must have values that are determined at compile time.
   - Example:
     ```csharp
     public const int MaxValue = 100; // Value is fixed at compile time.
     ```

2. **Cannot Be Changed**:
   - Once defined, the value cannot be modified, even by the class itself.
   - The compiler substitutes constants directly into the IL code, so there’s no variable memory address.

3. **Types Supported**:
   - Only C# **built-in types** (e.g., `int`, `float`, `bool`, `string`) can be `const`.
   - **Reference types** like classes, arrays, or user-defined types cannot be `const`. 
     - Instead, use the `readonly` modifier for such cases.

4. **Access Modifiers**:
   - Constants can have access modifiers (`public`, `private`, `protected`, etc.) to define their scope.
     ```csharp
     public const int DaysInWeek = 7;
     private const string DefaultName = "User";
     ```

5. **Cannot Be Passed by Reference**:
   - Since constants do not have memory addresses, they cannot be passed by reference.

6. **Circular References Are Not Allowed**:
   - You cannot initialize a constant with another constant if it creates a circular dependency.

---

#### Examples:

**Basic Declaration:**
```csharp
class Calendar
{
    public const int Months = 12;
}
```
- `Months` is a constant with a fixed value `12`.
- Accessed as `Calendar.Months`.

**Multiple Constants in One Declaration:**
```csharp
class Calendar
{
    public const int Months = 12, Weeks = 52, Days = 365;
}
```

**Constants Using Other Constants:**
```csharp
class Calendar
{
    public const int Days = 365;
    public const int Weeks = 52;
    public const double DaysPerWeek = (double)Days / Weeks;
}
```

---

#### Why Not Use Constants in DLLs?
- If you reference a constant from a **DLL** and the constant’s value changes in the DLL, your program still uses the old value until it is recompiled.
- **Best Practice**: Avoid hardcoding constants from external libraries; use configuration files or other mechanisms for dynamic values.

---

#### Constants vs. Readonly:
| Feature                 | **const**                          | **readonly**                           |
|-------------------------|-------------------------------------|----------------------------------------|
| **When Initialized**    | Compile-time                       | Runtime (e.g., in a constructor)       |
| **Allowed Types**       | Only built-in types and `string`   | Any type (e.g., arrays, objects)       |
| **Modification**        | Fixed after declaration            | Cannot be changed after initialization |
| **Memory Address**      | No memory address                  | Has memory address                     |

**Example (readonly):**
```csharp
class Configuration
{
    public readonly int RunTimeValue;

    public Configuration(int value)
    {
        RunTimeValue = value; // Initialized at runtime.
    }
}
```

---

#### Enum for Named Constants:
- **Enum** allows creating named constants for integral types (`int`, `long`, etc.):
```csharp
enum DaysOfWeek
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
DaysOfWeek today = DaysOfWeek.Friday;
```

---

#### Summary:
- Use `const` for immutable values that are fixed at compile time.
- Use `readonly` for values initialized at runtime but not changed afterward.
- Be cautious with constants from external libraries to avoid mismatched values.