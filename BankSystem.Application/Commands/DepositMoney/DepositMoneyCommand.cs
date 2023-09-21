using BankSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.DepositMoney
{
    public class DepositMoneyCommand : AuthRequest<DepositMoneyViewModel>
    {
        //Acount Number, Amount, Pin
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
        public decimal Amount { get; set; }

    }
}
