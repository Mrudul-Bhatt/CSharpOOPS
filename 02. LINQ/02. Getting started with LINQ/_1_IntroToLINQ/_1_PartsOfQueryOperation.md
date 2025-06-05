### **Query Operations in LINQ**

A LINQ query consists of three distinct actions that work together to retrieve and transform data from a data source.
Here's an explanation of each step, with examples.

* * * * *

### **1\. Obtain the Data Source**

The data source can be any collection of data, such as an array, list, database table, or XML document. In LINQ, the
data source must implement the `IEnumerable<T>` or `IQueryable<T>` interface.

**Example: Integer Array as Data Source**

```
// Data source
int[] numbers = { 0, 1, 2, 3, 4, 5, 6 };

```

Here, `numbers` is a simple integer array that acts as the data source.

* * * * *

### **2\. Create the Query**

A query is an expression that defines the logic for retrieving data. In LINQ, you use query syntax (
`from ... in ... where ... select ...`) or method syntax (`Where()`, `Select()`) to create queries.

**Example: Create a Query**

```
// Query creation: Select even numbers
var numQuery =
    from num in numbers
    where (num % 2) == 0
    select num;

```

In this query:

- **`from num in numbers`**: Iterates through the `numbers` array.
- **`where (num % 2) == 0`**: Filters the numbers to include only even ones.
- **`select num`**: Specifies the values to return (in this case, the even numbers).

* * * * *

### **3\. Execute the Query**

Queries are executed when you iterate over the query variable, such as in a `foreach` loop. LINQ uses deferred
execution, meaning the query logic is not processed until you enumerate the results.

**Example: Execute the Query**

```
// Query execution
foreach (int num in numQuery)
{
    Console.Write("{0,1} ", num);
}

```

**Output:**

```
0 2 4 6

```

* * * * *

### **Key Concepts**

- **Deferred Execution**: The query is not executed at the time of its creation. Instead, it runs when the data is
  enumerated (e.g., in a `foreach` loop).
- **Consistency Across Data Sources**: The same LINQ query patterns apply to arrays, lists, XML, databases, or other
  formats with a LINQ provider.

* * * * *

### **Example with Another Data Source: LINQ to XML**

The same principles apply to XML data:

```
using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // Data source: XML
        string xml = @"<Books>
                         <Book>
                           <Title>Book 1</Title>
                           <Price>500</Price>
                         </Book>
                         <Book>
                           <Title>Book 2</Title>
                           <Price>300</Price>
                         </Book>
                       </Books>";

        XElement books = XElement.Parse(xml);

        // Query creation
        var bookQuery =
            from book in books.Elements("Book")
            where (int)book.Element("Price") > 400
            select book.Element("Title").Value;

        // Query execution
        foreach (var title in bookQuery)
        {
            Console.WriteLine(title);
        }
    }
}

```

**Output:**

```
Book 1

```

* * * * *

### **Conclusion**

LINQ queries follow a consistent three-step process:

1. Define the data source.
2. Create the query using declarative syntax.
3. Execute the query through enumeration.

This process provides a uniform and powerful way to query data from a variety of sources while leveraging the type
safety and readability of C#.