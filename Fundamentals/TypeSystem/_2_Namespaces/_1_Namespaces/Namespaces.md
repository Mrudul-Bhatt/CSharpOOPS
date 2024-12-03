### **Namespaces in C#**

Namespaces are a key feature in C# to organize code and avoid naming conflicts. They are used in two main ways:

* * * * *

### **1\. Organizing Classes in the .NET Framework**

Namespaces group related classes in the .NET library. For example:

`System.Console.WriteLine("Hello World!");`

- `System` is the namespace, and `Console` is a class within it.
- To avoid typing the full namespace path repeatedly, you can use the `using` directive:

```using System;
Console.WriteLine("Hello World!");```

This simplifies code by allowing direct access to classes in the namespace.