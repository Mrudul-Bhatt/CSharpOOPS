using SampleNamespace;

namespace CSharpOOPS.Fundamentals.TypeSystem._2_Namespaces._1_Namespaces
{
    public class _1_Namespaces
    {
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