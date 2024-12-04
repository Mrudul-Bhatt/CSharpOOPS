namespace CSharpOOPS.Fundamentals.TypeSystem._4_Records;

public class _1_RecordTypes
{
    private class Template1
    {
        private static void Main(string[] args)
        {
            var person1 = new Person("Alice", 30);
            var person2 = new Person("Alice", 30);

            Console.WriteLine(person1 == person2); // True, because their values are equal.

            // person1.Age = 1; Error : Records are immutable

            //To create a new record from existing one
            var person3 = person1 with { Age = 1 };
        }

        private record Person(string Name, int Age);
    }

    private class Template2
    {
        private static void Main()
        {
            var person1 = new PersonClass("Alice", 30);
            var person2 = new PersonClass("Alice", 30);

            Console.WriteLine(person1 == person2); // False, because they are different objects.
        }

        private class PersonClass
        {
            public PersonClass(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; init; }
            public int Age { get; init; }
        }
    }

    private class Template3
    {
        // Use classes for EF Core entities to ensure reference equality.
        public class Order
        {
            public int Id { get; set; }
            public string Product { get; set; }
        }
    }

    private class Template4
    {
        private record Person(string FirstName, string LastName)
        {
            public string[] PhoneNumbers { get; init; }
        }

        public static class Program
        {
            public static void Main()
            {
                Person person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
                var person2 = person1 with { FirstName = "John" };

                Console.WriteLine(person1);
                // Output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

                Console.WriteLine(person2);
                // Output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
            }
        }
    }
}