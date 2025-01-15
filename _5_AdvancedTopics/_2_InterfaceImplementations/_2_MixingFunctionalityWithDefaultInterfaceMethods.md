### Explanation: Mixing Functionality with Default Interface Methods in C#

The tutorial introduces **default interface methods**, a feature in C# that allows you to define default implementations for methods directly in interfaces. This capability makes interfaces more flexible by allowing classes to decide whether to use the default implementation, override it, or opt-out entirely. Here's a detailed explanation:

---

### Key Concepts

1. **Default Interface Methods**:
   - Interfaces can provide default implementations for methods.
   - Classes implementing these interfaces can use the default behavior without explicitly implementing the method.

2. **Enhanced Flexibility**:
   - Classes can selectively override default methods when a custom behavior is needed.
   - Classes that don’t implement a specific method still inherit the default behavior.

3. **Separation of Concerns**:
   - Interfaces can now encapsulate both a contract (method declarations) and reusable functionality (default implementations).
   - This avoids duplicating code across multiple implementations.

4. **Virtual by Default**:
   - Methods with default implementations in interfaces are virtual, meaning classes can override them as needed.

---

### Practical Example: Home Automation

Consider a **home automation system** with different types of lights. These lights have various capabilities such as switching on/off, turning on for a specific duration, and blinking.

1. **Base Interface**:
   ```csharp
   public interface ILight
   {
       void SwitchOn();
       void SwitchOff();
       bool IsOn();
   }
   ```

   A basic **OverheadLight** implements this interface:
   ```csharp
   public class OverheadLight : ILight
   {
       private bool isOn;
       public bool IsOn() => isOn;
       public void SwitchOff() => isOn = false;
       public void SwitchOn() => isOn = true;

       public override string ToString() => $"The light is {(isOn ? "on" : "off")}";
   }
   ```

2. **Adding Features with Default Methods**:
   - A timer feature to turn a light off after a certain duration:
     ```csharp
     public interface ITimerLight : ILight
     {
         public async Task TurnOnFor(int duration)
         {
             Console.WriteLine("Default timer function.");
             SwitchOn();
             await Task.Delay(duration);
             SwitchOff();
             Console.WriteLine("Timer completed.");
         }
     }
     ```

   - **OverheadLight** can now inherit this timer functionality:
     ```csharp
     public class OverheadLight : ITimerLight { }
     ```

3. **Customizing Behavior**:
   - A **HalogenLight** overrides the timer feature with its custom implementation:
     ```csharp
     public class HalogenLight : ITimerLight
     {
         private bool isOn;
         public async Task TurnOnFor(int duration)
         {
             Console.WriteLine("Custom timer function.");
             await Task.Delay(duration);
             Console.WriteLine("Halogen timer completed.");
         }
     }
     ```

4. **Combining Features**:
   - You can define additional features like blinking lights:
     ```csharp
     public interface IBlinkingLight : ILight
     {
         public async Task Blink(int duration, int repeatCount)
         {
             Console.WriteLine("Default blinking function.");
             for (int i = 0; i < repeatCount; i++)
             {
                 SwitchOn();
                 await Task.Delay(duration);
                 SwitchOff();
                 await Task.Delay(duration);
             }
         }
     }
     ```

   - A **LEDLight** implements and overrides both blinking and timer features:
     ```csharp
     public class LEDLight : IBlinkingLight, ITimerLight
     {
         public async Task Blink(int duration, int repeatCount)
         {
             Console.WriteLine("Custom blink function.");
             await Task.Delay(duration * repeatCount);
         }
     }
     ```

---

### Key Advantages

1. **Reduced Boilerplate Code**:
   - Shared functionality can now be implemented once in an interface, reducing duplication.

2. **Encapsulation of Behavior**:
   - Interfaces can encapsulate both the behavior (via default implementations) and the contract.

3. **Modularity**:
   - Features can be split into smaller interfaces, making it easy to mix and match capabilities.

4. **Backward Compatibility**:
   - Adding a default implementation to an interface doesn’t break existing implementations.

---

### Testing with Pattern Matching

Using **pattern matching**, the application can dynamically detect and test a light's capabilities:
```csharp
private static async Task TestLightCapabilities(ILight light)
{
    if (light is ITimerLight timer)
        await timer.TurnOnFor(1000);

    if (light is IBlinkingLight blinker)
        await blinker.Blink(500, 5);
}
```

---

### Limitations and Best Practices

1. **Ambiguity**:
   - If multiple derived interfaces provide different default implementations for the same method, a class must override the method to resolve ambiguity.

2. **Keep Interfaces Small**:
   - Focus each interface on a single responsibility to avoid complex hierarchies.

3. **Avoid Overuse**:
   - Default interface methods are a powerful tool but should not replace abstract classes or inheritance in all scenarios.

---

### Conclusion

Default interface methods in C# introduce a new level of flexibility, allowing you to mix functionality directly in interfaces. This approach minimizes boilerplate code, enhances modularity, and makes your application design more expressive and adaptable to real-world scenarios.