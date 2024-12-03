using System;

namespace CSharpOOPS.Fundamentals.TypeSystem._3_Classes;

public class _1_ReferenceTypes
{

    class Template1
    {
        class MyClass
        {
            public int Value;
        }

        class Program
        {
            static void Main()
            {
                // Declare an object of type MyClass and initialize it using the new operator.
                MyClass mc = new MyClass();
                mc.Value = 42;

                // Declare another object of the same type and assign it the value of the first object.
                MyClass mc2 = mc;

                // Change the Value property through mc2.
                mc2.Value = 100;

                // Print the Value property of both objects.
                Console.WriteLine($"mc.Value: {mc.Value}");   // Output: 100
                Console.WriteLine($"mc2.Value: {mc2.Value}"); // Output: 100
            }
        }
    }

    class Template2
    {
        class MyClass
        {
            public int Value;
        }

        struct MyStruct
        {
            public int Value;
        }

        class Program
        {
            private void Reference()
            {
                MyClass obj1 = new MyClass { Value = 10 };
                MyClass obj2 = obj1;

                obj2.Value = 20;

                // Both values changed to 20
                Console.WriteLine(obj1.Value); // Output: 20
            }

            private void Value()
            {
                MyStruct struct1 = new MyStruct { Value = 10 };
                MyStruct struct2 = struct1;

                struct2.Value = 20;

                // Only second value changed, first value remains unaffected
                Console.WriteLine(struct1.Value); // Output: 10
            }
        }
    }
}

