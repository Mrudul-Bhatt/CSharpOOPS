### **Knowing When to Use `override` and `new` Keywords in C#**

In C#, when a method in a derived class has the same name as a method in a base class, you have the choice of whether to override or hide the base class method. You can specify how these methods interact using the `override` and `new` keywords. The use of these keywords depends on how you want to manage the behavior of inherited methods and how they interact with base class methods.

### **The `override` Keyword**:
- The `override` keyword is used when you want to **extend** the behavior of a `virtual` method defined in a base class. 
- It allows the derived class to provide a new implementation for a method that was already defined in the base class.

### **The `new` Keyword**:
- The `new` keyword is used when you want to **hide** the base class method with a new definition. This prevents the derived class method from overriding the base class method, but instead, it hides it completely.
- The `new` keyword is useful when you want to provide a different implementation of the same method name without altering the base class behavior.

### **Example to Understand the Use of `override` and `new`**

Consider two classes: `BaseClass` and `DerivedClass`, where `DerivedClass` inherits from `BaseClass`. Initially, `BaseClass` has two methods: `Method1` and `Method2`.

```csharp
class BaseClass  
{  
    public void Method1()  
    {  
        Console.WriteLine("Base - Method1");  
    }  
}  
  
class DerivedClass : BaseClass  
{  
    public void Method2()  
    {  
        Console.WriteLine("Derived - Method2");  
    }  
}
```

In this scenario, you have:
- `bc` of type `BaseClass`: Can only call methods from `BaseClass` (i.e., `Method1`).
- `dc` of type `DerivedClass`: Can call both `Method1` and `Method2` (from `BaseClass` and `DerivedClass` respectively).
- `bcdc` of type `BaseClass`, but the instance is of type `DerivedClass`: Calls `Method1` from `BaseClass` by default.

### **Adding a New Method to BaseClass**

Now, let's add `Method2` to `BaseClass`, which matches the signature of `Method2` in `DerivedClass`:

```csharp
public void Method2()  
{  
    Console.WriteLine("Base - Method2");  
}
```

When you compile this, a warning is triggered because `Method2` in `DerivedClass` hides the `Method2` in `BaseClass`. The compiler recommends using the `new` keyword to explicitly indicate that you intend to hide the base class method.

#### **Code Output Before Using `new`**:

```csharp
// Output:
// Base - Method1
// Base - Method2
// Base - Method1
// Derived - Method2
// Base - Method1
// Base - Method2
```

Without using the `new` keyword, the method `Method2` in `DerivedClass` hides the one in `BaseClass`, and no warning is generated at runtime, but the compiler will show a **CS0108** warning indicating that you have hidden a base class method.

To resolve this, you can add the `new` keyword:

```csharp
public new void Method2()  
{  
    Console.WriteLine("Derived - Method2");  
}
```

Now, this will suppress the warning, and the output will remain unchanged, but the compiler acknowledges that the method is intentionally hiding a method in the base class.

### **Using `override` for Inheritance Hierarchy Behavior**

Now, let's modify the example to use the `override` keyword. In this case, you would like to change the behavior of `Method1` in `BaseClass` and make sure that objects of `DerivedClass` use the overridden method rather than the base class method.

1. Add `virtual` to `Method1` in `BaseClass`:

```csharp
public virtual void Method1()  
{  
    Console.WriteLine("Base - Method1");  
}
```

2. Override `Method1` in `DerivedClass`:

```csharp
public override void Method1()  
{  
    Console.WriteLine("Derived - Method1");  
}
```

Now, when `Method1` is called on `bcdc` (which is of type `BaseClass` but an instance of `DerivedClass`), the method from `DerivedClass` is called, not the base class version.

#### **Code Output with `override`**:

```csharp
// Output:
// Base - Method1
// Base - Method2
// Derived - Method1
// Derived - Method2
// Derived - Method1
// Base - Method2
```

### **Key Differences Between `override` and `new`**:

- **`override`**:
  - It allows you to change or extend the functionality of a `virtual` or `abstract` method in the base class.
  - It applies to methods that are part of the base class's interface and can be overridden in derived classes.
  - When `override` is used, calls to the method are resolved at runtime based on the actual type of the object (polymorphism).

- **`new`**:
  - It hides a method in the base class, and the new method in the derived class is called instead of the base class version, regardless of the object’s actual type.
  - It doesn’t change the base class method's behavior; it simply creates a new method with the same name in the derived class.
  - It is primarily used to provide a different implementation while maintaining the base class method’s functionality.

### **Which One to Use?**
- Use **`override`** when:
  - You want to change or extend the behavior of a base class method in the derived class.
  - You are working with polymorphism, and you want the derived class method to be called when working with derived class objects.
  
- Use **`new`** when:
  - You want to hide a method in the base class and provide a completely new implementation in the derived class.
  - You are not concerned with polymorphism and want to handle the method in the derived class independently of the base class.

### **Conclusion**
- The **`override`** keyword is ideal for modifying or extending the functionality of a method from a base class, supporting polymorphism.
- The **`new`** keyword is used when you need to hide the base class method with a new implementation in the derived class, but it does not provide polymorphism.