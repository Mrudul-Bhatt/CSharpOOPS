### **Projection Operations in C#**

Projection in LINQ refers to transforming data from one form into another. This is typically done to extract specific
properties or reshape objects, potentially combining or modifying values during the process.

Projection is implemented using standard query operators such as `Select`, `SelectMany`, and `Zip`.

---

### **Projection Methods**

| **Method Name** | **Description**                                                              | **Query Syntax**        | **More Information**                            |
| --------------- | ---------------------------------------------------------------------------- | ----------------------- | ----------------------------------------------- |
| `Select`        | Transforms elements based on a function.                                     | `select`                | `Enumerable.Select`, `Queryable.Select`         |
| `SelectMany`    | Transforms elements into sequences and flattens them into a single sequence. | Multiple `from` clauses | `Enumerable.SelectMany`, `Queryable.SelectMany` |
| `Zip`           | Combines two or more sequences into tuples.                                  | Not applicable.         | `Enumerable.Zip`, `Queryable.Zip`               |

---

### **1\. `Select` Method**

The `Select` method transforms each element in a collection based on a projection function.

### Example 1: Extracting the First Letter from Strings

**Query Syntax**:

```
List<string> words = new List<string> { "an", "apple", "a", "day" };

var query = from word in words
            select word.Substring(0, 1);

foreach (string s in query)
{
    Console.WriteLine(s);
}

// Output:
// a
// a
// a
// d

```

**Method Syntax**:

```
List<string> words = new List<string> { "an", "apple", "a", "day" };

var query = words.Select(word => word.Substring(0, 1));

foreach (string s in query)
{
    Console.WriteLine(s);
}

// Output:
// a
// a
// a
// d

```

---

### **2\. `SelectMany` Method**

The `SelectMany` method transforms each element into a collection of values and flattens the result into a single
sequence.

### Example 1: Splitting Phrases into Words

**Query Syntax**:

```
List<string> phrases = new List<string> { "an apple a day", "the quick brown fox" };

var query = from phrase in phrases
            from word in phrase.Split(' ')
            select word;

foreach (string s in query)
{
    Console.WriteLine(s);
}

// Output:
// an
// apple
// a
// day
// the
// quick
// brown
// fox

```

**Method Syntax**:

```
List<string> phrases = new List<string> { "an apple a day", "the quick brown fox" };

var query = phrases.SelectMany(phrase => phrase.Split(' '));

foreach (string s in query)
{
    Console.WriteLine(s);
}

// Output:
// an
// apple
// a
// day
// the
// quick
// brown
// fox

```

---

### **3\. `Zip` Method**

The `Zip` method combines elements from two or more sequences into tuples.

### Example 1: Combining Numbers and Letters

```
IEnumerable<int> numbers = new[] { 1, 2, 3, 4, 5, 6, 7 };
IEnumerable<char> letters = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };

foreach ((int number, char letter) in numbers.Zip(letters))
{
    Console.WriteLine($"Number: {number} zipped with letter: '{letter}'");
}

// Output:
// Number: 1 zipped with letter: 'A'
// Number: 2 zipped with letter: 'B'
// Number: 3 zipped with letter: 'C'
// Number: 4 zipped with letter: 'D'
// Number: 5 zipped with letter: 'E'
// Number: 6 zipped with letter: 'F'

```

### Example 2: Combining Three Sequences

```
IEnumerable<int> numbers = new[] { 1, 2, 3, 4, 5, 6 };
IEnumerable<char> letters = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
IEnumerable<string> emoji = new[] { "🤓", "🔥", "🎉", "👀", "⭐", "💜" };

foreach ((int number, char letter, string em) in numbers.Zip(letters, emoji))
{
    Console.WriteLine($"Number: {number} is zipped with letter: '{letter}' and emoji: {em}");
}

// Output:
// Number: 1 is zipped with letter: 'A' and emoji: 🤓
// Number: 2 is zipped with letter: 'B' and emoji: 🔥
// ...

```

---

### **`Select` vs. `SelectMany`**

- **`Select`**:
  - Returns a collection with the same number of elements as the source.
  - Example: Transforming strings into their lengths.
- **`SelectMany`**:
  - Returns a flattened sequence by concatenating subcollections from the source.
  - Example: Splitting strings into words and combining them into a single sequence.

### Visualizing the Difference

| **`Select`**                   | **`SelectMany`**            |
| ------------------------------ | --------------------------- |
| Produces nested collections.   | Produces a single sequence. |
| Example: `[["a", "b"], ["c"]]` | Example: `["a", "b", "c"]`  |

---

### **Code Example: Flowers**

```
class Bouquet
{
    public required List<string> Flowers { get; init; }
}

static void SelectVsSelectMany()
{
    List<Bouquet> bouquets = new List<Bouquet>
    {
        new Bouquet { Flowers = new List<string> { "sunflower", "daisy", "daffodil" } },
        new Bouquet { Flowers = new List<string> { "tulip", "rose", "orchid" } }
    };

    var query1 = bouquets.Select(bq => bq.Flowers);
    var query2 = bouquets.SelectMany(bq => bq.Flowers);

    Console.WriteLine("Using Select():");
    foreach (var collection in query1)
    {
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
    }

    Console.WriteLine("\\nUsing SelectMany():");
    foreach (var item in query2)
    {
        Console.WriteLine(item);
    }
}

// Output:
// Using Select():
// sunflower
// daisy
// daffodil
// tulip
// rose
// orchid

// Using SelectMany():
// sunflower
// daisy
// daffodil
// tulip
// rose
// orchid

```

---

### **Summary**

Projection operations in LINQ provide a way to transform and reshape data effectively:

- Use **`Select`** for one-to-one transformations.
- Use **`SelectMany`** for flattening nested collections.
- Use **`Zip`** for combining multiple sequences into tuples.
