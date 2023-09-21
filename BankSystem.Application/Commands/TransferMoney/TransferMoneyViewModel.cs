using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.TransferMoney
{
    public class TransferMoneyViewModel
    {
        public string ReferenceID { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        
        public TransferMoneyViewModel(string referenceID, string transactionType, string status, decimal amount, DateTime dateCreated)
        {
            ReferenceID = referenceID;
            TransactionType = transactionType;
            Status = status;
            Amount = amount;

            TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateCreated, systemTimeZone);
            DateCreated = localDateTime;
        }
    }
}
