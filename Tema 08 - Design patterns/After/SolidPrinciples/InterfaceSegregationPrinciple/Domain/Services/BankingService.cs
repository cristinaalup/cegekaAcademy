﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple.Domain.Services
{
    internal class BankingService
    {
        public decimal CalculateWithdrawalAmount(decimal withdrawalComissionPercent, decimal amount)
        {
            return amount * withdrawalComissionPercent / 100 + amount;
        }

        public void PrintBalanceReport(IEnumerable<Account> accounts)
        {
            var sb = new StringBuilder();
            sb.AppendLine("==========================================");
            foreach (var account in accounts)
            {
                sb.AppendLine(string.Format("{0}    {1}", account.Iban, account.Balance));
            }
            sb.AppendLine("==========================================");
            Console.WriteLine(sb.ToString());
        }

        public void Transfer(WithdrawableAccount from, Account to, decimal amount)
        {
            from.Withdraw(amount);
            to.Deposit(amount);
        }

    }
}
