namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._3_Inheritance;

public class _1_Inheritance
{
    private class Template1
    {
        public class WorkItem
        {
            private static int currentID;

            // Static constructor
            static WorkItem()
            {
                currentID = 0;
            }

            // Default constructor
            public WorkItem()
            {
                ID = 0;
                Title = "Default Title";
                Description = "Default Description";
                JobLength = new TimeSpan();
            }

            // Parameterized constructor
            public WorkItem(string title, string description, TimeSpan jobLength)
            {
                ID = GetNextID();
                Title = title;
                Description = description;
                JobLength = jobLength;
            }

            protected int ID { get; }
            protected string Title { get; set; }
            protected string Description { get; set; }
            protected TimeSpan JobLength { get; set; }

            // Increment and get next ID
            protected int GetNextID()
            {
                return ++currentID;
            }

            // Update method
            public void Update(string title, TimeSpan jobLength)
            {
                Title = title;
                JobLength = jobLength;
            }

            // Override ToString
            public override string ToString()
            {
                return $"{ID} - {Title}";
            }
        }

        public class ChangeRequest : WorkItem
        {
            // Default constructor
            public ChangeRequest()
            {
            }

            // Parameterized constructor
            public ChangeRequest(string title, string description, TimeSpan jobLength, int originalID)
                : base(title, description, jobLength)
            {
                OriginalItemID = originalID;
            }

            protected int OriginalItemID { get; set; }
        }

        private class Program
        {
            private static void Main()
            {
                // Creating an instance of WorkItem
                var item = new WorkItem("Fix Bugs", "Fix all bugs in code", new TimeSpan(2, 0, 0));
                Console.WriteLine(item.ToString());

                // Creating an instance of ChangeRequest
                var change = new ChangeRequest("Add Features", "Implement new features", new TimeSpan(3, 0, 0), 1);

                // Using inherited method
                change.Update("Update Feature Implementation", new TimeSpan(4, 0, 0));

                // Display the ChangeRequest
                Console.WriteLine(change.ToString());
            }
        }
    }
}