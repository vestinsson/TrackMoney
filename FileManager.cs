using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMoney
{
    internal class FileManager
    {
        private const string FileName = "transactions.csv";
        private const string ErrorLogFile = "errorLog.txt";

        public List<Transaction> LoadTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                if (File.Exists(FileName))
                {
                    string[] lines = File.ReadAllLines(FileName); 
                    foreach (string line in lines) // reads the file from last session line by line to transactions list
                    {
                        try
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length != 3)
                            {
                                throw new FormatException($"Invalid line format: {line}");
                            }
                            int month;
                            if (!int.TryParse(parts[0], out month) || month < 1 || month > 12)
                            {
                                throw new ArgumentOutOfRangeException($"Invalid month value: {parts[0]}");
                            }
                            transactions.Add(new Transaction
                            {
                                Month = new DateTime(2000, month, 1), // default year 2000, year is not used
                                Amount = decimal.Parse(parts[1], new CultureInfo("sv-SE")),
                                IsExpense = parts[2] == "Expense"
                            });
                        }
                        catch (Exception ex) when (ex is FormatException || ex is IndexOutOfRangeException || ex is ArgumentOutOfRangeException)
                        {
                            string errorMessage = $"Error parsing line: {line}. Error: {ex.Message}";
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(errorMessage);
                            Console.ResetColor();
                            LogError(errorMessage);
                        }
                    }
                }
                else
                {
                    string message = $"File {FileName} not found. Starting with an empty transaction list.";
                    Console.WriteLine(message);
                    LogError(message);
                }
            }
            catch (IOException ex)
            {
                string errorMessage = $"Error reading file: {ex.Message}";
                Console.WriteLine(errorMessage);
                LogError(errorMessage);
            }
            catch (UnauthorizedAccessException ex)
            {
                string errorMessage = $"Access denied to file: {ex.Message}";
                Console.WriteLine(errorMessage);
                LogError(errorMessage);
            }
            return transactions;
        }

        public void SaveTransactions(List<Transaction> transactions)
        {
            try
            {
                File.WriteAllLines(FileName, transactions.Select(t => t.ToString())); // writes transactions from session to file
            }
            catch (IOException ex)
            {
                string errorMessage = $"Error writing to file: {ex.Message}";
                Console.WriteLine(errorMessage);
                LogError(errorMessage);
            }
            catch (UnauthorizedAccessException ex)
            {
                string errorMessage = $"Access denied when writing to file: {ex.Message}";
                Console.WriteLine(errorMessage);
                LogError(errorMessage);
            }
        }

        private void LogError(string errorMessage) 
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {errorMessage}";
                File.AppendAllText(ErrorLogFile, logEntry + Environment.NewLine); // write error text to file
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to write to error log: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}

