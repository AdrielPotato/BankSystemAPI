using BankSystem.Application.Models;
using BankSystem.Application.Repositories;
using BankSystem.Core.Entities;
using BankSystem.Core.Services;
using MediatR;
using System.Net;

namespace BankSystem.Application.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<CreateAccountViewModel>>
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        public CreateAccountCommandHandler(IAccountService accountService, IAccountRepository accountRepository)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
        }
        public async Task<Result<CreateAccountViewModel>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var initialBalance = 0;
                var (pinHash, pinSalt) = _accountService.CreatePinHash(request.Pin);

                var account = new Account(request.Name)
                {
                    PinHash = pinHash,
                    PinSalt = pinSalt
                };

                await _accountRepository.CreateAsync(account);

                return new Result<CreateAccountViewModel>(new CreateAccountViewModel(account.Name, account.AccountNumber, initialBalance))
                {
                    Success = true,
                    StatusCode = Convert.ToInt32(HttpStatusCode.OK),
                    Message = "Account creation success"
                };
            }
            catch (Exception)
            {
                return Result<CreateAccountViewModel>.Error(Convert.ToInt32(HttpStatusCode.InternalServerError), "Account creation failed");
            }
        }
    }
}
