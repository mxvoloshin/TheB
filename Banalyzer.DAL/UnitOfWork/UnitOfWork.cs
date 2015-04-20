using System;
using System.Data.Entity;

namespace Banalyzer.DAL.UnitOfWork
{
    public class UnitOfWork : IEntityFrameworkUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
        }

        public DbContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}