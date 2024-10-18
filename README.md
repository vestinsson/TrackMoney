# TrackMoney

TrackMoney is a simple console application for tracking personal expenses and income. It allows users to add, edit, and remove transactions, as well as view their current balance and transaction history.

## Features

- Add new expenses or income transactions
- Edit or remove existing transactions
- View all transactions, expenses only, or income only
- Sort transactions by month, type, or amount
- Automatically calculate and display current balance
- Save transactions to a CSV file for persistence
- Generate random test data for demonstration purposes

## Getting Started

### Prerequisites

- .NET Framework (version used in development)
- Visual Studio or any C# compatible IDE

### Installation

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio or your preferred C# IDE.
3. Build the solution to restore NuGet packages and compile the code.

## Usage

1. Run the application.
2. You will be presented with a main menu showing your current balance and options:
   - Show items
   - Add new expense or income
   - Edit or remove a post
   - Save and quit
3. Select an option by entering the corresponding number.

### Adding a Transaction

1. Choose option 2 from the main menu.
2. Enter the month (1-12) for the transaction.
3. Enter the amount (positive numbers only).
4. Specify whether it's an expense or income.

### Viewing Transactions

1. Choose option 1 from the main menu.
2. Select whether to view all items, expenses only, or income only.
3. Choose how to sort the transactions (by month, type, or amount).
4. Specify ascending or descending order for sorting.

### Editing or Removing a Transaction

1. Choose option 3 from the main menu.
2. Select the transaction you want to edit or remove.
3. Choose whether to edit (e) or remove (r) the transaction.
4. If editing, enter new values for the transaction details.

## File Structure

- `Program.cs`: Contains the `Main` method and entry point of the application.
- `TrackMyMoney.cs`: Main application logic and flow control.
- `Transaction.cs`: Defines the `Transaction` class representing individual transactions.
- `TransactionManager.cs`: Manages operations on transactions (add, edit, remove, display).
- `Menu.cs`: Handles user interface and menu options.
- `FileManager.cs`: Manages reading from and writing to the CSV file.

## Data Persistence

Transactions are saved to a file named `transactions.csv` in the same directory as the executable. This file is loaded when the application starts and updated when the user chooses to save and quit.

## Error Handling

The application includes basic error handling for invalid inputs and file operations. Errors are logged to a file named `errorLog.txt`.

## Contributing

Contributions to improve TrackMoney are welcome. Please feel free to submit pull requests or open issues to discuss proposed changes or report bugs.

## License

[Specify your chosen license here]

