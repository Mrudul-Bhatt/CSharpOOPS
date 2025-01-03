The changes described above modify the order of operations to make better use of **asynchronous programming** by
starting multiple tasks concurrently and awaiting their completion only when their results are actually needed. Here's a
breakdown of the approach and its benefits:

---

### **Original Code**

In the original code:

```
Task<Egg> eggsTask = FryEggsAsync(2);
Egg eggs = await eggsTask;
Console.WriteLine("Eggs are ready");

Task<Bacon> baconTask = FryBaconAsync(3);
Bacon bacon = await baconTask;
Console.WriteLine("Bacon is ready");

```

- Each task (`FryEggsAsync` and `FryBaconAsync`) is awaited immediately after starting, meaning the operations happen
  **sequentially**.
- While the eggs are frying, the program waits for that task to complete before even starting to fry the bacon.

This is less efficient because some operations, like frying bacon, could have started during the egg-frying delay.

---

### **Improved Code**

In the updated code:

```
Task<Egg> eggsTask = FryEggsAsync(2);
Task<Bacon> baconTask = FryBaconAsync(3);
Task<Toast> toastTask = ToastBreadAsync(2);

// Process toast while eggs and bacon are frying
Toast toast = await toastTask;
ApplyButter(toast);
ApplyJam(toast);
Console.WriteLine("Toast is ready");

// Wait for eggs and bacon to finish before serving breakfast
Egg eggs = await eggsTask;
Console.WriteLine("Eggs are ready");
Bacon bacon = await baconTask;
Console.WriteLine("Bacon is ready");

Console.WriteLine("Breakfast is ready!");

```

### **What's Happening?**

1.  **Starting Tasks Concurrently**:
    - Tasks for frying eggs, bacon, and toasting bread are all **started immediately**.
    - This does not block the main thread, and the operations run concurrently (asynchronously).
2.  **Processing While Waiting**:
    - Instead of awaiting each task immediately, the program processes tasks that can be completed independently:
      - Toast is processed while eggs and bacon are still frying.
    - This overlap improves efficiency by utilizing the time spent waiting for asynchronous operations.
3.  **Await at the End**:
    - The program awaits the completion of eggs and bacon only **when their results are needed** (before announcing they
      are ready and serving breakfast).

---

### **Benefits**

1.  **Concurrency**:
    - By starting all tasks immediately, the code allows these tasks to run in parallel (concurrently).
    - This reduces the total time needed to prepare breakfast since tasks that don't depend on each other (e.g., frying
      eggs and bacon) are done simultaneously.
2.  **Improved Efficiency**:
    - The program is not idle while waiting for long-running tasks like frying bacon or eggs. Instead, it works on other
      tasks (like preparing toast or pouring juice).
3.  **Simpler Structure**:
    - The flow of the code clearly separates the **starting of tasks** from their **completion handling**.

---

### **Execution Flow**

Here's how the execution flow looks with the updated code:

1.  Pour coffee and start tasks for frying eggs, frying bacon, and toasting bread.
2.  While eggs and bacon are frying, process the toast (apply butter and jam).
3.  Await the completion of eggs and bacon only after the toast is ready.
4.  Serve all items together once they are ready.

---

### **Illustration**

If tasks take the following times:

- FryEggsAsync: 5 seconds
- FryBaconAsync: 6 seconds
- ToastBreadAsync: 3 seconds

### Original Code (Sequential):

```
- Fry eggs: 5 seconds
- Fry bacon: 6 seconds
- Toast bread: 3 seconds
Total: 14 seconds

```

### Improved Code (Concurrent):

```
- Start all tasks concurrently.
- Toast bread: 3 seconds (complete first).
- Fry eggs and bacon finish after 6 seconds (bacon takes the longest).
Total: 6 seconds

```

By overlapping tasks, the breakfast is ready in 6 seconds instead of 14 seconds.

---

### **Conclusion**

The improved code demonstrates the power of asynchronous programming by:

1.  Starting multiple tasks simultaneously.
2.  Leveraging independent tasks to reduce overall wait time.
3.  Delivering results faster and more efficiently by avoiding unnecessary blocking.
