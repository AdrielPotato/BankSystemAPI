using BankSystem.Core.Contants;
using BankSystem.Core.Entities;
using BankSystem.Core.Services;

namespace BankSystem.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        public decimal CheckBalance(Account account)
        {
            if (account.Transactions == null)
                return 0;

            //debit
            var totalDeposits = account.Transactions
                .Where(x => x.TransactionType == TransactionType.Deposit && x.Status == TransactionStatus.Success)
                .Sum(x => x.Amount);

            var totalReceivedFundTransfer = account.Transactions
                .Where(x => x.TransactionType == TransactionType.FundTransfer && x.DestinationAccountID == account.ID && x.Status == TransactionStatus.Success)
                .Sum(x => x.Amount);

            //credit
            var totalFundTransfer = account.Transactions
                .Where(x => x.TransactionType == TransactionType.FundTransfer && x.DestinationAccountID != account.ID && x.Status == TransactionStatus.Success)
                .Sum(x => x.Amount);

            var totalWithdrawal = account.Transactions
                .Where(x => x.TransactionType == TransactionType.Withdrawal && x.Status == TransactionStatus.Success)
                .Sum(x => x.Amount);

            return (totalDeposits + totalReceivedFundTransfer) - (totalWithdrawal + totalFundTransfer);
        }
    }
}
