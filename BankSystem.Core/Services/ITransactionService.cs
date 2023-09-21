using BankSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Services
{
    public interface ITransactionService
    {
        public decimal CheckBalance(Account account);
    }
}
