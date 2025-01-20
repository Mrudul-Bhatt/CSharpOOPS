### **Declaring, Instantiating, and Using Delegates in C#**

A **delegate** is a type that defines a method signature, allowing you to reference methods with that signature dynamically. This enables **callback functionality** and promotes loose coupling between components.

---

### **Declaring a Delegate**
To declare a delegate:
1. Use the `delegate` keyword.
2. Define the method signature the delegate will reference.

**Example:**
```csharp
// Declare a delegate with a specific method signature.
delegate void NotifyCallback(string str);
```

This declares a delegate type, `NotifyCallback`, which references methods that:
- Take a `string` parameter.
- Return `void`.

---

### **Instantiating a Delegate**
You can instantiate a delegate using several techniques:

#### 1. **Using a Named Method**
```csharp
// A method matching the delegate's signature.
static void Notify(string name)
{
    Console.WriteLine($"Notification received for: {name}");
}

// Instantiate the delegate with the named method.
NotifyCallback del1 = new NotifyCallback(Notify);
```

You can also simplify it:
```csharp
NotifyCallback del2 = Notify; // Direct assignment of the method group.
```

---

#### 2. **Using an Anonymous Method**
An **anonymous method** is a method declared without a name, using the `delegate` keyword.

**Example:**
```csharp
NotifyCallback del3 = delegate (string name)
{
    Console.WriteLine($"Notification received for: {name}");
};
```

---

#### 3. **Using a Lambda Expression**
**Lambda expressions** are concise, inline implementations of delegate-compatible methods.

**Example:**
```csharp
NotifyCallback del4 = name => Console.WriteLine($"Notification received for: {name}");
```

Lambda expressions are the most commonly used method for delegates in modern C# due to their simplicity.

---

### **Using a Delegate**
Once a delegate is instantiated, you can invoke it like a method:
```csharp
del1("Alice");
del2("Bob");
del3("Charlie");
del4("Daisy");
```

---

### **Practical Example: Bookstore System**

This example demonstrates how to declare, instantiate, and use delegates in a real-world scenario.

---

#### **Step 1: Declare the Delegate**
```csharp
// A delegate to process a book.
public delegate void ProcessBookCallback(Book book);
```

This delegate references methods that:
- Take a `Book` object as a parameter.
- Return `void`.

---

#### **Step 2: Implement a Book Database**
The `BookDB` class maintains a list of books and uses a delegate to process paperback books.

**Key Points:**
- **Encapsulation:** `BookDB` stores the list of books privately.
- **Delegate Usage:** The `ProcessPaperbackBooks` method accepts a `ProcessBookCallback` delegate and calls it for each paperback book.

```csharp
public class BookDB
{
    private List<Book> list = new(); // List of books.

    public void AddBook(string title, string author, decimal price, bool paperBack) =>
        list.Add(new Book(title, author, price, paperBack));

    public void ProcessPaperbackBooks(ProcessBookCallback processBook)
    {
        foreach (Book b in list)
        {
            if (b.Paperback)
            {
                // Invoke the delegate for each paperback book.
                processBook(b);
            }
        }
    }
}
```

---

#### **Step 3: Define Client-Side Logic**
The `Test` class demonstrates how to use `BookDB` and delegates to:
1. **Print paperback book titles.**
2. **Calculate the average price of paperback books.**

```csharp
class Test
{
    static void Main()
    {
        BookDB bookDB = new BookDB();

        // Add books to the database.
        AddBooks(bookDB);

        // Print paperback book titles.
        Console.WriteLine("Paperback Book Titles:");
        bookDB.ProcessPaperbackBooks(PrintTitle);

        // Calculate average price of paperback books.
        PriceTotaller totaller = new PriceTotaller();
        bookDB.ProcessPaperbackBooks(totaller.AddBookToTotal);

        Console.WriteLine($"Average Paperback Book Price: ${totaller.AveragePrice():#.##}");
    }

    static void PrintTitle(Book b) => Console.WriteLine($"   {b.Title}");

    static void AddBooks(BookDB bookDB)
    {
        bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
        bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, true);
        bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, false);
        bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, true);
    }
}
```

---

#### **Step 4: Implement the Totaller**
The `PriceTotaller` class accumulates the total price of paperback books and calculates their average price.

```csharp
class PriceTotaller
{
    private int countBooks = 0;
    private decimal priceBooks = 0.0m;

    public void AddBookToTotal(Book book)
    {
        countBooks++;
        priceBooks += book.Price;
    }

    public decimal AveragePrice() => priceBooks / countBooks;
}
```

---

### **Output**
```plaintext
Paperback Book Titles:
   The C Programming Language
   The Unicode Standard 2.0
   Dogbert's Clues for the Clueless
Average Paperback Book Price: $23.97
```

---

### **Advantages of Using Delegates**
1. **Loose Coupling:**
   - The `BookDB` class processes paperback books without knowing how they are handled.
   - The client defines the specific processing logic.

2. **Reusability:**
   - Delegates allow different processing logic for the same dataset.
   - Example: Printing titles vs. calculating average price.

3. **Flexibility:**
   - You can change the processing logic dynamically by passing a different delegate instance.

---

### **Conclusion**
Delegates in C# enable dynamic method references, making them powerful tools for designing flexible, reusable, and decoupled systems. Using anonymous methods or lambda expressions simplifies delegate usage in modern C#.