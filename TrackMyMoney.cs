using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TrackMoney
{
    internal class TrackMyMoney
    {
        private List<Transaction> transactions;
        private FileManager fileManager;
        private TransactionManager transactionManager;
        public decimal CurrentBalance { get; set; }

        public TrackMyMoney()
        {
            fileManager = new FileManager();
            transactions = fileManager.LoadTransactions();
            transactionManager = new TransactionManager(transactions);
            UpDateCurrentBalance();
        }

        public void Run()
        {
            while (true)
            {
                string choice = Menu.GetMainMenuChoice(CurrentBalance);

                switch (choice)
                {
                    case "1":
                        UpDateCurrentBalance();
                        ShowItems();
                        break;
                    case "2":
                        transactionManager.AddTransaction();
                        UpDateCurrentBalance();
                        break;
                    case "3":
                        transactionManager.EditOrRemoveTransaction();
                        UpDateCurrentBalance();
                        break;
                    case "4":
                        fileManager.SaveTransactions(transactions);
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void ShowItems()
        {
            string choice = Menu.GetShowItemsChoice();
            string sortChoice = Menu.GetSortingChoice();
            string sortOrder = Menu.GetSortingOrder();

            var items = transactionManager.GetFilteredAndSortedTransactions(choice, sortChoice, sortOrder);

            if (items == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Invalid choice.");
                Console.ResetColor();
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Month:MM} ".PadRight(10,'.') +
                    $" {(item.IsExpense ? "Expense" : "Income")} ".PadRight(20,'.') +
                    $" {item.Amount.ToString("C2", new CultureInfo("sv-SE"))}");
            }
            Console.ReadLine();
        }

        public void UpDateCurrentBalance()
        {
            CurrentBalance = CalculateSum();
        }

        public Decimal CalculateSum()
        {
            Decimal incomeSum = transactions.Where(t => !t.IsExpense).Sum(t => t.Amount);
            Decimal expenseSum = transactions.Where(t => t.IsExpense).Sum(t => t.Amount);
            return incomeSum - expenseSum;
        }

    }
}