### **What is a Query Expression?**

A **query expression** is a concise and declarative syntax in LINQ used to query data sources in a way similar to SQL or
XQuery. It allows you to write clear and readable queries that filter, sort, group, or transform data. Query expressions
are first-class language constructs in C#, meaning they can be used anywhere a valid C# expression is allowed.

* * * * *

### **Structure of a Query Expression**

A query expression must:

1. **Start with a `from` clause** -- Specifies the data source.
2. **End with a `select` or `group` clause** -- Defines the final output of the query.

Between these clauses, you can include optional elements such as:

- **`where`** -- Filters the data.
- **`orderby`** -- Sorts the data.
- **`join`** -- Combines data from multiple sources.
- **`let`** -- Introduces a new range variable for use in the query.
- **`into`** -- Allows chaining additional operations after a `group` or `join`.

* * * * *

### **Query Variables**

A **query variable** is a variable that stores a query expression instead of the actual results. It represents an
enumerable collection that can be executed later to produce data. The query is executed only when iterated (e.g., in a
`foreach` loop or with methods like `ToList()`).

**Example of a Query Variable:**

```
int[] scores = { 90, 71, 82, 93, 75, 82 };

IEnumerable<int> scoreQuery =
    from score in scores
    where score > 80
    orderby score descending
    select score;

// Query execution
foreach (var score in scoreQuery)
{
    Console.WriteLine(score);
}

```

**Output:**

```
93
90
82
82

```

* * * * *

### **Examples**

### **1\. Basic Query Expression**

Query to filter cities with a population greater than 15 million.

```
City[] cities = {
    new City("Tokyo", 37_833_000),
    new City("Delhi", 30_290_000),
    new City("Shanghai", 27_110_000),
    new City("São Paulo", 22_043_000)
};

IEnumerable<City> majorCities =
    from city in cities
    where city.Population > 15_000_000
    orderby city.Population descending
    select city;

// Execution
foreach (var city in majorCities)
{
    Console.WriteLine($"{city.Name}: {city.Population}");
}

```

**Output:**

```
Tokyo: 37833000
Delhi: 30290000
Shanghai: 27110000
São Paulo: 22043000

```

* * * * *

### **2\. Nested Query with `from`**

Query to retrieve all cities with a population greater than 10,000 from multiple countries.

```
IEnumerable<City> largeCities =
    from country in countries
    from city in country.Cities
    where city.Population > 10_000
    select city;

// Execution
foreach (var city in largeCities)
{
    Console.WriteLine($"{city.Name}: {city.Population}");
}

```

**Output:**

```
Vatican City: 826
Monte Carlo: 38000
Funafuti: 6200
Majuro: 28000

```

* * * * *

### **3\. Transformation using `select`**

Transform cities into a string representation.

```
IEnumerable<string> cityDescriptions =
    from city in cities
    select $"{city.Name} has a population of {city.Population}.";

foreach (var description in cityDescriptions)
{
    Console.WriteLine(description);
}

```

**Output:**

```
Tokyo has a population of 37833000.
Delhi has a population of 30290000.
Shanghai has a population of 27110000.
São Paulo has a population of 22043000.

```

* * * * *

### **4\. Method Syntax Comparison**

Query expressions can be rewritten in **method-based syntax**.

**Query Syntax:**

```
IEnumerable<City> majorCities =
    from city in cities
    where city.Population > 15_000_000
    select city;

```

**Method Syntax:**

```
IEnumerable<City> majorCities = cities.Where(city => city.Population > 15_000_000);

```

* * * * *

### **Lazy Execution vs Immediate Execution**

### **Lazy Execution**

Query variables do not store results; they represent a query definition. Results are fetched only when the query is
executed, such as when iterating through it.

Example:

```
IEnumerable<int> scoreQuery =
    from score in scores
    where score > 80
    select score;

scores[0] = 95; // Modify the data source

// Query execution reflects updated data
foreach (var score in scoreQuery)
{
    Console.WriteLine(score);
}

```

### **Immediate Execution**

If you want to store the results immediately, you must use methods like `ToList()` or `ToArray()`.

Example:

```
var scoreList = (
    from score in scores
    where score > 80
    select score
).ToList();

```

* * * * *

### **Key Points to Remember**

1. **Declarative Syntax**: Query expressions use a declarative approach, describing *what* to retrieve rather than *how*
   to retrieve it.
2. **First-Class Citizen**: Query expressions can be used wherever any C# expression is valid.
3. **Composable**: You can chain multiple clauses in a query for filtering, sorting, grouping, or transforming data.
4. **Execution**: Queries are executed only when results are explicitly requested, allowing for efficient data
   manipulation.
5. **Flexibility**: Query syntax can often be replaced by equivalent method-based syntax.

* * * * *

### **Practical Use Case**

Filter cities by population and transform them into a descriptive format.

```
var citySummaries = (
    from country in countries
    from city in country.Cities
    where city.Population > 5_000
    orderby city.Population descending
    select $"{city.Name}, Population: {city.Population}"
).ToList();

citySummaries.ForEach(Console.WriteLine);

```