namespace Banalyzer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 3),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepositeMoneyTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Deposite_Id = c.Guid(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Comment = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deposites", t => t.Deposite_Id, cascadeDelete: true)
                .Index(t => t.Deposite_Id);
            
            CreateTable(
                "dbo.Deposites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OpenedDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(nullable: false),
                        BankName = c.String(nullable: false, maxLength: 50),
                        OpenedAmount = c.Double(nullable: false),
                        CurrentAmount = c.Double(nullable: false),
                        Currency_Id = c.Int(nullable: false),
                        Percent = c.Double(nullable: false),
                        Owner = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id, cascadeDelete: true)
                .Index(t => t.Currency_Id);
            
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseSubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ExpenseSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExpenseTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseCategories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.WalletExpenses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ExpenseDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Section_Id = c.Int(nullable: false),
                        SourceWallet_Id = c.Guid(nullable: false),
                        SubCategory_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExpenseSections", t => t.Section_Id, cascadeDelete: true)
                .ForeignKey("dbo.Wallets", t => t.SourceWallet_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseSubCategories", t => t.SubCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseTags", t => t.Tag_Id)
                .Index(t => t.Section_Id)
                .Index(t => t.SourceWallet_Id)
                .Index(t => t.SubCategory_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                        Amount = c.Double(nullable: false),
                        Currency_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id, cascadeDelete: true)
                .Index(t => t.Currency_Id);
            
            CreateTable(
                "dbo.WalletMoneyTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionType = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Comment = c.String(maxLength: 255),
                        Wallet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .Index(t => t.Wallet_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WalletExpenses", "Tag_Id", "dbo.ExpenseTags");
            DropForeignKey("dbo.WalletExpenses", "SubCategory_Id", "dbo.ExpenseSubCategories");
            DropForeignKey("dbo.WalletExpenses", "SourceWallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.WalletMoneyTransactions", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.Wallets", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.WalletExpenses", "Section_Id", "dbo.ExpenseSections");
            DropForeignKey("dbo.ExpenseTags", "Category_Id", "dbo.ExpenseCategories");
            DropForeignKey("dbo.ExpenseSubCategories", "Category_Id", "dbo.ExpenseCategories");
            DropForeignKey("dbo.DepositeMoneyTransactions", "Deposite_Id", "dbo.Deposites");
            DropForeignKey("dbo.Deposites", "Currency_Id", "dbo.Currencies");
            DropIndex("dbo.WalletMoneyTransactions", new[] { "Wallet_Id" });
            DropIndex("dbo.Wallets", new[] { "Currency_Id" });
            DropIndex("dbo.WalletExpenses", new[] { "Tag_Id" });
            DropIndex("dbo.WalletExpenses", new[] { "SubCategory_Id" });
            DropIndex("dbo.WalletExpenses", new[] { "SourceWallet_Id" });
            DropIndex("dbo.WalletExpenses", new[] { "Section_Id" });
            DropIndex("dbo.ExpenseTags", new[] { "Category_Id" });
            DropIndex("dbo.ExpenseSubCategories", new[] { "Category_Id" });
            DropIndex("dbo.Deposites", new[] { "Currency_Id" });
            DropIndex("dbo.DepositeMoneyTransactions", new[] { "Deposite_Id" });
            DropTable("dbo.WalletMoneyTransactions");
            DropTable("dbo.Wallets");
            DropTable("dbo.WalletExpenses");
            DropTable("dbo.ExpenseTags");
            DropTable("dbo.ExpenseSections");
            DropTable("dbo.ExpenseSubCategories");
            DropTable("dbo.ExpenseCategories");
            DropTable("dbo.Deposites");
            DropTable("dbo.DepositeMoneyTransactions");
            DropTable("dbo.Currencies");
        }
    }
}
