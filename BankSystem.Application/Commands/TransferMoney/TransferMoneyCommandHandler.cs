using BankSystem.Application.Commands.DepositMoney;
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

namespace BankSystem.Application.Commands.TransferMoney
{
    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, Result<TransferMoneyViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;

        public TransferMoneyCommandHandler(
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

        public async Task<Result<TransferMoneyViewModel>> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //validate accountnumber and pin
                var account = await _accountRepository.GetAccountAsync(request.AccountNumber,true);
                if (account == null)
                {
                    return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.NotFound), "Account not found");
                }

                bool isValid = _accountService.ValidatePin(request.Pin, account.PinHash, account.PinSalt);
                if (!isValid)
                {
                    return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Invalid Pin");
                }

                var destinationAccount = await _accountRepository.GetAccountAsync(request.DestinationAccount);
                if (destinationAccount == null)
                {
                    return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.NotFound), "Destination account not found");
                }

                //check balance first

                if (_transactionService.CheckBalance(account) < request.Amount)
                {
                    return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Not enough funds");
                }

                // Create Transaction
                var transaction = new Transaction(account.ID, TransactionType.FundTransfer, request.Amount, account.ID, destinationAccount.ID);

                var result = await _transactionRepository.CreateAsync(transaction);

                if (result != null)
                {
                    //create also a transaction for the destination account
                    var transfer = new Transaction(destinationAccount.ID, TransactionType.FundTransfer, request.Amount, account.ID, destinationAccount.ID);
                    var transferResult = await _transactionRepository.CreateAsync(transfer);

                    if (transferResult != null)
                    {
                        await _transactionRepository.UpdateStatus(result.ID, TransactionStatus.Success);
                        await _transactionRepository.UpdateStatus(transfer.ID, TransactionStatus.Success);
                    }
                    else
                    {
                        await _transactionRepository.UpdateStatus(result.ID, TransactionStatus.Fail);
                    }
                }
                else
                {
                    return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Transfer failed. Try again later.");
                }

                return new Result<TransferMoneyViewModel>(new TransferMoneyViewModel(result.ReferenceID, result.TransactionType, TransactionStatus.Success, result.Amount, result.DateCreated))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = "Deposit success"
                };
            }
            catch (Exception)
            {
                return Result<TransferMoneyViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Fund Transfer failed. Try again later.");
            }
        }
    }
}
