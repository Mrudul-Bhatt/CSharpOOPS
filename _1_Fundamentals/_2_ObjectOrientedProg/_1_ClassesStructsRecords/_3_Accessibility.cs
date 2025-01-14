namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._1_ClassesStructsRecords;

public class _3_Accessibility
{
    //Public
    private class Template1
    {
        private void PrivateMethod()
        {
            // Usage:
            var obj = new PublicClass();
            obj.PublicMethod(); // Works
        }

        public class PublicClass
        {
            public void PublicMethod()
            {
                Console.WriteLine("Accessible from anywhere.");
            }
        }
    }

    private class Template2
    {
        private void PrivateMethod()
        {
            // Usage:
            var obj = new PrivateExample();
            // obj.PrivateMethod();  // Error: PrivateMethod is inaccessible
            obj.CallPrivateMethod(); // Indirectly calls the private method
        }

        public class PrivateExample
        {
            private void PrivateMethod()
            {
                Console.WriteLine("Accessible only within this class.");
            }

            public void CallPrivateMethod()
            {
                PrivateMethod(); // Allowed
            }
        }
    }

    private class Template3
    {
        private void PrivateMethod()
        {
            // Usage:
            var obj = new DerivedClass();
            obj.AccessProtectedMethod(); // Works
        }

        public class BaseClass
        {
            protected void ProtectedMethod()
            {
                Console.WriteLine("Accessible in derived classes.");
            }
        }

        public class DerivedClass : BaseClass
        {
            public void AccessProtectedMethod()
            {
                ProtectedMethod(); // Allowed
            }
        }
    }

    private class Template4
    {
        private void PrivateMethod()
        {
            // Usage (within the same assembly):
            var obj = new InternalClass();
            obj.InternalMethod(); // Works
            // From another assembly: Error
        }

        internal class InternalClass
        {
            public void InternalMethod()
            {
                Console.WriteLine("Accessible within the same assembly.");
            }
        }
    }

    private class Template5
    {
        private void PrivateMethod()
        {
            // Usage:
            var obj = new ProtectedInternalClass();
            obj.ProtectedInternalMethod(); // Works within the same assembly
        }

        public class ProtectedInternalClass
        {
            protected internal void ProtectedInternalMethod()
            {
                Console.WriteLine("Accessible within assembly or derived classes.");
            }
        }
    }

    private class Template6
    {
        private void PrivateMethod()
        {
            // Usage:
            var obj = new DerivedClass();
            obj.AccessPrivateProtectedMethod();
            // From outside: Error
        }

        public class BaseClass
        {
            private protected void PrivateProtectedMethod()
            {
                Console.WriteLine("Accessible in derived classes in the same assembly.");
            }
        }

        public class DerivedClass : BaseClass
        {
            public void AccessPrivateProtectedMethod()
            {
                PrivateProtectedMethod(); // Allowed within the same assembly
            }
        }
    }
}