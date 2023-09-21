using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.CreateAccount
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Exceeded length of 100");
            RuleFor(x => x.Pin)
                .NotEmpty()
                .WithMessage("Pin is required")
                .Length(6)
                .WithMessage("Pin must be 6 digits");
            RuleFor(x => x.ConfirmPin)
                .Equal(x => x.Pin)
                .WithMessage("ConfirmPin must be equal to 'Pin'");
        }
    }
}
