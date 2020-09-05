using System;

namespace BudgeterShared.DTOs
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
