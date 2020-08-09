using System;
using System.Collections.Generic;
using System.Text;

namespace BudgeterShared
{
    public class TransactionDTO
    {
        public DateTime Date { get; set; }

        public String Description { get; set; }

        public Decimal Amount { get; set; }
    }
}
