using BankSystem.Application.Commands.CreateAccount;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.DepositMoney
{
    public class DepositMoneyCommandValidator : AbstractValidator<DepositMoneyCommand>
    {
        public DepositMoneyCommandValidator()
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
                .GreaterThanOrEqualTo(100)
                .WithMessage("Amount should be atleast 100");
        }
    }
}
