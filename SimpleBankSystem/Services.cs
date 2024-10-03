using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/BankService.cs
using System;
using SimpleBankSystem.Models;

namespace SimpleBankSystem.Services
{
    public class BankService
    {
        // Skapar privat pga säkerhet
        private readonly BankAccount personkonto;
        private readonly BankAccount sparkonto;
        private readonly BankAccount investeringskonto;

        public BankService() // Konto information
        {
            personkonto = new BankAccount("P 6789-4163455589", "Lawko Abdullah", "Personkonto", 50000);
            sparkonto = new BankAccount("S 6789-4163455809", "Lawko Abdullah", "Sparkonto", 100000);
            investeringskonto = new BankAccount("I 6789-4163455621", "Lawko Abdullah", "Investeringskonto", 200000);
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nVälj ett alternativ:\n1. Insättning\n2. Uttag\n3. Överföring\n4. Kontrollera saldo\n5. Avsluta");
                switch (Console.ReadLine())
                {
                    case "1":
                        HandleDeposit();
                        break;
                    case "2":
                        HandleWithdraw();
                        break;
                    case "3":
                        HandleTransfer();
                        break;
                    case "4":
                        CheckBalance();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;
                }
            }
        }

        private BankAccount SelectAccount(string message) // Fel hantering och val av system
        {
            Console.WriteLine(message + "\n1. Personkonto\n2. Sparkonto\n3. Investeringskonto");
            switch (Console.ReadLine())
            {
                case "1": return personkonto;
                case "2": return sparkonto;
                case "3": return investeringskonto;
                default: Console.WriteLine("Ogiltigt val."); return null;
            }
        }

        private void HandleDeposit() // insättnning
        {
            BankAccount account = SelectAccount("Välj konto för insättning:");
            if (account != null)
            {
                Console.WriteLine("Ange belopp:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    account.Deposit(amount);
                else
                    Console.WriteLine("Ogiltigt belopp.");
            }
        }

        private void HandleWithdraw() //utdrag
        {
            BankAccount account = SelectAccount("Välj konto för uttag:");
            if (account != null)
            {
                Console.WriteLine("Ange belopp:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    account.Withdraw(amount);
                else
                    Console.WriteLine("Ogiltigt belopp.");
            }
        }

        private void HandleTransfer() // överför 
        {
            BankAccount fromAccount = SelectAccount("Välj konto att överföra från:");
            if (fromAccount != null)
            {
                BankAccount toAccount = SelectAccount("Välj konto att överföra till:");
                if (toAccount != null && toAccount != fromAccount)
                {
                    Console.WriteLine("Ange belopp:");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        fromAccount.Transfer(toAccount, amount);
                    else
                        Console.WriteLine("Ogiltigt belopp.");
                }
            }
        }

        private void CheckBalance() //saldo ?
        {
            BankAccount account = SelectAccount("Välj konto för att kontrollera saldo:");
            if (account != null)
            {
                Console.WriteLine($"Saldo på {account.AccountHolderName} ({account.AccountType}): {account.Balance}");
                Console.WriteLine($"Kontonummer: {account.AccountNumber}"); // Lägg till detta för att visa kontonumret
            }
        }
    }

    // BankAccount klass (lägg till denna om den inte redan finns)
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        public string AccountType { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountNumber, string accountHolderName, string accountType, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount) // kom ihåg studera try catch satser
        {
            Balance += amount;
            Console.WriteLine($"Insättning genomförd. Nytt saldo: {Balance}");
        }

        public void Withdraw(decimal amount) //  catch satser rad ovan kolla youtube
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Uttag genomfört. Nytt saldo: {Balance}");
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo.");
            }
        }

        public void Transfer(BankAccount toAccount, decimal amount)
        {
            if (amount <= Balance)
            {
                Withdraw(amount);
                toAccount.Deposit(amount);
                Console.WriteLine($"Överföring av {amount} genomförd till {toAccount.AccountHolderName}.");
            }
            else
            {
                Console.WriteLine("Otillräckligt saldo för överföring.");
            }
        }
    }

}
