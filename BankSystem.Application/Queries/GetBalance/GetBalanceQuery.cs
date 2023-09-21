using BankSystem.Application.Commands.WithdrawMoney;
using BankSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Queries.GetBalance
{
    public class GetBalanceQuery : AuthRequest<GetBalanceViewModel>
    {
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
    }
}
