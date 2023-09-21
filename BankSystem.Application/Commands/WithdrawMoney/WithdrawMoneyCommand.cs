using BankSystem.Application.Commands.TransferMoney;
using BankSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommand : AuthRequest<WithdrawMoneyViewModel>
    {
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
        public decimal Amount { get; set; }
    }
}
