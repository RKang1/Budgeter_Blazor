using System;
using System.Collections.Generic;
using System.Text;

namespace BudgeterShared
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public String Description { get; set; }

        public Decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime RevisionDate { get; set; }
    }
}
