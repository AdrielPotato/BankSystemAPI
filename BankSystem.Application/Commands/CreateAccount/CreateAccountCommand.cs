using BankSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Commands.CreateAccount
{
    public class CreateAccountCommand : AuthRequest<CreateAccountViewModel>
    {
        public string Name { get; set; }
        public string Pin { get; set; }
        public string ConfirmPin { get; set; }
    }
}
