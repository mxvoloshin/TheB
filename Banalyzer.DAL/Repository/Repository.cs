using System;
using System.Data.Entity;
using Domain.DAL;
using Domain.DAL.Repository;

namespace Banalyzer.DAL.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : DomainEntity<TKey> 
        where TKey : struct
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}