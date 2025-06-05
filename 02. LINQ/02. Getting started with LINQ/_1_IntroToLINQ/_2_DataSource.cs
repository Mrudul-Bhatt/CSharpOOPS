using System.Xml.Linq;

namespace CSharpOOPS._2_LINQ._2_GettingStartedWithLINQ._1_IntroToLINQ;

public class _2_DataSource
{
    //1. LINQ with an In-Memory Array
    private class Template1
    {
        private void Main()
        {
            // In-memory data source: an integer array
            int[] numbers = { 1, 2, 3, 4, 5, 6 };

            // Query to select even numbers
            var evenNumbers =
                from num in numbers
                where num % 2 == 0
                select num;

            // Execute the query
            foreach (var number in evenNumbers) Console.WriteLine(number);

            // Output:
            // 2
            // 4
            // 6
        }
    }

    // 2. LINQ with XML Using LINQ to XML
    private class Template2
    {
        private void Main()
        {
            // Load an XML document as a data source
            var contacts = XElement.Parse(@"
                                    <Contacts>
                                        <Contact>
                                            <Name>John Doe</Name>
                                            <City>London</City>
                                        </Contact>
                                        <Contact>
                                            <Name>Jane Smith</Name>
                                            <City>New York</City>
                                        </Contact>
                                    </Contacts>");

            // Query contacts from London
            var londonContacts =
                from contact in contacts.Elements("Contact")
                where contact.Element("City").Value == "London"
                select contact.Element("Name").Value;

            // Execute the query
            foreach (var name in londonContacts) Console.WriteLine(name);

            // Output:
            // John Doe
        }
    }

    // 3. LINQ with Databases Using Entity Framework
    private class Template3
    {
        private void Main()
        {
            // using (var db = new Northwnd(@"c:\northwnd.mdf"))
            // {
            //     // Query: Retrieve customers in London
            //     var londonCustomers =
            //         from customer in db.Customers
            //         where customer.City == "London"
            //         select customer;
            //
            //     // Execute and display results
            //     foreach (var cust in londonCustomers) Console.WriteLine($"{cust.CustomerID} - {cust.CompanyName}");
            // }
        }
    }
}