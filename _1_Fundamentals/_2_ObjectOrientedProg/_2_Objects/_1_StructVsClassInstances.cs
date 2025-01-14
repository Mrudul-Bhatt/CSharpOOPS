namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._2_Objects;

public class _1_StructVsClassInstances
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
                // Create an instance of the Person class
                var person1 = new Person("Leopold", 6);
                Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);

                // Assign person1 reference to person2
                var person2 = person1;

                // Modify person2
                person2.Name = "Molly";
                person2.Age = 16;

                // Both person1 and person2 reflect changes because they point to the same object
                Console.WriteLine("person2: Name = {0}, Age = {1}", person2.Name, person2.Age);
                Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);
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
                // Create an instance of the Person struct
                var person1 = new Person("Alex", 9);
                Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);

                // Copy the value of person1 into person2
                var person2 = person1;

                // Modify person2
                person2.Name = "Spencer";
                person2.Age = 7;

                // person1 remains unchanged
                Console.WriteLine("person2: Name = {0}, Age = {1}", person2.Name, person2.Age);
                Console.WriteLine("person1: Name = {0}, Age = {1}", person1.Name, person1.Age);
            }
        }
    }
}