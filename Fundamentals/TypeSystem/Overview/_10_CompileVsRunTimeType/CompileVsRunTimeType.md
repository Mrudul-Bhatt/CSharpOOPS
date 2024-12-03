### **Compile-Time Type vs. Run-Time Type**

In C#, every variable has:

1. **Compile-Time Type**:

    - The type that is explicitly declared (or inferred) in the source code.
    - This type is used by the compiler to check for syntax, type-safety, method calls, and overload resolution.
    - It determines what operations are allowed at compile time.
2. **Run-Time Type**:

    - The actual type of the instance referred to by the variable when the program is executed.
    - This type is determined during execution and controls behaviors like virtual method resolution and type casting.
    - It may differ from the compile-time type when polymorphism or type inheritance is involved.

### **Key Takeaways**

- **Compile-time type** defines what you can do at compile time (method calls, type checks).
- **Run-time type** defines the actual behavior of the object at runtime (method dispatch, type checking).
- When using polymorphism, inheritance, or type interfaces, be mindful of the differences between these types.