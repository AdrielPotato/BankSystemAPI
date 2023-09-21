using BankSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountAsync(string accountNumber, bool includeTransactions = false);
        Task<bool> CreateAsync(Account account);
    }
}
