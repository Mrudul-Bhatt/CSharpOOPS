namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._4_Polymorphism;

public class _2_Polymorphism2
{
    private class Template1
    {
        private void Main()
        {
            // Usage:
            BaseClass baseObj = new DerivedClass();
            baseObj.DoWork(); // Output: DerivedClass DoWork
        }

        public class BaseClass
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseClass DoWork");
            }
        }

        public class DerivedClass : BaseClass
        {
            public override void DoWork()
            {
                Console.WriteLine("DerivedClass DoWork");
            }
        }
    }

    private class Template2
    {
        private void Main()
        {
            // Usage:
            BaseClass obj = new AnotherDerivedClass();
            obj.DoWork(); // Output: BaseClass DoWork
        }

        public class BaseClass
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseClass DoWork");
            }
        }

        public class AnotherDerivedClass : BaseClass
        {
            // Inherits DoWork() from BaseClass without changes.
        }
    }

    private class Template3
    {
        private void Main()
        {
            // Usage:
            var hiddenObj = new HiddenDerivedClass();
            hiddenObj.DoWork(); // Output: HiddenDerivedClass DoWork

            BaseClass baseHiddenObj = hiddenObj;
            baseHiddenObj.DoWork(); // Output: BaseClass DoWork
        }

        public class BaseClass
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseClass DoWork");
            }
        }

        public class HiddenDerivedClass : BaseClass
        {
            public new void DoWork()
            {
                Console.WriteLine("HiddenDerivedClass DoWork");
            }
        }
    }

    private class Template4
    {
        private void Main()
        {
        }

        public class BaseClass
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseClass DoWork");
            }
        }

        public class SealedBaseClass : BaseClass
        {
            public sealed override void DoWork()
            {
                Console.WriteLine("SealedBaseClass DoWork");
            }
        }

        // Attempting to override in a further derived class will cause a compilation error.
        public class FurtherDerivedClass : SealedBaseClass
        {
            // Error: Cannot override a sealed method
            // public override void DoWork() { }
        }
    }

    private class Template5
    {
        private void Main()
        {
            // Usage:
            BaseWithLogging obj = new DerivedWithLogging();
            obj.DoWork();
            // Output:
            // DerivedWithLogging Pre-Work
            // BaseWithLogging DoWork
            // DerivedWithLogging Post-Work
        }

        public class BaseWithLogging
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseWithLogging DoWork");
            }
        }

        public class DerivedWithLogging : BaseWithLogging
        {
            public override void DoWork()
            {
                Console.WriteLine("DerivedWithLogging Pre-Work");
                base.DoWork(); // Call the base class implementation
                Console.WriteLine("DerivedWithLogging Post-Work");
            }
        }
    }

    private class Template6
    {
        private void Main()
        {
            // Usage:
            var anotherObj = new AnotherClass();
            anotherObj.DoWork(); // Output: AnotherClass DoWork

            SealedClass sealedObj = anotherObj;
            sealedObj.DoWork(); // Output: SealedClass DoWork
        }

        public class BaseClass
        {
            public virtual void DoWork()
            {
                Console.WriteLine("BaseClass DoWork");
            }
        }

        public class SealedClass : BaseClass
        {
            public sealed override void DoWork()
            {
                Console.WriteLine("SealedClass DoWork");
            }
        }

        public class AnotherClass : SealedClass
        {
            public new void DoWork()
            {
                Console.WriteLine("AnotherClass DoWork");
            }
        }
    }
}