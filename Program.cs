using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMoney
{
    internal class Program
    {
        public static void RandomData(int n) // code for testing purpose only, creates random data in transaction file
        {
            Random random = new Random();
            // using StreamWriter class                            
            // second parameter in StreamWriter, true means append to end of file
            using (StreamWriter writer = new StreamWriter("transactions.csv", true))
            {
                for (int i = 0; i < n; i++)
                {
                    int month = random.Next(1, 13); // month 1 - 12
                    int amount = random.Next(101); // max amount 100,00 Kr
                    string type = random.Next(2) == 0 ? "Expense" : "Income"; // either/or

                    writer.WriteLine($"{month};{amount};{type}"); // write post in csv with ';' as seperator
                }
            }
            Console.WriteLine("Random data is saved in transactions.csv"); // success
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.Write("Do you want to create random testdata? (y/n): ");
            string a;
            a = Console.ReadLine();
            if ("y" == a)
            {
                Console.WriteLine("How many new test data do you want? : ");
                int n = int.Parse(Console.ReadLine());
                RandomData(n);
            }

            TrackMyMoney app = new TrackMyMoney(); // creates the application
            app.Run();
            
            // the end
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The application TrackMoney terminated gracefully");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
