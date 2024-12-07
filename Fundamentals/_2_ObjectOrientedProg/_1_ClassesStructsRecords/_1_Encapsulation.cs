namespace CSharpOOPS.Fundamentals._2_ObjectOrientedProg._1_ClassesStructsRecords;

public class _1_Encapsulation
{
    private class Template1
    {
        private class BankAccount
        {
            // Private field to hold the balance

            // Public property to get the balance (read-only)
            public decimal Balance { get; private set; }

            // Method to deposit money (controlled access)
            public void Deposit(decimal amount)
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Deposit amount must be greater than zero.");
                    return;
                }

                Balance += amount;
                Console.WriteLine($"Deposited: {amount:C}. New Balance: {Balance:C}");
            }

            // Method to withdraw money (controlled access)
            public void Withdraw(decimal amount)
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Withdrawal amount must be greater than zero.");
                    return;
                }

                if (amount > Balance)
                {
                    Console.WriteLine("Insufficient funds.");
                    return;
                }

                Balance -= amount;
                Console.WriteLine($"Withdrew: {amount:C}. New Balance: {Balance:C}");
            }
        }

        private class Program
        {
            private static void Main()
            {
                var account = new BankAccount();

                // Attempt to directly modify the balance (not allowed)
                // account._balance = 1000; // Compilation error

                // Use methods to modify the balance
                account.Deposit(500); // Deposited: $500.00. New Balance: $500.00
                account.Withdraw(200); // Withdrew: $200.00. New Balance: $300.00
                account.Withdraw(400); // Insufficient funds.

                // Access balance using the read-only property
                Console.WriteLine($"Final Balance: {account.Balance:C}");
            }
        }
    }
}