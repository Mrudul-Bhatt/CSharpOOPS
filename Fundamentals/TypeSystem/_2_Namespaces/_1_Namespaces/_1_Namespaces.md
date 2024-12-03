### Namespaces in C#

Namespaces are a key feature in C# to organize code and avoid naming conflicts. They are used in two main ways:

* * * * *

### 1\. Organizing Classes in the .NET Framework

Namespaces group related classes in the .NET library. For example:

System.Console.WriteLine("Hello World!");

- System is the namespace, and Console is a class within it.

- To avoid typing the full namespace path repeatedly, you can use the using directive:

using System;

Console.WriteLine("Hello World!");

This simplifies code by allowing direct access to classes in the namespace.

### 2\. Declaring Custom Namespaces

You can create your own namespaces to organize your code and prevent naming conflicts in larger projects.

#### Example 1: Declaring a Namespace

namespace SampleNamespace

{

class SampleClass

{

public void SampleMethod()

{

System.Console.WriteLine("SampleMethod inside SampleNamespace");

}

}

}

class Program

{

static void Main()

{

// Use the fully qualified name to access the class

SampleNamespace.SampleClass sample = new SampleNamespace.SampleClass();

sample.SampleMethod();

}

}

- Namespace Name: SampleNamespace

- Class Name: SampleClass

Usage: You need to use the namespace explicitly unless it is imported using using.

#### Example 2: Importing a Custom Namespace

using SampleNamespace;

### 3\. File-Scoped Namespace (C# 10 and Later)

In C# 10, you can use a simpler syntax to declare namespaces for all types in a file.

#### Example

namespace SampleNamespace;

class AnotherSampleClass

{

public void AnotherSampleMethod()

{

System.Console.WriteLine("AnotherMethod inside SampleNamespace");

}

}

class Program

{

static void Main()

{

AnotherSampleClass sample = new AnotherSampleClass();

sample.AnotherSampleMethod();

}

}

- The namespace declaration applies to the entire file without needing braces.

- This style saves horizontal space and improves readability.

### 4\. Properties of Namespaces

1. Organize Large Codebases:\
   Group related classes, interfaces, and methods to make large projects manageable.

2. Delimiter:\
   Use the . operator to access nested namespaces and their members.

   using System.Text;

StringBuilder sb = new StringBuilder("Hello");

1. using Directive:\
   Simplifies code by avoiding the need to write fully qualified names.

2. Global Namespace:\
   The global:: prefix refers to the root namespace in the .NET library.

   global::System.Console.WriteLine("Hello from global namespace");

* * * * *

### When to Use Namespaces

- To prevent naming conflicts in large projects.

- To organize code logically into modules.

- To make the code more readable and maintainable.

Namespaces are an essential tool for structuring code, especially in large-scale applications. They ensure that your
classes and methods do not conflict with those from other libraries or parts of your project.