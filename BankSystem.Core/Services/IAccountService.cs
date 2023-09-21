using BankSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Services
{
    public interface IAccountService
    {
        public (byte[] pinHash, byte[] pinSalt) CreatePinHash(string Pin);
        public bool ValidatePin(string pin, byte[] hash, byte[] salt);

    }
}
