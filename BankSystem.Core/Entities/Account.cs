using BankSystem.Core.Contants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Entities
{
    public class Account
    {
        public Guid ID { get; set; }
        public string AccountNumber { get; set; } //Generated
        public string Name { get; set; }
        public byte[] PinHash { get; set; }
        public byte[] PinSalt { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Account(string name) 
        {
            Name = name;
            AccountNumber = GenerateAccountNumber();
        }
        
        private string GenerateAccountNumber()
        {
            Random rand = new();
            return Convert.ToString((long)Math.Floor(rand.NextDouble() * 9_000_000_000L + 1_000_000_000L));
        }
        
    }
}
