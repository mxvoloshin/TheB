using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
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
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<TEntity> All()
        {
            return _context.Set<TEntity>();
        }
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}