### **Value Types and Reference Types**

C# has two fundamental types: **Value Types** and **Reference Types**. Both are essential in understanding data storage
and behavior in .NET.

* * * * *

### **Value Types**

- **Definition**: Value types derive from `System.ValueType`, which derives from `System.Object`. They store data
  directly in memory.
- **Key Characteristics**:
    - Memory is allocated *inline* where the variable is declared (stack memory).
    - No heap allocation or garbage collection overhead.
    - **Immutable**: They cannot be derived from.
    - Examples: `int`, `double`, `struct`, `enum`.