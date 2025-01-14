### **Polymorphism Overview with Examples**

Polymorphism in C# allows methods or properties in a base class to be overridden in derived classes. This is achieved
using **virtual members**, enabling flexibility in designing object-oriented systems. Here's a detailed explanation with
examples:

* * * * *

### **1\. Virtual Members: Overriding in Derived Classes**

**Concept**: Virtual members in the base class allow derived classes to provide their own implementation using the
`override` keyword.

```
public class BaseClass
{
    public virtual void DoWork()
    {
        Console.WriteLine("BaseClass DoWork");
    }
}

public class DerivedClass : BaseClass
{
    public override void DoWork()
    {
        Console.WriteLine("DerivedClass DoWork");
    }
}

// Usage:
BaseClass baseObj = new DerivedClass();
baseObj.DoWork(); // Output: DerivedClass DoWork

```

- **Key Point**: Even though `baseObj` is of type `BaseClass`, the overridden method in `DerivedClass` is called due to
  **runtime polymorphism**.

* * * * *

### **2\. Inheriting Behavior Without Overriding**

**Concept**: A derived class may inherit the base class implementation without overriding it.

```
public class AnotherDerivedClass : BaseClass
{
    // Inherits DoWork() from BaseClass without changes.
}

// Usage:
BaseClass obj = new AnotherDerivedClass();
obj.DoWork(); // Output: BaseClass DoWork

```

* * * * *

### **3\. Hiding Base Class Members with `new`**

**Concept**: The `new` keyword can be used to define a new implementation that hides the base class member, but it does
not participate in runtime polymorphism.

```
public class HiddenDerivedClass : BaseClass
{
    public new void DoWork()
    {
        Console.WriteLine("HiddenDerivedClass DoWork");
    }
}

// Usage:
HiddenDerivedClass hiddenObj = new HiddenDerivedClass();
hiddenObj.DoWork(); // Output: HiddenDerivedClass DoWork

BaseClass baseHiddenObj = hiddenObj;
baseHiddenObj.DoWork(); // Output: BaseClass DoWork

```

- **Key Point**: When casting to `BaseClass`, the base class method is called because the new method does not override
  the base class method.

* * * * *

### **4\. Preventing Overriding with `sealed`**

**Concept**: The `sealed` keyword prevents further overriding of a virtual member in a derived class.

```
public class SealedBaseClass : BaseClass
{
    public sealed override void DoWork()
    {
        Console.WriteLine("SealedBaseClass DoWork");
    }
}

// Attempting to override in a further derived class will cause a compilation error.
public class FurtherDerivedClass : SealedBaseClass
{
    // Error: Cannot override a sealed method
    // public override void DoWork() { }
}

```

* * * * *

### **5\. Calling Base Class Members Using `base`**

**Concept**: The `base` keyword allows a derived class to invoke the base class implementation of a virtual member.

```
public class BaseWithLogging
{
    public virtual void DoWork()
    {
        Console.WriteLine("BaseWithLogging DoWork");
    }
}

public class DerivedWithLogging : BaseWithLogging
{
    public override void DoWork()
    {
        Console.WriteLine("DerivedWithLogging Pre-Work");
        base.DoWork(); // Call the base class implementation
        Console.WriteLine("DerivedWithLogging Post-Work");
    }
}

// Usage:
BaseWithLogging obj = new DerivedWithLogging();
obj.DoWork();
// Output:
// DerivedWithLogging Pre-Work
// BaseWithLogging DoWork
// DerivedWithLogging Post-Work

```

* * * * *

### **6\. Combining `sealed` and `new`**

**Concept**: A `sealed` virtual method can still be replaced with a `new` method in a derived class.

```
public class SealedClass : BaseClass
{
    public sealed override void DoWork()
    {
        Console.WriteLine("SealedClass DoWork");
    }
}

public class AnotherClass : SealedClass
{
    public new void DoWork()
    {
        Console.WriteLine("AnotherClass DoWork");
    }
}

// Usage:
AnotherClass anotherObj = new AnotherClass();
anotherObj.DoWork(); // Output: AnotherClass DoWork

SealedClass sealedObj = anotherObj;
sealedObj.DoWork(); // Output: SealedClass DoWork

```

* * * * *

### **Key Points Summary**

- **Virtual**: Allows overriding in derived classes.
- **Override**: Replaces the base class implementation in a derived class.
- **new**: Hides the base class implementation, does not participate in runtime polymorphism.
- **sealed**: Prevents further overriding of a virtual member.
- **base**: Accesses the base class implementation of a member from a derived class.

This flexible system lets developers design extensible, maintainable, and modular applications. Let me know if you want
further elaboration or additional examples!