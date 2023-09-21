using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Queries.GetBalance
{
    public class GetBalanceViewModel
    {
        public decimal Balance { get; set; }

        public GetBalanceViewModel(decimal balance)
        {
            Balance = balance;
        }
    }
}
