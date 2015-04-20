using System.ComponentModel.DataAnnotations;

namespace Domain.DAL
{
    public abstract class DomainEntity<T> where T:struct 
    {
        [Key]
        public virtual T Id { get; set; }
    }
}