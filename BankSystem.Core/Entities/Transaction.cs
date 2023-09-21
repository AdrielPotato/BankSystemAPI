using BankSystem.Core.Contants;
using BankSystem.Core.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Entities
{
    public class Transaction
    {
        public Guid ID { get; set; }
        public Guid AccountID { get; set; }
        public string ReferenceID { get; set; }
        public string TransactionType { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public Guid SourceAccountID { get; set; }
        public Guid DestinationAccountID { get; set; }
        public DateTime DateCreated { get; set; }

        public Transaction()
        {
            ReferenceID = GenerateUniqueID.Execute();
            DateCreated = DateTime.UtcNow;
            Status = TransactionStatus.Created;
        }
        public Transaction(Guid accountID, string transactionType, decimal amount, Guid sourceAccount, Guid destinationAccount): this()
        {
            AccountID = accountID;
            TransactionType = transactionType;
            Amount = amount;
            SourceAccountID = sourceAccount;
            DestinationAccountID = destinationAccount;
        }

        public void SetStatus(string status)
        {
            Status = status;
        }
    }
}
