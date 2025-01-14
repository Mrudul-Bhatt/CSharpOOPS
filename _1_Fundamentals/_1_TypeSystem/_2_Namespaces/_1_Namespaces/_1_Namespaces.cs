using SampleNamespace;

namespace CSharpOOPS.Fundamentals.TypeSystem._2_Namespaces._1_Namespaces
{
    public class _1_Namespaces
    {
        // 1. Organizing Classes in the .NET Framework
        // Namespaces group related classes in the .NET library. For example:
        // System.Console.WriteLine("Hello World!");
        // System is the namespace, and Console is a class within it.
        // To avoid typing the full namespace path repeatedly, you can use the using directive:
        // using System;
        // Console.WriteLine("Hello World!");
        private void Example()
        {
            // System.Console.WriteLine("Hello World!");

            // using System;
            // Console.WriteLine("Hello World!");
        }

        private class Program
        {
            private static void Main()
            {
                // Use the fully qualified name to access the class
                var sample = new SampleClass();
                sample.SampleMethod();
            }
        }
    }
}

namespace SampleNamespace
{
    internal class SampleClass
    {
        public void SampleMethod()
        {
            Console.WriteLine("SampleMethod inside SampleNamespace");
        }
    }
}