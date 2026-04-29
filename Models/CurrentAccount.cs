namespace CashMachineATM.Models
{
    public class CurrentAccount : Account
    {
        public CurrentAccount(int accNo, int pin, decimal balance)
            : base(accNo, pin, balance) { }

        public override string GetAccountType()
        {
            return "Current Account";
        }
    }
}