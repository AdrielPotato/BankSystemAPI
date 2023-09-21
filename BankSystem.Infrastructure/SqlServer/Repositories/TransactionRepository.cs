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
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            if (transaction.ID == Guid.Empty)
            {
                _dbContext.Add(transaction);
            }
            else
            {
                _dbContext.Update(transaction);
            }

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Transactions.SingleOrDefaultAsync(x=>x.ID == transaction.ID);
        }

        public async Task UpdateStatus(Guid transactionID, string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return;
            }

            await _dbContext.Transactions.Where(x => x.ID == transactionID).ExecuteUpdateAsync(x => x.SetProperty(u => u.Status, status));
        }
    }
}
