using System;
using System.Security.Cryptography.X509Certificates;

namespace Banalyzer.Domain.Common
{
    public interface IMoneyTransaction
    {
        MoneyTransactionType TransactionType { get; set; }
        DateTime TransactionDate { get; set; }
        Decimal Amount { get; set; }
        Currency Currency { get; set; }
    }
}