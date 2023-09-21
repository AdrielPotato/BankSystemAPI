using BankSystem.Application.Models;

namespace BankSystem.Application.Commands.TransferMoney
{
    public class TransferMoneyCommand : AuthRequest<TransferMoneyViewModel>
    {
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
        public string DestinationAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
