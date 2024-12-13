### **Classes and Structs: Blueprints and Instances**

A **class** or **struct** acts as a blueprint that defines the structure (fields, properties) and behavior (methods,
events) of objects. These objects, also called **instances**, are created from the blueprint during program execution.
While both classes and structs define types, they differ in behavior, especially in memory allocation and object
assignment.

* * * * *

### **Key Differences Between Class and Struct Instances**

| **Aspect**              | **Classes**                   | **Structs**                                  |
|-------------------------|-------------------------------|----------------------------------------------|
| **Type**                | Reference Type                | Value Type                                   |
| **Memory Allocation**   | On the managed heap           | On the stack                                 |
| **Assignment Behavior** | Copies the reference (shared) | Copies the value (independent)               |
| **Use of `new`**        | Required to initialize        | Optional                                     |
| **Garbage Collection**  | Yes (handled by CLR)          | No (stack memory is reclaimed automatically) |

* * * * *

### **Example 1: Class Behavior**

In this example, class instances are reference types. Changes made to one reference affect all references pointing to
the same object.

```
using System;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main()
    {
        // Create an instance of the Person class
        Person person1 = new Person("Leopold", 6);
        Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);

        // Assign person1 reference to person2
        Person person2 = person1;

        // Modify person2
        person2.Name = "Molly";
        person2.Age = 16;

        // Both person1 and person2 reflect changes because they point to the same object
        Console.WriteLine("person2: Name = {0}, Age = {1}", person2.Name, person2.Age);
        Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);
    }
}

```

### **Output**

```
person1: Name = Leopold, Age = 6
person2: Name = Molly, Age = 16
person1: Name = Molly, Age = 16

```

* * * * *

### **Example 2: Struct Behavior**

In contrast, struct instances are value types. Assignments copy the value, resulting in independent objects.

```
using System;

public struct Person
{
    public string Name;
    public int Age;

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

class Program
{
    static void Main()
    {
        // Create an instance of the Person struct
        Person person1 = new Person("Alex", 9);
        Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);

        // Copy the value of person1 into person2
        Person person2 = person1;

        // Modify person2
        person2.Name = "Spencer";
        person2.Age = 7;

        // person1 remains unchanged
        Console.WriteLine("person2: Name = {0}, Age = {1}", person2.Name, person2.Age);
        Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);
    }
}

```

### **Output**

```
person1: Name = Alex, Age = 9
person2: Name = Spencer, Age = 7
person1: Name = Alex, Age = 9

```

* * * * *

### **Explanation of Memory Allocation**

1. **Classes (Reference Types)**
    - Allocated on the **heap**.
    - Multiple variables can point to the same memory location (object).
    - Changes via one variable reflect in all variables referencing the same object.
2. **Structs (Value Types)**
    - Allocated on the **stack**.
    - Each variable gets a **copy** of the data.
    - Modifications to one variable do not affect others.

* * * * *

### **Use Cases for Classes and Structs**

- Use **classes** when:
    - Objects are large or complex.
    - You need shared references.
    - Objects have behaviors or methods.
- Use **structs** when:
    - Data is small and simple (e.g., points, colors).
    - Value semantics are preferred.
    - Performance is critical for stack allocation.

* * * * *

### **Conclusion**

Classes and structs serve different purposes in C#. Understanding their memory behavior and assignment semantics is
critical for designing robust and efficient applications. While classes offer reference-based behavior suitable for
complex entities, structs provide lightweight value-based behavior ideal for small, immutable data.