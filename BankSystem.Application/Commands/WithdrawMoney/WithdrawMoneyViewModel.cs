using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.WithdrawMoney
{
    public class WithdrawMoneyViewModel
    {
        public string ReferenceID { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; }

        public WithdrawMoneyViewModel(string referenceID, string transactionType, string status, decimal amount, decimal balance, DateTime dateCreated)
        {
            ReferenceID = referenceID;
            TransactionType = transactionType;
            Status = status;
            Amount = amount;
            Balance = balance;

            TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateCreated, systemTimeZone);
            DateCreated = localDateTime;
        }
    }
}
