### **Types of Literal Values in C#**

In C#, literal values represent fixed data and are automatically assigned a type by the compiler. Literal values can
also be explicitly typed using suffixes for numeric types. All literals ultimately derive from `System.Object`.

* * * * *

### **Key Points**

1. **Default Type Assignment**:

    - For **integer literals** (e.g., `123`), the default type is `int`.
    - For **floating-point literals** (e.g., `3.14`), the default type is `double`.
2. **Type Suffixes**:

    - You can explicitly define the type of a numeric literal by appending a suffix:
        - `f` or `F`: Specifies the value as `float`.
        - `d` or `D`: Specifies the value as `double`.
        - `m` or `M`: Specifies the value as `decimal`.
        - `l` or `L`: Specifies the value as `long`.
        - `u` or `U`: Specifies the value as `uint`.
3. **Type Inference and Methods**:

    - Because literals are typed, you can call methods on them or check their type using `.GetType()`.

* * * * *

### **Conclusion**

- Literal values in C# are strongly typed by default, and the type can be explicitly defined using suffixes.
- You can use methods on literal values because they derive from `System.Object`.
- Using the correct type suffix ensures clarity and avoids type mismatch errors during operations.