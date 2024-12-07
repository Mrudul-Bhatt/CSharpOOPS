namespace CSharpOOPS.Fundamentals.TypeSystem._7_AnonymousTypes;

public class _1_AnonymousTypes
{
    // Simple Anonymous Type
    private class Template1
    {
        private static void Main()
        {
            var product = new { Name = "Laptop", Price = 999.99 };

            Console.WriteLine($"Product Name: {product.Name}");
            Console.WriteLine($"Product Price: ${product.Price}");
        }
    }

    //Using Anonymous Types in LINQ Queries

    private class Template2
    {
        private class Product
        {
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public string? Category { get; set; }
        }

        private class Program
        {
            private static void Main()
            {
                var products = new List<Product>
                {
                    new() { Name = "Laptop", Price = 1200, Category = "Electronics" },
                    new() { Name = "Table", Price = 150, Category = "Furniture" },
                    new() { Name = "Phone", Price = 800, Category = "Electronics" }
                };

                // Select specific properties using an anonymous type
                var electronics = from product in products
                    where product.Category == "Electronics"
                    select new { product.Name, product.Price };

                foreach (var item in electronics) Console.WriteLine($"Name: {item.Name}, Price: ${item.Price}");
            }
        }
    }

    //Nested Anonymous Types
    private class Template3
    {
        private static void Main()
        {
            var product = new { Name = "Laptop", Price = 1200 };
            var shipment = new { Address = "123 Street", Product = product };

            Console.WriteLine($"Shipment Address: {shipment.Address}");
            Console.WriteLine($"Product Name: {shipment.Product.Name}");
        }
    }

    //Non-Destructive Mutation with "with" Expressions
    private class Template4
    {
        private static void Main()
        {
            var original = new { Item = "Laptop", Price = 1200.00 };
            var discounted = original with { Price = 999.99 };

            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Discounted: {discounted}");
        }
    }

    //Array of Anonymous Types
    private class Template5
    {
        private static void Main()
        {
            var products = new[]
            {
                new { Name = "Laptop", Price = 1200 },
                new { Name = "Phone", Price = 800 },
                new { Name = "Tablet", Price = 600 }
            };

            foreach (var product in products) Console.WriteLine($"Name: {product.Name}, Price: ${product.Price}");
        }
    }
}