### **Versioning with the `override` and `new` Keywords in C#**

In C#, versioning between base and derived classes allows evolution while maintaining backward compatibility, which is essential when libraries evolve over time. Specifically, the introduction of new members in the base class or changes to existing members in derived classes are managed using the `override` and `new` keywords. These keywords help handle method overriding and hiding scenarios to avoid unexpected behavior.

### **Key Concepts**

1. **Method Overriding**:
   - In C#, a derived class can override a base class method. This means the method in the derived class replaces the base class method, ensuring that calls to the method from an instance of the derived class will invoke the overridden version.

2. **Method Hiding**:
   - If a derived class defines a method with the same name as one in the base class, but does not intend to override it, the method hides the base class method. This can lead to ambiguity and is why the `new` keyword is used to explicitly tell the compiler that this is intentional.

### **The `override` Keyword**:
- **Usage**: The `override` keyword is used when a method in the derived class is intended to replace a `virtual` or `abstract` method from the base class.
- **Requirement**: The base class method must be marked as `virtual` or `abstract` to be overridden.
- **Behavior**: When a method is overridden, calls to the method on instances of the derived class will call the derived version, but the base class version can still be accessed using the `base` keyword.

#### **Example of Method Overriding**:

```csharp
class GraphicsClass
{
    public virtual void DrawLine() { }
    public virtual void DrawRectangle() { }
}

class YourDerivedGraphicsClass : GraphicsClass
{
    // Overriding the DrawRectangle method
    public override void DrawRectangle() { }
}
```

In this case, the derived class's `DrawRectangle` method overrides the base class's `DrawRectangle` method. If the `override` keyword was not used and the derived method had the same name, the compiler would issue a warning about hiding the base class method.

You can still call the base class method explicitly using the `base` keyword:

```csharp
base.DrawRectangle();  // Calls the base class version
```

### **The `new` Keyword**:
- **Usage**: The `new` keyword is used when a method in the derived class is intended to hide a method in the base class.
- **Behavior**: The `new` keyword explicitly tells the compiler that the method in the derived class is not overriding the base class method, but is instead hiding it.

#### **Example of Method Hiding with `new`**:

```csharp
class GraphicsClass
{
    public virtual void DrawLine() { }
    public virtual void DrawRectangle() { }
}

class YourDerivedGraphicsClass : GraphicsClass
{
    // Hiding the DrawRectangle method using the new keyword
    public new void DrawRectangle() { }
}
```

In this scenario, the method `DrawRectangle` in `YourDerivedGraphicsClass` hides the base class's `DrawRectangle` method. If the `new` keyword was not used, the compiler would issue a warning about method hiding.

### **Versioning in Practice:**
To illustrate the impact of versioning in real-world scenarios, consider the following example:

1. **Initial Base Class**: Company A releases a `GraphicsClass` with two methods: `DrawLine` and `DrawPoint`.

```csharp
class GraphicsClass
{
    public virtual void DrawLine() { }
    public virtual void DrawPoint() { }
}
```

2. **Derived Class**: Your program derives a new class `YourDerivedGraphicsClass` and adds a new method `DrawRectangle`.

```csharp
class YourDerivedGraphicsClass : GraphicsClass
{
    public void DrawRectangle() { }
}
```

3. **New Version of `GraphicsClass`**: Company A releases an updated version of `GraphicsClass`, adding a new `DrawRectangle` method.

```csharp
class GraphicsClass
{
    public virtual void DrawLine() { }
    public virtual void DrawPoint() { }
    public virtual void DrawRectangle() { }  // New method
}
```

   Initially, everything works fine, and your derived class continues to function as expected because it’s binary compatible with the old version of `GraphicsClass`.

4. **Recompiling with the New Version**: When you recompile your application with the new version of `GraphicsClass`, you will get a **CS0108** compiler warning, which indicates that there are two methods named `DrawRectangle` (one in the base class and one in the derived class), and you must decide how to handle this conflict.

- If you want to override the new base class method, use the `override` keyword.

```csharp
class YourDerivedGraphicsClass : GraphicsClass
{
    public override void DrawRectangle() { }
}
```

- If you don't want to override the base class's method but want to hide it, use the `new` keyword:

```csharp
class YourDerivedGraphicsClass : GraphicsClass
{
    public new void DrawRectangle() { }
}
```

### **Method Selection in C#**:
When a method is called on an object, and there are multiple methods with the same name, the C# compiler will select the best match based on the parameters provided.

#### **Example**:

```csharp
public class Base
{
    public virtual void DoWork(int param) { }
}

public class Derived : Base
{
    public override void DoWork(int param) { }   // Override
    public void DoWork(double param) { }         // New method
}
```

When `DoWork` is called on an instance of `Derived`:

```csharp
Derived d = new Derived();
d.DoWork(5);  // Calls DoWork(int) - from Base class
d.DoWork(5.5);  // Calls DoWork(double) - from Derived class
```

- If the parameter matches the type of the overridden method (`int`), the overridden version is called.
- If no match is found in the overridden methods, the compiler will try to match based on the method parameters in the derived class.

### **Avoiding Conflicts**:
- **Method Name Conflicts**: To avoid conflicts between new and overridden methods, it’s best to avoid defining new methods with the same name as a `virtual` method in the base class. If this is necessary, ensure clarity by using `new` or `override` explicitly.
- **Casting to Base**: If you want to call the base class method explicitly, cast the instance to the base class.

```csharp
((Base)d).DoWork(5);  // Calls the overridden method in Base class
```

### **Summary**
- **`override`**: Used when you intend to replace the base class method with a new implementation in the derived class. The base class method must be marked as `virtual` or `abstract`.
- **`new`**: Used to hide a base class method intentionally, signaling that the derived class method does not override the base class version.
- Both `override` and `new` allow derived classes to handle methods with the same name differently, enabling backward compatibility and flexibility in versioning.