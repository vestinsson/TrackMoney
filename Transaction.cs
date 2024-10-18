using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackMoney
{
    internal class Transaction // responsibility får expense and income transactions
    {
        public decimal Amount { get; set; } // decimal dont lose 'ören' in computations
        public DateTime Month { get; set; }
        public bool IsExpense { get; set; } // is expense else income
        public decimal AccountSum { get; set; } // store the total in the account

        public override string ToString() // overrides method ToString()
        {           // will only use month in the DateTime object, swedish krona is used, the conditional operator ? :
            return $"{Month:MM};{Amount.ToString("F2", new CultureInfo("sv-SE"))};{(IsExpense ? "Expense" : "Income")}";
        }           // returns ex "03;670,90;Expense" swedish convention uses ';' as separator in CSV instead of ','

    }
}
