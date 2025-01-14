### **Object Identity vs. Value Equality**

When comparing objects, the distinction between **object identity** and **value equality** is crucial:

1. **Object Identity**: Determines if two variables reference the exact same object in memory.
2. **Value Equality**: Determines if the values of fields or properties of two objects are the same.

* * * * *

### **Key Points**

- **Reference Types (Classes)**:
    - By default, equality checks for **identity** (same memory address).
    - You can override equality methods (`Equals`, `==`) or implement interfaces (`IEquatable<T>`) to support **value
      equality**.
- **Value Types (Structs)**:
    - Comparisons are based on **value equality** by default.
    - Use the `Equals` method or the `==` operator for field-by-field comparison.

* * * * *

### **Example 1: Object Identity in Reference Types**

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
        // Create two separate instances
        Person person1 = new Person("Alice", 30);
        Person person2 = new Person("Alice", 30);

        // Compare references (identity)
        if (ReferenceEquals(person1, person2))
            Console.WriteLine("person1 and person2 are the same object.");
        else
            Console.WriteLine("person1 and person2 are different objects.");
    }
}

```

### **Output**

```
person1 and person2 are different objects.

```

Explanation: Even though `person1` and `person2` have the same values, they are different objects in memory.

* * * * *

### **Example 2: Value Equality in Structs**

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
        // Create two struct instances with the same values
        Person p1 = new Person("Bob", 40);
        Person p2 = new Person("Bob", 40);

        // Compare values
        if (p1.Equals(p2))
            Console.WriteLine("p1 and p2 have the same values.");
        else
            Console.WriteLine("p1 and p2 have different values.");
    }
}

```

### **Output**

```
p1 and p2 have the same values.

```

Explanation: Structs are value types, so their `Equals` method compares field values.

* * * * *

### **Example 3: Custom Value Equality for Classes**

For classes, you need to define custom equality logic if you want to compare **values** instead of **references**.

```
using System;

public class Person : IEquatable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public bool Equals(Person other)
    {
        if (other == null) return false;
        return Name == other.Name && Age == other.Age;
    }

    public override bool Equals(object obj)
    {
        if (obj is Person other)
            return Equals(other);
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }
}

class Program
{
    static void Main()
    {
        Person person1 = new Person("Charlie", 25);
        Person person2 = new Person("Charlie", 25);

        // Compare values
        if (person1.Equals(person2))
            Console.WriteLine("person1 and person2 have the same values.");
        else
            Console.WriteLine("person1 and person2 have different values.");
    }
}

```

### **Output**

```
person1 and person2 have the same values.

```

* * * * *

### **Example 4: Records and Value Equality**

In C#, **records** are reference types but use **value semantics** for equality by default.

```
public record Person(string Name, int Age);

class Program
{
    static void Main()
    {
        Person person1 = new Person("Dana", 35);
        Person person2 = new Person("Dana", 35);

        // Compare records for value equality
        if (person1 == person2)
            Console.WriteLine("person1 and person2 have the same values.");
        else
            Console.WriteLine("person1 and person2 have different values.");
    }
}

```

### **Output**

```
person1 and person2 have the same values.

```

Explanation: Unlike regular classes, records automatically implement value-based equality for their properties.

* * * * *

### **Comparison Summary**

| **Type**            | **Comparison Default** | **How to Customize**                                |
|---------------------|------------------------|-----------------------------------------------------|
| **Reference Types** | Identity (memory)      | Override `Equals`, `==`, or use `IEquatable<T>`     |
| **Value Types**     | Field values           | Already compares values, but can override if needed |
| **Records (C# 9+)** | Field values           | Automatic value equality, can still override        |

Understanding the difference between **identity** and **value** comparisons helps you decide the appropriate approach
for each scenario.