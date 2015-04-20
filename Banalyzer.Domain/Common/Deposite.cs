using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Banalyzer.Domain.MoneyTransaction;
using Domain.DAL;

namespace Banalyzer.Domain.Common
{
    public class Deposite : DomainEntity<Guid>
    {
        [Required]
        public DateTime OpenedDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        [Required]
        [MaxLength(50)]
        public String BankName { get; set; }
        [Required]
        public Decimal OpenedAmount { get; set; }
        [Required]
        public Decimal CurrentAmount { get; set; }
        [Required]
        public Currency Currency { get; set; }
        [Required]
        public Single Percent { get; set; }
        public virtual IList<DepositeMoneyTransaction> MoneyTransactions { get; set; }
    }
}