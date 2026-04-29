using System.Collections.Generic;
using CashMachineATM.Models;

namespace CashMachineATM.Services
{
    public class AccountManager
    {
        private List<Account> accounts = new List<Account>();

        public AccountManager()
        {
            accounts.Add(new SavingsAccount(1001, 1234, 1000));
            accounts.Add(new CurrentAccount(1002, 1111, 1500));
            accounts.Add(new SavingsAccount(1003, 2222, 2000));
            accounts.Add(new CurrentAccount(1004, 3333, 500));
        }

        public Account ValidatePin(int pin)
        {
            foreach (var acc in accounts)
            {
                if (acc.Pin == pin)
                    return acc;
            }
            return null;
        }
    }
}