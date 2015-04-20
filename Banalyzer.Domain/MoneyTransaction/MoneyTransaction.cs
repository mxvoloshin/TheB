using System;
using System.ComponentModel.DataAnnotations;
using Banalyzer.Domain.Common;
using Domain.DAL;

namespace Banalyzer.Domain.MoneyTransaction
{
    public abstract class MoneyTransaction : DomainEntity<Int32>
    {
        [Required]
        public MoneyTransactionType TransactionType { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        [Required]
        public virtual Currency Currency { get; set; }
    }
}