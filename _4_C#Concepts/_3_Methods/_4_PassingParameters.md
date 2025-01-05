### **Parameter Passing in C#**

In C#, the way parameters are passed to methods plays a crucial role in how data is manipulated and returned. Parameters can be passed **by value** (default), **by reference**, or using a **parameter collection** (`params`).

---

### **1. Passing Parameters by Value**
- **Definition**: The method gets a copy of the value (for value types) or a copy of the reference (for reference types).
- **Behavior**:
  - For **value types**: Changes to the parameter in the method do not affect the original variable.
  - For **reference types**: Changes to the **fields or properties** of the object will affect the original object, but assigning a new object to the parameter does not change the original reference.
  
#### **Example with Value Type**
```csharp
public static class ByValueExample
{
    public static void Main()
    {
        var value = 20;
        Console.WriteLine("In Main, value = {0}", value);
        ModifyValue(value);
        Console.WriteLine("Back in Main, value = {0}", value);
    }

    static void ModifyValue(int i)
    {
        i = 30; // Changes only the copy of the value.
        Console.WriteLine("In ModifyValue, parameter value = {0}", i);
    }
}
```
**Output**:
```
In Main, value = 20
In ModifyValue, parameter value = 30
Back in Main, value = 20
```

#### **Example with Reference Type**
```csharp
public class SampleRefType
{
    public int value;
}

public static class ByRefTypeExample
{
    public static void Main()
    {
        var rt = new SampleRefType { value = 44 };
        ModifyObject(rt);
        Console.WriteLine(rt.value);
    }

    static void ModifyObject(SampleRefType obj)
    {
        obj.value = 33; // Modifies the field of the object.
    }
}
```
**Output**:
```
33
```
Here, the `value` field of `rt` is modified because the reference itself is passed by value, allowing access to the object it points to.

---

### **2. Passing Parameters by Reference**
- **Definition**: The method gets a reference to the original variable or object, allowing direct modifications to the original data.
- **Keywords**:
  - `ref`: Requires the variable to be initialized before being passed.
  - `out`: Does not require the variable to be initialized before being passed, but must be assigned within the method.
  - `in`: Passes the parameter by reference but ensures it cannot be modified within the method.
  
#### **Example with `ref`**
```csharp
public static class ByRefExample
{
    public static void Main()
    {
        var value = 20;
        Console.WriteLine("In Main, value = {0}", value);
        ModifyValue(ref value);
        Console.WriteLine("Back in Main, value = {0}", value);
    }

    private static void ModifyValue(ref int i)
    {
        i = 30; // Directly modifies the original variable.
        Console.WriteLine("In ModifyValue, parameter value = {0}", i);
    }
}
```
**Output**:
```
In Main, value = 20
In ModifyValue, parameter value = 30
Back in Main, value = 30
```

#### **Example with `out`**
```csharp
public static void Initialize(out int value)
{
    value = 42; // Must be assigned within the method.
}

public static void Main()
{
    int value;
    Initialize(out value);
    Console.WriteLine(value); // Outputs 42.
}
```

#### **Example with `in`**
```csharp
public static void DisplayValue(in int value)
{
    // value = 30; // Compilation error - cannot modify a `readonly` in parameter.
    Console.WriteLine(value);
}

public static void Main()
{
    int value = 42;
    DisplayValue(in value); // Passes by reference but is read-only in the method.
}
```

---

### **3. Swapping Variables Using `ref`**
- A common pattern with `ref` is swapping the values of two variables.
```csharp
public static class RefSwapExample
{
    static void Main()
    {
        int i = 2, j = 3;
        Console.WriteLine("i = {0}  j = {1}", i, j);

        Swap(ref i, ref j);

        Console.WriteLine("i = {0}  j = {1}", i, j);
    }

    static void Swap(ref int x, ref int y) =>
        (y, x) = (x, y);
}
```
**Output**:
```
i = 2  j = 3
i = 3  j = 2
```

---

### **4. Parameter Collections Using `params`**
- **Definition**: Allows passing a variable number of arguments of the same type to a method.
- **Rules**:
  - The `params` parameter must be the last in the parameter list.
  - It can accept an array, a comma-separated list of arguments, or no arguments.

#### **Example**
```csharp
static class ParamsExample
{
    static void Main()
    {
        Console.WriteLine(GetVowels(new[] { "apple", "banana", "pear" }));
        Console.WriteLine(GetVowels("apple", "banana", "pear"));
        Console.WriteLine(GetVowels(null));
        Console.WriteLine(GetVowels());
    }

    static string GetVowels(params string[]? input)
    {
        if (input == null || input.Length == 0) return string.Empty;

        char[] vowels = { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' };
        return string.Concat(input.SelectMany(word => word.Where(vowels.Contains)));
    }
}
```
**Output**:
```
aeaaaea
aeaaaea

```

---

### **5. Summary**
- **Pass by Value**:
  - Value types: Copy is passed; changes in the method don’t affect the original variable.
  - Reference types: Reference is passed; changes to fields affect the original object, but reassignment doesn’t.
  
- **Pass by Reference**:
  - Use `ref`, `out`, or `in` to modify or prevent copying.
  - Enables direct interaction with the caller's variable or object.

- **Parameter Collections**:
  - `params` allows variable-length argument lists, simplifying methods that handle an unknown number of parameters.