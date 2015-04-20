using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.DAL;

namespace Banalyzer.Domain.Category
{
    public class ExpenseCategory : DomainEntity<Int32>
    {
        [Required]
        [MaxLength(30)]
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual IList<ExpenseSubCategory> SubCategories { get; set; }
    }
}