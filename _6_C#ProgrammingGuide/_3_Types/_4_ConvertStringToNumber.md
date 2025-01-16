### **How to Convert a String to a Number in C#**

In C#, converting a string to a numeric type (like `int`, `long`, `double`, etc.) is commonly done using methods like `Parse`, `TryParse`, or `Convert.ToXxx`. These methods are part of the respective numeric types or the `System.Convert` class.

#### **1. Using `Parse` and `TryParse` Methods**

The `Parse` and `TryParse` methods are the most straightforward ways to convert strings to numbers. These methods belong to numeric types such as `int`, `double`, `decimal`, etc.

- **`Parse` Method**: 
  - The `Parse` method is used when you are confident that the string contains a valid number. If the conversion fails, it throws an exception (`FormatException` or `OverflowException`).
  
- **`TryParse` Method**: 
  - The `TryParse` method is safer as it doesn't throw an exception. Instead, it returns a boolean value indicating whether the conversion was successful. If the conversion succeeds, it places the result in an `out` parameter.

#### **Example: Using `Parse` and `TryParse`**

```csharp
using System;

public static class StringConversion
{
    public static void Main()
    {
        string input = String.Empty;
        try
        {
            int result = Int32.Parse(input);  // Throws FormatException if input is empty
            Console.WriteLine(result);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{input}'");
        }

        try
        {
            int numVal = Int32.Parse("-105");
            Console.WriteLine(numVal);  // Output: -105
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }

        if (Int32.TryParse("-105", out int j))
        {
            Console.WriteLine(j);  // Output: -105
        }
        else
        {
            Console.WriteLine("String could not be parsed.");
        }

        const string inputString = "abc";
        if (Int32.TryParse(inputString, out int numValue))
        {
            Console.WriteLine(numValue);
        }
        else
        {
            Console.WriteLine($"Int32.TryParse could not parse '{inputString}' to an int.");
        }
        // Output: Int32.TryParse could not parse 'abc' to an int.
    }
}
```

- **Explanation**:
  - The `Parse` method throws a `FormatException` if the string is not a valid number (like an empty string or a non-numeric value).
  - The `TryParse` method returns `false` when the string is invalid but doesn't throw an exception. It's useful for avoiding exceptions in cases where the input might be unpredictable.

#### **Parsing Strings with Non-Numeric Characters**

If a string includes a mix of valid numeric characters (such as in hexadecimal values) and trailing non-numeric characters, you can extract the valid numeric part and then parse it.

#### **Example: Parsing a Hexadecimal String**

```csharp
using System;

public static class StringConversion
{
    public static void Main()
    {
        var str = "  10FFxxx";
        string numericString = string.Empty;
        foreach (var c in str)
        {
            if ((c >= '0' && c <= '9') || (char.ToUpperInvariant(c) >= 'A' && char.ToUpperInvariant(c) <= 'F') || c == ' ')
            {
                numericString = string.Concat(numericString, c.ToString());
            }
            else
            {
                break;
            }
        }

        if (int.TryParse(numericString, System.Globalization.NumberStyles.HexNumber, null, out int i))
        {
            Console.WriteLine($"'{str}' --> '{numericString}' --> {i}");
        }
        // Output: '  10FFxxx' --> '  10FF' --> 4351
    }
}
```

- **Explanation**:
  - The example checks each character in the string, and if it's a valid hexadecimal character (`0-9`, `A-F`), it appends it to the `numericString`. 
  - After extracting the valid part, it uses `TryParse` to convert the string into a number.

---

#### **2. Using the `Convert` Class**

The `Convert` class provides a set of methods for converting strings to various numeric types. Unlike `Parse` and `TryParse`, `Convert` works on objects that implement the `IConvertible` interface and internally calls the `Parse` method for most conversions.

#### **Example: Using `Convert.ToInt32`**

```csharp
using System;

public class ConvertStringExample
{
    static void Main(string[] args)
    {
        int numVal = -1;
        bool repeat = true;

        while (repeat)
        {
            Console.Write("Enter a number: ");
            string? input = Console.ReadLine();

            try
            {
                numVal = Convert.ToInt32(input);  // Uses Parse internally
                if (numVal < Int32.MaxValue)
                {
                    Console.WriteLine("The new value is {0}", ++numVal);
                }
                else
                {
                    Console.WriteLine("numVal cannot be incremented beyond its current value");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            Console.Write("Go again? Y/N: ");
            string? go = Console.ReadLine();
            if (go?.ToUpper() != "Y")
            {
                repeat = false;
            }
        }
    }
}
```

- **Explanation**:
  - The `Convert.ToInt32()` method attempts to convert the string to an integer. If the string is not a valid number or exceeds the bounds of an `int`, it throws a `FormatException` or `OverflowException`.
  - This method is useful when you need a general conversion approach, especially when working with objects that implement `IConvertible`.

---

### **Key Points to Remember**
- **`Parse`** throws an exception if the string is not a valid number.
- **`TryParse`** returns `false` without throwing an exception, making it safer for unpredictable input.
- The **`Convert`** class provides methods like `Convert.ToInt32`, `Convert.ToDecimal`, etc., for converting strings to numbers.
- Always handle exceptions when using `Parse` or `Convert` to avoid runtime errors.
