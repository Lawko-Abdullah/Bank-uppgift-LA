using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Models/BankAccount.cs
using System;

namespace SimpleBankSystem.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; }
        public string AccountHolderName { get; }
        public string AccountType { get; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string accountHolderName, string accountType, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Insättning av {amount} till {AccountHolderName} ({AccountType}).");
            }
            else
            {
                Console.WriteLine("Beloppet måste vara positivt.");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Uttag av {amount} från {AccountHolderName} ({AccountType}).");
            }
            else
            {
                Console.WriteLine("Ogiltigt belopp eller otillräckliga medel.");
            }
        }

        public void Transfer(BankAccount toAccount, decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Withdraw(amount);
                toAccount.Deposit(amount);
                Console.WriteLine($"Överförde {amount} från {AccountHolderName} ({AccountType}) till {toAccount.AccountHolderName} ({toAccount.AccountType}).");
            }
            else
            {
                Console.WriteLine("Överföring misslyckades.");
            }
        }
    }
}
