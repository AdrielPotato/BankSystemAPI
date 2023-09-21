using BankSystem.Application.Commands.CreateAccount;
using BankSystem.Application.Models;
using BankSystem.Application.Repositories;
using BankSystem.Core.Contants;
using BankSystem.Core.Entities;
using BankSystem.Core.Services;
using MediatR;
using System.Net;

namespace BankSystem.Application.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, Result<DepositMoneyViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DepositMoneyCommandHandler(
            IAccountService accountservice,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository
            )
        {
            _accountService = accountservice;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<Result<DepositMoneyViewModel>> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //validate accountnumber and pin
                var account = await _accountRepository.GetAccountAsync(request.AccountNumber);

                if (account == null)
                {
                    return Result<DepositMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.NotFound), "Account not found");
                }

                bool isValid = _accountService.ValidatePin(request.Pin, account.PinHash, account.PinSalt);

                if (!isValid)
                {
                    return Result<DepositMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Invalid Pin");
                }

                // Create Transaction
                var transaction = new Transaction(account.ID, TransactionType.Deposit, request.Amount, TransactionSources.System, account.ID);

                var result = await _transactionRepository.CreateAsync(transaction);

                if (result!=null)
                {
                    await _transactionRepository.UpdateStatus(result.ID, TransactionStatus.Success);
                }
                else
                {
                    return Result<DepositMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Deposit failed. Try again later.");
                }

                return new Result<DepositMoneyViewModel>(new DepositMoneyViewModel(transaction.ReferenceID,transaction.TransactionType, transaction.Status, transaction.Amount,transaction.DateCreated))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = "Deposit success"
                };
            }
            catch (Exception)
            {
                return Result<DepositMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Deposit failed. Try again later.");
            }

        }
    }
}
