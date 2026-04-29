namespace CashMachineATM.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(int accNo, int pin, decimal balance)
            : base(accNo, pin, balance) { }

        public override string GetAccountType()
        {
            return "Savings Account";
        }
    }
}