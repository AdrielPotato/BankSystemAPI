using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.DepositMoney
{
    public class DepositMoneyViewModel
    {
        public string ReferenceID { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }

        public DepositMoneyViewModel(string reference, string transactionType, string status, decimal amount, DateTime created) 
        { 
            ReferenceID = reference;
            TransactionType = transactionType;
            Status = status;
            Amount = amount;

            TimeZoneInfo systemTimeZone = TimeZoneInfo.Local;
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(created, systemTimeZone);
            DateCreated = localDateTime;
        }  
    }
}
