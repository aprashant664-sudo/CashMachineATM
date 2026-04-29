using System;
using System.Windows.Forms;
using CashMachineATM.Models;
using CashMachineATM.Services;

namespace CashMachineATM
{
    public partial class MainForm : Form
    {
        private AccountManager manager = new AccountManager();
        private Account currentAccount;
        private Transaction lastTransaction;
        private string enteredPin = "";
        private TextBox displayBox;

        public MainForm()
        {
            InitializeComponent();

            displayBox = FindDisplayBox();
            displayBox.Text = "Enter PIN";

            ConnectButtons();
        }

        private TextBox FindDisplayBox()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                    return (TextBox)c;
            }

            throw new Exception("Please add one TextBox for display.");
        }

        private void ConnectButtons()
        {
            foreach (Control c in Controls)
            {
                if (c is Button btn)
                {
                    btn.Click -= Button_Click;
                    btn.Click += Button_Click;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string text = btn.Text.Trim();

            if (text == "0" || text == "1" || text == "2" || text == "3" || text == "4" ||
                text == "5" || text == "6" || text == "7" || text == "8" || text == "9")
            {
                AddDigit(text);
            }
            else if (text.Equals("Clear", StringComparison.OrdinalIgnoreCase))
            {
                ClearPin();
            }
            else if (text.Equals("Enter", StringComparison.OrdinalIgnoreCase))
            {
                Login();
            }
            else if (text.Equals("Balance", StringComparison.OrdinalIgnoreCase))
            {
                ShowBalance();
            }
            else if (text.Equals("Withdraw", StringComparison.OrdinalIgnoreCase))
            {
                WithdrawMoney();
            }
            else if (text.Equals("Receipt", StringComparison.OrdinalIgnoreCase))
            {
                ShowReceipt();
            }
            else if (text.Equals("Logout", StringComparison.OrdinalIgnoreCase))
            {
                Logout();
            }
        }

        private void AddDigit(string digit)
        {
            if (enteredPin.Length < 4)
            {
                enteredPin += digit;
                displayBox.Text = new string('*', enteredPin.Length);
            }
        }

        private void ClearPin()
        {
            enteredPin = "";
            displayBox.Text = "Enter PIN";
        }

        private void Login()
        {
            if (enteredPin.Length != 4)
            {
                displayBox.Text = "Enter 4-digit PIN";
                return;
            }

            int pin = int.Parse(enteredPin);
            currentAccount = manager.ValidatePin(pin);

            if (currentAccount != null)
                displayBox.Text = "Login Successful - " + currentAccount.GetAccountType();
            else
                displayBox.Text = "Invalid PIN";

            enteredPin = "";
        }

        private void ShowBalance()
        {
            if (currentAccount == null)
            {
                displayBox.Text = "Please login first";
                return;
            }

            displayBox.Text = "Balance: Rs. " + currentAccount.Balance;
        }

        private void WithdrawMoney()
        {
            if (currentAccount == null)
            {
                displayBox.Text = "Please login first";
                return;
            }

            string input = Prompt.ShowDialog("Enter withdrawal amount:", "Withdraw");

            if (decimal.TryParse(input, out decimal amount))
            {
                if (currentAccount.Withdraw(amount))
                {
                    lastTransaction = new Transaction("Withdraw", amount);
                    displayBox.Text = "Withdraw Successful";
                }
                else
                {
                    displayBox.Text = "Insufficient Funds";
                }
            }
            else
            {
                displayBox.Text = "Invalid Amount";
            }
        }

        private void ShowReceipt()
        {
            if (lastTransaction == null)
                MessageBox.Show("No transaction yet", "Receipt");
            else
                MessageBox.Show(lastTransaction.ToString(), "Receipt");
        }

        private void Logout()
        {
            currentAccount = null;
            enteredPin = "";
            displayBox.Text = "Enter PIN";
        }

        // Old auto-created event handlers to stop designer errors
        private void MainForm_Load(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button10_Click(object sender, EventArgs e) { }
        private void button14_Click(object sender, EventArgs e) { }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label label = new Label() { Left = 20, Top = 20, Text = text, Width = 240 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "OK", Left = 100, Width = 80, Top = 80 };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(label);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            prompt.ShowDialog();

            return inputBox.Text;
        }
    }
}