using System;

namespace CashMachineATM.Models
{
    public class Transaction
    {
        public string Type { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Type: {Type}\nAmount: Rs. {Amount}\nDate: {Date}";
        }
    }
}