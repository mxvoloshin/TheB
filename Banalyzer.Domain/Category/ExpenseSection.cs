using System;
using System.ComponentModel.DataAnnotations;
using Domain.DAL;

namespace Banalyzer.Domain.Category
{
    public class ExpenseSection : DomainEntity<Int32>
    {
        [Required]
        [MaxLength(30)]
        public String Name { get; set; } 
    }
}