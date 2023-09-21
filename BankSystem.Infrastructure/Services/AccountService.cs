using BankSystem.Core.Contants;
using BankSystem.Core.Entities;
using BankSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Infrastructure.Services
{
    public class AccountService: IAccountService
    {

        public (byte[] pinHash, byte[] pinSalt) CreatePinHash(string Pin)
        {
            //checks pin
            if (string.IsNullOrEmpty(Pin)) throw new ArgumentNullException("Pin");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return (hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin)), hmac.Key);
            }
        }

        public bool ValidatePin(string pin, byte[] hash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedPinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != hash[i]) return false;
                }
            }

            return true;
        }


    }
}
