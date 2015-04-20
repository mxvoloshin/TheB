using System;
using System.ComponentModel.DataAnnotations;
using Domain.DAL;

namespace Banalyzer.Domain.Category
{
    public class ExpenseTag : DomainEntity<Int32>
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }
        [Required]
        public virtual ExpenseCategory Category { get; set; }
    }
}