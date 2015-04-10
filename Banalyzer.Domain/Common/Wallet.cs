using System;
using System.Collections.Generic;

namespace Banalyzer.Domain.Common
{
    public class Wallet
    {
        public Decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public IList<WalletExpense> Expenses { get; set; } 
    }
}