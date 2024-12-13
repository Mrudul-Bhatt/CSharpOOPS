using System.Xml.Linq;

namespace CSharpOOPS._2_LINQ._2_GettingStartedWithLINQ._1_IntroToLINQ;

public class _1_PartsOfQueryOperation
{
    private class Template1
    {
        private class Program
        {
            private static void Main()
            {
                // Data source: XML
                var xml = @"<Books>
                         <Book>
                           <Title>Book 1</Title>
                           <Price>500</Price>
                         </Book>
                         <Book>
                           <Title>Book 2</Title>
                           <Price>300</Price>
                         </Book>
                       </Books>";

                var books = XElement.Parse(xml);

                // Query creation
                var bookQuery =
                    from book in books.Elements("Book")
                    where (int)book.Element("Price") > 400
                    select book.Element("Title").Value;

                // Query execution
                foreach (var title in bookQuery) Console.WriteLine(title);
            }
        }
    }
}