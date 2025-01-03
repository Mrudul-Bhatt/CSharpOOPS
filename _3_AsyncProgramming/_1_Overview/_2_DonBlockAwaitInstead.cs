using System;
using System.Threading.Tasks;

namespace CSharpOOPS._3_AsyncProgramming._1_Overview._2_DontBlockAwaitInstead
{
    // These classes are intentionally empty for the purpose of this example.
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

    class Program
    {
        static async Task Main(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("Coffee is ready");

            Egg eggs = await FryEggsAsync(2);
            Console.WriteLine("Eggs are ready");

            Bacon bacon = await FryBaconAsync(3);
            Console.WriteLine("Bacon is ready");

            Toast toast = await ToastBreadAsync(2);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("Toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("Orange juice is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(3000); // Simulates toasting
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine($"Putting {slices} slices of bacon in the pan");
            Console.WriteLine("Cooking first side of bacon...");
            await Task.Delay(3000); // Simulates cooking
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Flipping a slice of bacon");
            }
            Console.WriteLine("Cooking the second side of bacon...");
            await Task.Delay(3000); // Simulates cooking
            Console.WriteLine("Put bacon on plate");

            return new Bacon();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000); // Simulates warming
            Console.WriteLine($"Cracking {howMany} eggs");
            Console.WriteLine("Cooking the eggs...");
            await Task.Delay(3000); // Simulates cooking
            Console.WriteLine("Put eggs on plate");

            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
    }
}
