using System;
using System.Collections;
using System.Collections.Generic;

namespace AccountLibrary.API.Models
{
    public class AccountDetails
    {
        public string AccountIdentifier { get; set; }
        public string AccountType { get; set; }
        public string AccountSubType { get; set; }

        public string AccountHolderName { get; set; }
        public string AccountStatus { get; set; }

        public string AccountName { get; set; }
        public string OpenDate { get; set; }
        public string SortCode { get; set; }
        public string ProductName { get; set; }
        public double InterestRate { get; set; }
        public AvailableBalance AvailableBalance { get; set; }
        public AvailableBalance CurrentBalance { get; set; }
        public List<Transaction> Transactions { get; set; }
        

    }
    


}
