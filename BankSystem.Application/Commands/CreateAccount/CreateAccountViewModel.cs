using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.CreateAccount
{
    public class CreateAccountViewModel
    {
        
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public CreateAccountViewModel(string name, string accntNumber, decimal balance)
        {
            AccountName = name;
            AccountNumber = accntNumber;
            Balance = balance;
        }
    }
}
