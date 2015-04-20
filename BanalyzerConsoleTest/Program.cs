using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banalyzer.DAL;
using Banalyzer.DAL.Repository;
using Banalyzer.DAL.UnitOfWork;
using Banalyzer.Domain.Category;

namespace BanalyzerConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sf = SessionFactory.CreateSessionFactory();
            using (var uof = new UnitOfWork(sf))
            {
                var repo = new Repository<ExpenseSection, Int32>(uof.Session);

                var section = new ExpenseSection();
                section.Name = "temp";

                repo.Add(section);

                uof.CommitTransaction();
            }
        }
    }
}
