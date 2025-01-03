### **Composition with Tasks**

The concept of **task composition** involves combining asynchronous and synchronous operations into a single
asynchronous operation. This is a fundamental technique for creating modular, maintainable, and efficient asynchronous
code.

---

### **Key Principle**

> If any part of an operation is asynchronous, the entire operation is asynchronous.

This principle ensures that asynchronous operations can be composed and treated as units of work, even when they include
synchronous steps.

---

### **Example: Toast Composition**

In the breakfast analogy, the process of making toast includes:

1.  **Asynchronous operation:** Toasting the bread.
2.  **Synchronous operations:** Applying butter and jam.

This composite task is represented by the `MakeToastWithButterAndJamAsync` method:

```
static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
{
    var toast = await ToastBreadAsync(number); // Asynchronous
    ApplyButter(toast);                        // Synchronous
    ApplyJam(toast);                           // Synchronous

    return toast; // Returns the result of the composed task
}

```

### **Explanation**

- **`async` modifier:** Indicates that this method performs asynchronous work.
- **`await` keyword:** Waits for the `ToastBreadAsync` task to complete before proceeding.
- **Synchronous operations:** `ApplyButter` and `ApplyJam` are called once the toast is ready.
- **Return value:** The method returns a `Task<Toast>` that represents the entire process.

---

### **Advantages of Composition**

1.  **Modularity:**
    - By encapsulating the toast-making logic into a separate method, the code becomes easier to understand and reuse.
2.  **Abstraction:**
    - Consumers of `MakeToastWithButterAndJamAsync` don't need to know how toast is made. They only await the final
      result.
3.  **Concurrent Execution:**
    - Other tasks can run while the `ToastBreadAsync` operation is in progress.

---

### **Updated Main Method**

The main logic for preparing breakfast now incorporates task composition:

```
static async Task Main(string[] args)
{
    Coffee cup = PourCoffee();
    Console.WriteLine("coffee is ready");

    var eggsTask = FryEggsAsync(2);               // Start frying eggs
    var baconTask = FryBaconAsync(3);             // Start frying bacon
    var toastTask = MakeToastWithButterAndJamAsync(2); // Start making toast

    var eggs = await eggsTask;                    // Await eggs
    Console.WriteLine("eggs are ready");

    var bacon = await baconTask;                  // Await bacon
    Console.WriteLine("bacon is ready");

    var toast = await toastTask;                  // Await toast
    Console.WriteLine("toast is ready");

    Juice oj = PourOJ();                          // Pour juice
    Console.WriteLine("oj is ready");
    Console.WriteLine("Breakfast is ready!");
}

```

---

### **Key Changes**

1.  **Encapsulation:**
    - Toast-making logic is moved into `MakeToastWithButterAndJamAsync`.
2.  **Concurrent Execution:**
    - All tasks (`FryEggsAsync`, `FryBaconAsync`, `MakeToastWithButterAndJamAsync`) start simultaneously.
3.  **Deferred Awaiting:**
    - Tasks are awaited only when their results are needed, reducing idle time.

---

### **Benefits of Task Composition**

### **Improved Readability**

- Separating complex operations into discrete methods makes the code easier to read and maintain.

### **Reuse and Extensibility**

- The `MakeToastWithButterAndJamAsync` method can be reused wherever toast-making logic is required.

### **Efficient Execution**

- By starting all tasks concurrently and awaiting results as needed, the total time is minimized.

---

### **Practical Applications**

The pattern demonstrated here is widely used in real-world programming scenarios:

1.  **Web Applications:**
    - Fetching data from multiple APIs and combining the results.
    - Example: Fetching user info, order history, and recommended products concurrently.
2.  **Batch Processing:**
    - Performing independent tasks like processing files or running database queries.
3.  **UI Applications:**
    - Handling animations, loading data, and responding to user input concurrently.

---

### **Summary**

Task composition simplifies asynchronous programming by combining related operations into a single asynchronous task.
This approach enhances modularity, reusability, and efficiency, enabling developers to build robust and responsive
applications. By using task composition, you can manage complexity while ensuring optimal performance.
