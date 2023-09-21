using System.Security.Cryptography;
using System.Text;

namespace BankSystem.Core.Functions
{
    public static class GenerateUniqueID
    {
        public static string Execute(int length = 12)
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, length)}";
        }
    }
}