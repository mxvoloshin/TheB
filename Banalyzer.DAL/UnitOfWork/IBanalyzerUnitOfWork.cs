using System;
using Banalyzer.DAL.Repository;
using Domain.DAL;
using Domain.DAL.UnitOfWork;

namespace Banalyzer.DAL.UnitOfWork
{
    public interface IBanalyzerUnitOfWork : IUnitOfWork, IDisposable
    {
        BanalyzerContext Context { get; }

        Repository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : DomainEntity<TKey> where TKey : struct;
    }
}