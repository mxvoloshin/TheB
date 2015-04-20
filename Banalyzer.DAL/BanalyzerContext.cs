using System.Data.Entity;
using Banalyzer.Domain.Category;
using Banalyzer.Domain.Common;
using Banalyzer.Domain.MoneyTransaction;

namespace Banalyzer.DAL
{
    public class BanalyzerContext : DbContext
    {
        public BanalyzerContext()
            : base("BanalyzerContext")
        {
            
        }

        //categories
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseSection> ExpenseSections { get; set; }
        public DbSet<ExpenseSubCategory> ExpenseSubCategories { get; set; }
        public DbSet<ExpenseTag> ExpenseTags { get; set; } 

        //common
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deposite> Deposites { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletExpense> WalletExpenses { get; set; }
        
        //money transactions
        public DbSet<DepositeMoneyTransaction> DepositeMoneyTransactions { get; set; }
        public DbSet<WalletMoneyTransaction> WalletMoneyTransactions { get; set; } 

    }
}