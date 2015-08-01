using System;
using System.Threading.Tasks;

namespace Domain.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitTransaction();
    }
}