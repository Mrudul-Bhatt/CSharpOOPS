### **Top-Level Statements and Implicit Global Usings in .NET 6**

.NET 6 introduces several new features to simplify application development, including **top-level statements** and **implicit global using directives**.

* * * * *

**1\. Top-Level Statements**
----------------------------

Top-level statements allow you to write the entry point of your application without explicitly defining a `Main` method
or a class. This feature simplifies small programs and reduces boilerplate code.

### **Example: Traditional `Main` Method**

```
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

```

### **Example: Top-Level Statements in .NET 6**

```

Console.WriteLine("Hello, World!");

```

- No need for a `Main` method.
- Suitable for small applications or scripts.
- The compiler generates the necessary `Main` method during compilation.

* * * * *

**2\. Implicit Global Using Directives**
----------------------------------------

With the .NET 6 SDK, some commonly used namespaces are automatically included depending on the project type. You no
longer need to manually add `using` directives for these namespaces.

### **Supported SDKs**

Implicit global `using` directives are added automatically for projects using the following SDKs:

1. **`Microsoft.NET.Sdk`**: Console or library applications.
2. **`Microsoft.NET.Sdk.Web`**: Web applications.
3. **`Microsoft.NET.Sdk.Worker`**: Worker services.

### **Common Implicit Usings**

For example, in a console application (`Microsoft.NET.Sdk`), these namespaces might be included automatically:

```

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

```

You can focus on your core logic without writing these imports explicitly.

### **Example: Without Implicit Usings**

```

using System;

Console.WriteLine("Hello, World!");

```

### **Example: With Implicit Usings**

```

Console.WriteLine("Hello, World!"); // Works without explicitly writing "using System;"

```

* * * * *

**3\. Customizing Implicit Usings**
-----------------------------------

You can control implicit global using directives in your project by editing the `.csproj` file.

### **Disable Implicit Usings**

To turn off implicit usings:

```

<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <ImplicitUsings>disable</ImplicitUsings>
  </PropertyGroup>
</Project>

```

Once disabled, you must manually add all `using` directives.

* * * * *

**Benefits of These Features**
------------------------------

1. **Simplified Syntax**: Reduces boilerplate code for small applications.
2. **Improved Productivity**: Focus on logic without worrying about common `using` directives.
3. **Consistency**: Implicit usings ensure commonly needed namespaces are always available.

* * * * *

### **Conclusion**

If you're using .NET 6 or later, your project might already use **top-level statements** and **implicit global usings**.
This results in cleaner, more concise code, especially for small projects. However, understanding these features ensures
you can adapt to the changes or customize them to your needs.