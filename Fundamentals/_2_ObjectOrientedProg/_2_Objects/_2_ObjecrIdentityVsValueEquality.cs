namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._2_Objects;

public class _2_ObjecrIdentityVsValueEquality
{
    private class Template1
    {
        public class Person
        {
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class Program
        {
            private static void Main()
            {
                // Create two separate instances
                var person1 = new Person("Alice", 30);
                var person2 = new Person("Alice", 30);

                // Compare references (identity)
                if (ReferenceEquals(person1, person2))
                    Console.WriteLine("person1 and person2 are the same object.");
                else
                    Console.WriteLine("person1 and person2 are different objects.");
            }
        }
    }

    private class Template2
    {
        public struct Person
        {
            public string Name;
            public int Age;

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        private class Program
        {
            private static void Main()
            {
                // Create two struct instances with the same values
                var p1 = new Person("Bob", 40);
                var p2 = new Person("Bob", 40);

                // Compare values
                if (p1.Equals(p2))
                    Console.WriteLine("p1 and p2 have the same values.");
                else
                    Console.WriteLine("p1 and p2 have different values.");
            }
        }
    }

    private class Template3
    {
        public class Person : IEquatable<Person>
        {
            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; }
            public int Age { get; }

            public bool Equals(Person other)
            {
                if (other == null) return false;
                return Name == other.Name && Age == other.Age;
            }

            public override bool Equals(object obj)
            {
                if (obj is Person other)
                    return Equals(other);
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }

        private class Program
        {
            private static void Main()
            {
                var person1 = new Person("Charlie", 25);
                var person2 = new Person("Charlie", 25);

                // Compare values
                if (person1.Equals(person2))
                    Console.WriteLine("person1 and person2 have the same values.");
                else
                    Console.WriteLine("person1 and person2 have different values.");
            }
        }
    }

    private class Template4
    {
        public record Person(string Name, int Age);

        private class Program
        {
            private static void Main()
            {
                var person1 = new Person("Dana", 35);
                var person2 = new Person("Dana", 35);

                // Compare records for value equality
                if (person1 == person2)
                    Console.WriteLine("person1 and person2 have the same values.");
                else
                    Console.WriteLine("person1 and person2 have different values.");
            }
        }
    }
}