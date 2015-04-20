using System.Data.Entity;
using Domain.DAL.UnitOfWork;

namespace Banalyzer.DAL.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; } 
    }
}