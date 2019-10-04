using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
   
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class PremiumAccountTests
    {
        [TestCase("99999", "Premium Account", 10000, AccountType.Free, 250, false)]
        [TestCase("99999", "Premium Account", 10000, AccountType.Premium, -100, false)]
        [TestCase("99999", "Premium Account", 10000, AccountType.Premium, 250, true)]
        [Test]     public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("99999", "Premium Account", 10000, AccountType.Premium, -5000, 5000, false)]
        [TestCase("99999", "Premium Account", 10000, AccountType.Free, -1000, 9000, false)]
        [TestCase("99999", "Premium Account", 10000, AccountType.Premium, 1000, 9000, false)]
        [TestCase("99999", "Premium Account", 150, AccountType.Premium, -50, 100, true)]
        [TestCase("99999", "Premium Account", 100, AccountType.Premium, -150, -150, true)]
        [Test]
        public void PremiumAccountWithdrawRule(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success == true)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
        }
    }
}
