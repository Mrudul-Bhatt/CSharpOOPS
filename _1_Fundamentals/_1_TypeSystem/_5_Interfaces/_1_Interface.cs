namespace CSharpOOPS.Fundamentals.TypeSystem._5_Interfaces;

public class _1_Interface
{
    private class Template1
    {
        private interface IEquatable<T>
        {
            bool Equals(T obj);
        }

        public class Car : IEquatable<Car>
        {
            public string? Make { get; }
            public string? Model { get; }
            public string? Year { get; }

            // Implementation of IEquatable<Car>
            public bool Equals(Car? car)
            {
                return (Make, Model, Year) ==
                       (car?.Make, car?.Model, car?.Year);
            }
        }
    }

    private class Template2
    {
        private interface IPrinter
        {
            void Print();
        }

        private interface IScanner
        {
            void Print();
        }

        public class MultiFunctionDevice : IPrinter, IScanner
        {
            void IPrinter.Print()
            {
                Console.WriteLine("Printing as a Printer.");
            }

            void IScanner.Print()
            {
                Console.WriteLine("Printing as a Scanner.");
            }
        }
    }

    private class Template3
    {
        private interface ILogger
        {
            void Log(string message)
            {
                Console.WriteLine($"Default Logger: {message}");
            }
        }

        private class CustomLogger : ILogger
        {
        }

        public class Program
        {
            public static void Main()
            {
                ILogger logger = new CustomLogger();
                logger.Log("Hello, World!"); // Output: Default Logger: Hello, World!
            }
        }
    }

    private class Template4
    {
        private interface IBase
        {
            void BaseMethod();
        }

        private interface IDerived : IBase
        {
            void DerivedMethod();
        }

        private class Implementation : IDerived
        {
            public void BaseMethod()
            {
                Console.WriteLine("Base method implementation.");
            }

            public void DerivedMethod()
            {
                Console.WriteLine("Derived method implementation.");
            }
        }
    }

    private class Template5
    {
        private interface IShape
        {
            double GetArea();
        }

        private struct Circle : IShape
        {
            public double Radius { get; }

            public double GetArea()
            {
                return Math.PI * Radius * Radius;
            }
        }
    }

    private class Template6
    {
        private interface INotificationService
        {
            void SendNotification(string message);
        }

        private class EmailNotification : INotificationService
        {
            public void SendNotification(string message)
            {
                Console.WriteLine($"Email sent: {message}");
            }
        }

        private class SmsNotification : INotificationService
        {
            public void SendNotification(string message)
            {
                Console.WriteLine($"SMS sent: {message}");
            }
        }

        private class NotificationManager
        {
            private readonly INotificationService _service;

            public NotificationManager(INotificationService service)
            {
                _service = service;
            }

            public void Notify(string message)
            {
                _service.SendNotification(message);
            }
        }
    }
}