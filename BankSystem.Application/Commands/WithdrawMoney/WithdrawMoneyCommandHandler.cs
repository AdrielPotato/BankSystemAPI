using BankSystem.Application.Commands.DepositMoney;
using BankSystem.Application.Commands.TransferMoney;
using BankSystem.Application.Models;
using BankSystem.Application.Repositories;
using BankSystem.Core.Contants;
using BankSystem.Core.Entities;
using BankSystem.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyCommand, Result<WithdrawMoneyViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawMoneyCommandHandler(
            IAccountService accountservice,
            IAccountRepository accountRepository,
            ITransactionService transactionService,
            ITransactionRepository transactionRepository
            )
        {
            _accountService = accountservice;
            _accountRepository = accountRepository;
            _transactionService = transactionService;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<WithdrawMoneyViewModel>> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //validate accountnumber and pin
                var account = await _accountRepository.GetAccountAsync(request.AccountNumber,true);

                if (account == null)
                {
                    return Result<WithdrawMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.NotFound), "Account not found");
                }

                bool isValid = _accountService.ValidatePin(request.Pin, account.PinHash, account.PinSalt);

                if (!isValid)
                {
                    return Result<WithdrawMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Invalid Pin");
                }

                //check balance first
                var balance = _transactionService.CheckBalance(account);
                if (balance < request.Amount)
                {
                    return Result<WithdrawMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Not enough funds");
                }

                // Create Transaction
                var transaction = new Transaction(account.ID, TransactionType.Withdrawal, request.Amount, account.ID, TransactionSources.System);

                var result = await _transactionRepository.CreateAsync(transaction);

                if (result != null)
                {
                    await _transactionRepository.UpdateStatus(result.ID, TransactionStatus.Success);
                }
                else
                {
                    return Result<WithdrawMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Withdrawal failed. Try again later.");
                }

                // recheck balance
                balance = _transactionService.CheckBalance(account);

                return new Result<WithdrawMoneyViewModel>(new WithdrawMoneyViewModel(transaction.ReferenceID, transaction.TransactionType, transaction.Status, transaction.Amount, balance, transaction.DateCreated))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = "Deposit success"
                };
            }
            catch (Exception)
            {
                return Result<WithdrawMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Withdrawal failed. Try again later.");
            }
        }
    }
}
