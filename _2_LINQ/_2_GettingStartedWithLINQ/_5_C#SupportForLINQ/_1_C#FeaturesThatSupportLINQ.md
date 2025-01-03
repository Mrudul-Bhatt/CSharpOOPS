### **C# Features That Support LINQ**

LINQ (Language Integrated Query) utilizes several C# features that enable concise, readable, and flexible querying of
data. These features collectively simplify working with data collections and databases.

---

### **1\. Query Expressions**

Query expressions offer a declarative, SQL-like syntax for querying data in C#.

- **How It Works:**
  - The query syntax is compiled into method calls for a LINQ provider's implementation of standard query methods (e.g.,
    `Where`, `GroupBy`, `Select`).
  - The `using` directive specifies the namespace for the LINQ methods, such as `System.Linq`.

### **Example: Group and Order Strings**

```
var query = from str in stringArray
            group str by str[0] into stringGroup
            orderby stringGroup.Key
            select stringGroup;

```

- Groups strings by their first character (`str[0]`) and orders the groups by their keys.

---

### **2\. Implicitly Typed Variables (`var`)**

The `var` keyword allows the compiler to infer the type of a variable based on the assigned value.

### **Example: Query with `var`**

```
var query = from str in stringArray
            where str[0] == 'm'
            select str;

```

- `query` is strongly typed as `IEnumerable<string>`.
- Makes it easier to work with anonymous types and complex queries.

---

### **3\. Object and Collection Initializers**

These enable creating and initializing objects or collections in a single step, without explicitly calling a
constructor.

### **Object Initializer Example**

```
var cust = new Customer { Name = "Mike", Phone = "555-1212" };

```

### **Query with Object Initialization**

```
var newLargeOrderCustomers = from o in IncomingOrders
                             where o.OrderSize > 5
                             select new Customer { Name = o.Name, Phone = o.Phone };

```

- Projects a subset of the source data (`IncomingOrders`) into a collection of `Customer` objects.

### **Method Syntax Equivalent**

```
var newLargeOrderCustomers = IncomingOrders
    .Where(x => x.OrderSize > 5)
    .Select(y => new Customer { Name = y.Name, Phone = y.Phone });

```

---

### **4\. Anonymous Types**

Anonymous types are used to group properties without defining a named class.

### **Example: Anonymous Type in Query**

```
var query = from cust in customers
            select new { Name = cust.Name, Phone = cust.Phone };

```

- The compiler generates a temporary, unnamed type for the result.

### **Tuples (C# 7+)**

You can use tuples as an alternative to anonymous types.

```
var query = from cust in customers
            select (cust.Name, cust.Phone);

```

---

### **5\. Extension Methods**

Extension methods add new functionality to existing types without modifying them.

- LINQ's standard query operators (`Where`, `Select`, etc.) are implemented as extension methods for types implementing
  `IEnumerable<T>`.

### **Example: LINQ Method Syntax**

```
var filtered = stringArray.Where(str => str.StartsWith("m"));

```

---

### **6\. Lambda Expressions**

Lambda expressions are inline functions used in LINQ for concise representation of operations.

### **Example: Lambda with LINQ**

```
var query = stringArray.Where(s => s.Length > 3).Select(s => s.ToUpper());

```

- `s => s.Length > 3`: Filters strings longer than 3 characters.
- `s => s.ToUpper()`: Converts strings to uppercase.

---

### **7\. Query Composition**

LINQ queries are composable, meaning you can build or modify queries dynamically.

### **Returning Queries**

A method can return a query object instead of its results. The query is executed only when enumerated.

### **Example: Returning a Query**

```
IEnumerable<string> QueryMethod(int[] nums) =>
    from n in nums
    where n > 4
    select n.ToString();

```

```
var query = QueryMethod(numbers); // Returns the query.
foreach (var s in query)          // Executes the query.
{
    Console.WriteLine(s);
}

```

### **Modifying Queries**

You can modify a query by further composing it.

```
myQuery1 = from item in myQuery1
           orderby item descending
           select item;

```

---

### **8\. Expressions as Data**

LINQ queries are not executed immediately; they represent an expression tree that describes how to fetch or transform
data.

### **Deferred Execution**

- A LINQ query is executed only when it is enumerated (e.g., in a `foreach` loop or by calling `.ToList()` or
  `.ToArray()`).

---

### **Key Benefits of LINQ Features**

1.  **Declarative Syntax:** Query expressions make code easier to read and write.
2.  **Strong Typing:** Features like `var` and generics ensure type safety at compile time.
3.  **Dynamic Querying:** Query composition allows dynamic and flexible query building.
4.  **Efficient Data Projection:** Anonymous types and object initializers help shape data to specific requirements.

By combining these features, LINQ offers a robust and flexible way to query and manipulate data across various data
sources.
