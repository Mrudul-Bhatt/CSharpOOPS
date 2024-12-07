namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._1_ClassesStructsRecords;

public class _2_MembersOfClass
{
    private class Template1
    {
        private class Person
        {
            // Constant
            public const string Species = "Homo Sapiens";

            // Fields
            private readonly string firstName;
            private readonly string lastName;

            // Constructor
            public Person(string firstName, string lastName)
            {
                this.firstName = firstName;
                this.lastName = lastName;
            }

            // Property
            public string FullName => $"{firstName} {lastName}";

            // Method
            public void Introduce()
            {
                Console.WriteLine($"Hi, I am {FullName}.");
            }

            // Event
            public event EventHandler Greeted;

            public void Greet()
            {
                Console.WriteLine($"Hello! I am {FullName}.");
                Greeted?.Invoke(this, EventArgs.Empty);
            }

            // Finalizer
            ~Person()
            {
                Console.WriteLine($"Person object {FullName} is being finalized.");
            }

            // Nested Type
            public class Address
            {
                public string Street { get; set; }
                public string City { get; set; }
            }
        }

        private class Program
        {
            private static void Main()
            {
                // Create an instance of the Person class
                Person person = new("John", "Doe");

                // Accessing a property
                Console.WriteLine(person.FullName);

                // Calling a method
                person.Introduce();

                // Using a nested type
                var address = new Person.Address
                {
                    Street = "123 Main St",
                    City = "Springfield"
                };
                Console.WriteLine($"Address: {address.Street}, {address.City}");

                // Raising an event
                person.Greeted += (s, e) => Console.WriteLine("Person was greeted!");
                person.Greet();
            }
        }
    }
}