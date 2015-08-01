using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Banalyzer.Domain.MoneyTransaction;
using Domain.DAL;

namespace Banalyzer.Domain.Common
{
    public class Deposite : DomainEntity<Guid>
    {
        public Deposite()
        {
            OpenedDate = DateTime.Now;
            CloseDate = DateTime.Now;
        }

        [Required]
        public DateTime OpenedDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }
        [Required]
        [MaxLength(50)]
        public String BankName { get; set; }
        [Required]
        public Double OpenedAmount { get; set; }
        [Required]
        public Double CurrentAmount { get; set; }
        [Required]
        public Int32 Currency_Id { get; set; }
        [ForeignKey("Currency_Id")]
        public virtual Currency Currency { get; set; }
        [Required]
        public Double Percent { get; set; }
        [Required]
        public String Owner { get; set; }
        public virtual IList<DepositeMoneyTransaction> MoneyTransactions { get; set; }
    }
}