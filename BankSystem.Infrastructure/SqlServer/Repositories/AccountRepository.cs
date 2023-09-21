using BankSystem.Application.Repositories;
using BankSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Infrastructure.SqlServer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Account> GetAccountAsync(string accountNumber, bool includeTransactions = false)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return null;
            }

            if (includeTransactions)
            {
                return await _dbContext.Accounts.Include(x => x.Transactions).SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);

            }
            else
            {
                return await _dbContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
            }
        }

        public async Task<bool> CreateAsync(Account account)
        {
            if (account.ID != Guid.Empty)
            {
                return false;
            }

            _dbContext.Add(account);
            return (await _dbContext.SaveChangesAsync()) == 1;

        }
    }
}
