namespace CashMachineATM.Models
{
    public abstract class Account
    {
        public int AccountNumber { get; private set; }
        public int Pin { get; private set; }
        public decimal Balance { get; protected set; }

        public Account(int accNo, int pin, decimal balance)
        {
            AccountNumber = accNo;
            Pin = pin;
            Balance = balance;
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0) return false;

            if (Balance >= amount)
            {
                Balance -= amount;
                return true;
            }

            return false;
        }

        public abstract string GetAccountType();
    }
}