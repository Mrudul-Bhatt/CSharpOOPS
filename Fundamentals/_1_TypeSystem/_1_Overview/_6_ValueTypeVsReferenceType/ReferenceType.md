### **Reference Types**

- **Definition**: Reference types store a *reference* to the data's memory location, not the data itself.
- **Key Characteristics**:
    - Memory is allocated on the **managed heap**.
    - Variables are initially `null` until explicitly assigned.
    - Can support inheritance and polymorphism.
    - Examples: `class`, `record`, `interface`, `delegate`, arrays.

### **Key Differences**

| Feature         | Value Types                       | Reference Types                        |
|-----------------|-----------------------------------|----------------------------------------|
| **Storage**     | Stack memory                      | Managed heap                           |
| **Behavior**    | Copies data directly              | Copies reference to data               |
| **Inheritance** | Cannot inherit from or be derived | Supports inheritance and polymorphism  |
| **Examples**    | `int`, `double`, `struct`, `enum` | `class`, `array`, `record`, `delegate` |

* * * * *

### **Conclusion**

- Use **Value Types** for lightweight, immutable data.
- Use **Reference Types** for complex behavior, large data, or when inheritance is required.