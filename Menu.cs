using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMoney
{
    internal class Menu
    {
        public static string GetMainMenuChoice(decimal currentBalance)  // main menu
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to TrackMoney");
            Console.WriteLine($"You have Currently {currentBalance.ToString("C2", new CultureInfo("sv-SE"))} on your account.");

            Console.WriteLine("Pick an option:");
            Console.WriteLine("\n>> (1) Show items");
            Console.WriteLine(">> (2) Add new expense or income");
            Console.WriteLine(">> (3) Edit or remove a post");
            Console.WriteLine(">> (4) Save and quit");
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        public static string GetShowItemsChoice() // choice item to display
        {
            Console.Clear();
            Console.WriteLine("\n>> (1) All items");
            Console.WriteLine(">> (2) Expenses only");
            Console.WriteLine(">> (3) Income only");
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        public static string GetEditOrRemoveChoice() // choice edit or remove post
        {
            Console.Clear();
            Console.Write("Do you want to edit >> (e) or remove >> (r) this transaction? ");
            return Console.ReadLine().ToLower();
        }

        public static string GetSortingChoice() // coice for sorting by
        {
            Console.Clear();
            Console.WriteLine("\nSort by:");
            Console.WriteLine(">> (1) Month");
            Console.WriteLine(">> (2) Transaction type (Expense/Income)");
            Console.WriteLine(">> (3) Amount");
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        public static string GetSortingOrder() // choice for sort forwards or backwards
        {
            Console.Clear();
            Console.Write("Sort in ascending >> (a) or descending >> (d) order? ");
            return Console.ReadLine().ToLower();
        }
    }

}
