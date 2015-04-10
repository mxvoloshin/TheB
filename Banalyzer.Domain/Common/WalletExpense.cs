using System;

namespace Banalyzer.Domain.Common
{
    public class WalletExpense : IMoneyTransaction
    {
        public MoneyTransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Wallet SourceWallet { get; set; }
    }
}