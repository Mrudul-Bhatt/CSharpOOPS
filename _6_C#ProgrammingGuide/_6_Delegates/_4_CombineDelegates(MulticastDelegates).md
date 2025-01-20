### **How to Combine Delegates (Multicast Delegates) in C#**

---

### **What Are Multicast Delegates?**
- A **multicast delegate** is a delegate that references more than one method.
- Multiple methods can be assigned to a single delegate instance using the `+` operator.
- When the multicast delegate is invoked, **all methods in its invocation list are executed sequentially** in the order they were added.

---

### **Key Points About Multicast Delegates**

1. **Combination of Delegates:**
   - Delegates of the **same type** can be combined using the `+` operator.
   - When combined, the resulting delegate invokes all methods in the invocation list.

2. **Removing Delegates:**
   - Use the `-` operator to remove a delegate from the invocation list.
   - If the delegate to be removed is not found, the operation has no effect.

3. **Execution Order:**
   - Methods in a multicast delegate are executed in the **order of addition**.
   - If a method throws an exception, subsequent methods are **not executed**.

4. **Return Values:**
   - If the delegate returns a value, the **return value of the last method** in the invocation list is used.
   - Avoid return values in multicast delegates if you rely on all methods contributing equally.

---

### **Example Explained**

```csharp
using System;

// Define a custom delegate that takes a string parameter and returns void.
delegate void CustomCallback(string s);

class TestClass
{
    // Define two methods that match the delegate signature.
    static void Hello(string s)
    {
        Console.WriteLine($"  Hello, {s}!");
    }

    static void Goodbye(string s)
    {
        Console.WriteLine($"  Goodbye, {s}!");
    }

    static void Main()
    {
        // Declare instances of the custom delegate.
        CustomCallback hiDel, byeDel, multiDel, multiMinusHiDel;

        // Initialize hiDel to reference the Hello method.
        hiDel = Hello;

        // Initialize byeDel to reference the Goodbye method.
        byeDel = Goodbye;

        // Combine hiDel and byeDel into multiDel.
        multiDel = hiDel + byeDel;

        // Remove hiDel from multiDel, leaving only byeDel.
        multiMinusHiDel = (multiDel - hiDel)!;

        // Invoke each delegate and observe the output.
        Console.WriteLine("Invoking delegate hiDel:");
        hiDel("A");

        Console.WriteLine("Invoking delegate byeDel:");
        byeDel("B");

        Console.WriteLine("Invoking delegate multiDel:");
        multiDel("C"); // Calls Hello and Goodbye in sequence.

        Console.WriteLine("Invoking delegate multiMinusHiDel:");
        multiMinusHiDel("D"); // Calls only Goodbye.
    }
}
```

#### **Execution Flow**

1. **Delegate Instantiation:**
   - `hiDel` references the `Hello` method.
   - `byeDel` references the `Goodbye` method.

2. **Combining Delegates:**
   - `multiDel = hiDel + byeDel;` combines `hiDel` and `byeDel`.
   - When `multiDel` is invoked, it calls both `Hello` and `Goodbye`.

3. **Removing a Delegate:**
   - `multiMinusHiDel = (multiDel - hiDel)!;` removes `hiDel` from `multiDel`, leaving only `byeDel`.
   - When `multiMinusHiDel` is invoked, it calls only `Goodbye`.

---

### **Output Explanation**

```plaintext
Invoking delegate hiDel:
  Hello, A!              // hiDel invokes Hello.

Invoking delegate byeDel:
  Goodbye, B!            // byeDel invokes Goodbye.

Invoking delegate multiDel:
  Hello, C!              // multiDel invokes Hello.
  Goodbye, C!            // multiDel invokes Goodbye.

Invoking delegate multiMinusHiDel:
  Goodbye, D!            // multiMinusHiDel invokes only Goodbye.
```

---

### **Important Notes**

1. **Multicast Delegate with Return Values:**
   - Only the **last method’s return value** is captured.
   - For example:
     ```csharp
     delegate int Calculate(int x);
     
     Calculate calc = Add + Multiply;

     int Add(int x) => x + 1;
     int Multiply(int x) => x * 2;

     Console.WriteLine(calc(3)); // Output: 6 (from Multiply)
     ```

2. **Exception Handling:**
   - If a method in the invocation list throws an exception, **subsequent methods are not executed**.
   - Example:
     ```csharp
     delegate void Process();

     void Method1() => Console.WriteLine("Method1 executed");
     void Method2() => throw new Exception("Error in Method2");
     void Method3() => Console.WriteLine("Method3 executed");

     Process process = Method1 + Method2 + Method3;

     try
     {
         process(); // Executes Method1, throws an exception in Method2, skips Method3.
     }
     catch (Exception ex)
     {
         Console.WriteLine(ex.Message);
     }
     ```

3. **Thread-Safety:**
   - Modifying multicast delegates (adding/removing methods) is **not thread-safe**.
   - Use synchronization mechanisms if delegates are shared across threads.

---

### **When to Use Multicast Delegates**

- **Event Handling:**
  - Multicast delegates are commonly used for **events**, where multiple subscribers respond to the same event.
  - Example:
    ```csharp
    public event EventHandler OnDataReceived;

    // Multiple methods can subscribe to OnDataReceived.
    OnDataReceived += Method1;
    OnDataReceived += Method2;
    ```

- **Chaining Related Methods:**
  - When multiple operations need to be performed sequentially.
  - Example:
    ```csharp
    delegate void Log(string message);

    Log logger = LogToConsole + LogToFile;
    logger("Application started");
    ```

---

### **Conclusion**

- **Multicast delegates** are powerful tools for executing multiple methods in a sequence.
- Use them carefully to handle exceptions and avoid unintended side effects.
- They are especially useful for **event-driven programming** or **callbacks**.