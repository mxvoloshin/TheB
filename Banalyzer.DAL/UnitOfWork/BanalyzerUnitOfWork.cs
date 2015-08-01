using System;
using System.Threading.Tasks;
using Banalyzer.DAL.Repository;
using Domain.DAL;

namespace Banalyzer.DAL.UnitOfWork
{
    public class BanalyzerUnitOfWork : IBanalyzerUnitOfWork
    {
        private readonly BanalyzerContext _context = new BanalyzerContext();
        private long _hasActiveCommitTransaction = 0;
        public async Task CommitTransaction()
        {
            System.Threading.Interlocked.Exchange(ref _hasActiveCommitTransaction, 1);
            await _context.SaveChangesAsync();
            System.Threading.Interlocked.Exchange(ref _hasActiveCommitTransaction, 0);
            Dispose();
        }

        public BanalyzerContext Context
        {
            get { return _context; }
        }

        public Repository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : DomainEntity<TKey> where TKey : struct
        {
            return new Repository<TEntity, TKey>(_context);
        }

        public void Dispose()
        {
            System.Threading.Interlocked.Read(ref _hasActiveCommitTransaction);
            if (_hasActiveCommitTransaction != 1)
            {
                _context.Dispose();
            }
        }
    }
}