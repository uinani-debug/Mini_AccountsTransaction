namespace AccountLibrary.API.Models
{
    public class Transaction
    {
        public string transactionName { get; set; }
        public string transactionDate { get; set; }
        public decimal transactionAmount { get; set; }
        public string transactionType { get; set; }
        public decimal currentBalance { get; set; }

    }

}
