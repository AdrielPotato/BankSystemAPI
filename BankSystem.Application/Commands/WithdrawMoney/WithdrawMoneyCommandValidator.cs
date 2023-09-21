using BankSystem.Application.Commands.DepositMoney;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandValidator : AbstractValidator<WithdrawMoneyCommand>
    {
        public WithdrawMoneyCommandValidator() 
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .WithMessage("Account Number is required")
                .MaximumLength(20)
                .WithMessage("Exceeded length");
            RuleFor(x => x.Pin)
                .NotEmpty()
                .WithMessage("Pin is required")
                .Length(6)
                .WithMessage("Pin must be 6 digits");
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Amount is required");
        }
    }
}
