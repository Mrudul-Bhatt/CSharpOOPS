namespace CSharpOOPS.Fundamentals._4_FunctionalTechniques._1_PatternMatching;

public class _4_MultipleInputMatching
{
    private class Template1
    {
        public decimal CalculateDiscount(Order order)
        {
            return order switch
            {
                { Items: > 10, Cost: > 1000.00m } => 0.10m, // High quantity and high cost
                { Items: > 5, Cost: > 500.00m } => 0.05m, // Moderate quantity and cost
                { Cost: > 250.00m } => 0.02m, // Moderate cost
                null => throw new ArgumentNullException(nameof(order), "Order cannot be null"), // Null check
                _ => 0m // Default: no discount
            };
        }

        private void Main()
        {
            // Usage:
            var discount1 = CalculateDiscount(new Order(15, 1500.00m)); // Output: 0.10m
            var discount2 = CalculateDiscount(new Order(7, 600.00m)); // Output: 0.05m
            var discount3 = CalculateDiscount(new Order(3, 300.00m)); // Output: 0.02m
            var discount4 = CalculateDiscount(new Order(1, 50.00m)); // Output: 0m
        }

        public record Order(int Items, decimal Cost);
    }

    private class Template2
    {
        public decimal CalculateDiscount(Order order)
        {
            return order switch
            {
                (> 10, > 1000.00m) => 0.10m, // High quantity and high cost
                (> 5, > 500.00m) => 0.05m, // Moderate quantity and cost
                (_, > 250.00m) => 0.02m, // Ignore Items, check Cost
                null => throw new ArgumentNullException(nameof(order), "Order cannot be null"),
                _ => 0m // Default: no discount
            };
        }

        public decimal CalculateDiscount2(Order order)
        {
            return order switch
            {
                { Items: > 10, Cost: > 1000.00m } => 0.10m,
                (> 5, > 500.00m) => 0.05m, // Positional pattern
                { Cost: > 250.00m } => 0.02m,
                null => throw new ArgumentNullException(nameof(order), "Order cannot be null"),
                _ => 0m
            };
        }

        private void Main()
        {
            // Usage:
            var discount1 = CalculateDiscount(new Order(15, 1500.00m)); // Output: 0.10m
            var discount2 = CalculateDiscount(new Order(7, 600.00m)); // Output: 0.05m
            var discount3 = CalculateDiscount(new Order(3, 300.00m)); // Output: 0.02m
            var discount4 = CalculateDiscount(new Order(1, 50.00m)); // Output: 0m
        }

        public record Order(int Items, decimal Cost);
    }
}