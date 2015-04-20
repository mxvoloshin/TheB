using System;
using System.ComponentModel.DataAnnotations;
using Domain.DAL;

namespace Banalyzer.Domain.Common
{
    public class Currency : DomainEntity<Int32>
    {
        [Required]
        [MaxLength(3)]
        public String Code { get; set; }
        [MaxLength(100)]
        public String Description { get; set; }
    }
}