using BankSystem.Application.Commands.CreateAccount;
using BankSystem.Application.Commands.WithdrawMoney;
using BankSystem.Application.Models;
using BankSystem.Application.Repositories;
using BankSystem.Core.Services;
using MediatR;
using System.Net;

namespace BankSystem.Application.Queries.GetBalance
{
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, Result<GetBalanceViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IAccountRepository _accountRepository;

        public GetBalanceQueryHandler(
            IAccountService accountservice,
            ITransactionService transactionService,
            IAccountRepository accountRepository
            )
        {
            _accountService = accountservice;
            _transactionService = transactionService;
            _accountRepository = accountRepository;
        }
        public async Task<Result<GetBalanceViewModel>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //validate accountnumber and pin
                var account = await _accountRepository.GetAccountAsync(request.AccountNumber, true);

                if (account == null)
                {
                    return Result<GetBalanceViewModel>.Error(Convert.ToInt32(HttpStatusCode.NotFound), "Account not found");
                }

                bool isValid = _accountService.ValidatePin(request.Pin, account.PinHash, account.PinSalt);

                if (!isValid)
                {
                    return Result<GetBalanceViewModel>.Error(Convert.ToInt32(HttpStatusCode.OK), "Invalid Pin");
                }

                //check balance first
                var balance = _transactionService.CheckBalance(account);

                return new Result<GetBalanceViewModel>(new GetBalanceViewModel(balance))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = "Account creation success"
                };
            }
            catch (Exception)
            {
                return Result<GetBalanceViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Get balance failed. Try again later.");
            }
        }
    }
}
