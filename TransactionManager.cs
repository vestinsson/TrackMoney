using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMoney
{
    internal class TransactionManager
    {
        private List<Transaction> transactions;

        public event Action TransactionsChanged;

        public TransactionManager(List<Transaction> transactions)
        {
            this.transactions = transactions;
        }
        public void AddTransaction()
        {
            try
            {
                Console.Write("Enter month (1-12): ");
                if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
                {
                    throw new ArgumentException("Invalid month. Please enter a number between 1 and 12.");
                }

                Console.Write("Enter amount: ");
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, new CultureInfo("sv-SE"), out decimal amount))
                {
                    throw new ArgumentException("Invalid amount. Please enter a valid decimal number.");
                }

                Console.Write("Is this an expense? (y/n): ");
                string expenseInput = Console.ReadLine().ToLower();
                if (expenseInput != "y" && expenseInput != "n")
                {
                    throw new ArgumentException("Invalid input for expense. Please enter 'y' or 'n'.");
                }
                bool isExpense = expenseInput == "y";

                transactions.Add(new Transaction { Month = new DateTime(2000, month, 1), Amount = Math.Abs(amount), IsExpense = isExpense });
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Transaction added successfully.");
                TransactionsChanged?.Invoke();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }
        public void EditOrRemoveTransaction()
        {
            try
            {
                for (int i = 0; i < transactions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {transactions[i]}");
                }

                Console.Write("Enter the number of the transaction to edit/remove: ");
                if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > transactions.Count)
                {
                    throw new ArgumentException("Invalid transaction number. Please enter a valid number.");
                }
                index--;

                string choice = Menu.GetEditOrRemoveChoice();

                if (choice == "r")
                {
                    transactions.RemoveAt(index);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Transaction removed successfully.");
                    TransactionsChanged?.Invoke();
                }
                else if (choice == "e")
                {
                    EditTransaction(index);
                    TransactionsChanged?.Invoke();
                }
                else
                {
                    throw new ArgumentException("Invalid choice. Please enter 'e' to edit or 'r' to remove.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private void EditTransaction(int index)
        {
            Transaction t = transactions[index];

            Console.Write($"Enter new month (1-12) ({t.Month:MM}): ");
            string monthInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(monthInput))
            {
                if (!int.TryParse(monthInput, out int newMonth) || newMonth < 1 || newMonth > 12)
                {
                    throw new ArgumentException("Invalid month. Please enter a number between 1 and 12.");
                }
                t.Month = new DateTime(2000, newMonth, 1);
            }

            Console.Write($"Enter new amount ({t.Amount.ToString("F2", new CultureInfo("sv-SE"))}): ");
            string amountInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(amountInput))
            {
                if (!decimal.TryParse(amountInput, NumberStyles.Number, new CultureInfo("sv-SE"), out decimal newAmount))
                {
                    throw new ArgumentException("Invalid amount. Please enter a valid decimal number.");
                }
                t.Amount = Math.Abs(newAmount);
            }

            Console.Write($"Is this an expense? (y/n) ({(t.IsExpense ? "y" : "n")}): ");
            string expenseInput = Console.ReadLine().ToLower();
            if (!string.IsNullOrEmpty(expenseInput))
            {
                if (expenseInput != "y" && expenseInput != "n")
                {
                    throw new ArgumentException("Invalid input for expense. Please enter 'y' or 'n'.");
                }
                t.IsExpense = expenseInput == "y";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Transaction updated successfully.");
        }
        public IEnumerable<Transaction> GetFilteredAndSortedTransactions(string filterChoice, string sortChoice, string sortOrder)
        {
            IEnumerable<Transaction> items = filterChoice switch
            {
                "1" => transactions,
                "2" => transactions.Where(t => t.IsExpense),
                "3" => transactions.Where(t => !t.IsExpense),
                _ => null
            };

            if (items == null) return null;

            return SortItems(items, sortChoice, sortOrder);
        }

        private IEnumerable<Transaction> SortItems(IEnumerable<Transaction> items, string sortChoice, string sortOrder)
        {
            Func<Transaction, object> sortSelector = sortChoice switch
            {
                "1" => t => t.Month,
                "2" => t => t.IsExpense,
                "3" => t => t.Amount,
                _ => t => t.Month
            };

            return sortOrder == "a"
            ? items.OrderBy(sortSelector)
            : items.OrderByDescending(sortSelector);
        }
    }
}
