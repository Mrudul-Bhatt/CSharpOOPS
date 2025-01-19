### **Constructors in C#**

A constructor is a special method that is called automatically when an instance of a class or struct is created. Its primary purpose is to initialize the new instance, ensuring its fields or properties are valid.

---

### **Key Features of Constructors**
1. **Same Name as the Class**:
   - A constructor must have the same name as the class or struct.
   - It does not have a return type, not even `void`.

2. **Multiple Constructors**:
   - A class or struct can have multiple constructors with different parameter lists (constructor overloading).

3. **Automatic Invocation**:
   - Constructors are invoked when an instance is created using the `new` keyword.

4. **Types of Constructors**:
   - **Instance Constructors**: Initialize instance members of a class or struct.
   - **Static Constructors**: Initialize static members of a class or struct.

---

### **Initialization Process**
When a new object is created, the following steps occur in order:

1. **Instance Fields Set to Default**:
   - Numeric types are set to `0`.
   - Reference types are set to `null`.

2. **Field Initializers Execute**:
   - Initializers for instance fields in the most derived type run.

3. **Base Class Field Initializers Execute**:
   - Field initializers in base classes execute in order from `System.Object` up to the immediate base class.

4. **Base Instance Constructors Execute**:
   - Base class constructors run in the inheritance hierarchy, starting from `System.Object`.

5. **Derived Class Constructor Executes**:
   - The constructor for the derived class executes.

6. **Object Initializers Execute**:
   - If the `new` expression includes object initializers, they run after the instance constructor.

---

### **Instance Constructor Syntax**
An instance constructor initializes a new object of a class. Example:

```csharp
public class Person
{
    private string lastName;
    private string firstName;

    // Instance constructor
    public Person(string lastName, string firstName)
    {
        this.lastName = lastName;
        this.firstName = firstName;
    }
}
```

#### **Expression-Bodied Constructors**
If the constructor can be implemented as a single statement, an expression-bodied member can be used:

```csharp
public class Location
{
    private string locationName;

    public Location(string name) => locationName = name;
}
```

---

### **Static Constructors**
A static constructor initializes static members of a class. 

#### **Features**:
- Static constructors are parameterless.
- They are called **once**, before any instance of the class is created or any static member is accessed.
- They cannot be called explicitly; the runtime invokes them.

#### **Example**:
```csharp
public class Adult
{
    private static int minimumAge;

    // Static constructor
    static Adult() => minimumAge = 18;

    public Adult(string name) { }
}
```

---

### **Primary Constructors** (Introduced in C# 12)
A primary constructor simplifies the declaration of required parameters for a class or struct. Parameters are declared directly in the class definition and are used to initialize properties or fields.

#### **Example**:
```csharp
public class LabelledContainer<T>(string label)
{
    public string Label { get; } = label;
    public required T Contents { get; init; }
}
```

---

### **Object Initialization**
Object initializers are used to set properties or fields after the constructor has run:

```csharp
var person = new Person("Doe", "John")
{
    Age = 30
};
```

---

### **Key Points**
1. **Default Constructor**:
   - If no constructor is defined, the compiler provides a parameterless constructor.

2. **Constructor Overloading**:
   - Multiple constructors with different parameter sets can be defined to support various initialization needs.

3. **Static Constructor Restrictions**:
   - No parameters.
   - Cannot be overloaded.

4. **Base Constructor Calls**:
   - A derived class constructor must call a base class constructor using `base()`.

---

### **Best Practices**
- Use constructors to enforce required initialization.
- For complex initialization, prefer factory methods.
- Avoid long parameter lists; consider using object initializers or builder patterns.
- Use static constructors judiciously to initialize static members.