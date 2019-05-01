namespace BankTwo.Application.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AccountConfiguration_Current",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentCreditInterestRate = c.Double(nullable: false),
                        CurrentMinimumBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IEAccountId = c.Int(nullable: false),
                        COT = c.Double(nullable: false),
                        COTIncomeId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.COTIncomeId, cascadeDelete: false)
                .ForeignKey("dbo.GLAccounts", t => t.IEAccountId, cascadeDelete: false)
                .Index(t => t.IEAccountId)
                .Index(t => t.COTIncomeId);
            
            CreateTable(
                "dbo.GLAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncryptedId = c.String(),
                        GLAccountName = c.String(nullable: false),
                        GLAccountCode = c.Int(nullable: false),
                        GLCategoryId = c.Int(nullable: false),
                        User = c.Int(nullable: false),
                        GLAccountType = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        IsVault = c.Boolean(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.GLCategories", t => t.GLCategoryId, cascadeDelete: true)
                .Index(t => t.GLCategoryId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.GLCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncryptedId = c.String(),
                        GLCategoryName = c.String(nullable: false),
                        MainCategoriesId = c.Int(nullable: false),
                        GLCategoryDescription = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainCategories", t => t.MainCategoriesId, cascadeDelete: true)
                .Index(t => t.MainCategoriesId);
            
            CreateTable(
                "dbo.MainCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainCategoryName = c.String(nullable: false),
                        MainCategoryAccountType = c.String(nullable: false),
                        MainCategoryOperation = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncryptedId = c.String(),
                        CustomerId = c.Int(nullable: false),
                        CAccountName = c.String(nullable: false),
                        CAccountNumber = c.Int(nullable: false),
                        AccountTypeEnum = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        BranchId = c.Int(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DailyInterest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfWithdrawals = c.Int(nullable: false),
                        Charges = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncryptedId = c.String(),
                        FirstName = c.String(nullable: false),
                        IdentificationNumber = c.Int(nullable: false),
                        LastName = c.String(nullable: false),
                        OtherNames = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EODs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsClosed = c.Boolean(nullable: false),
                        FinancialDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoanAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EncryptedId = c.String(),
                        CAccountName = c.String(nullable: false),
                        CAccountNumber = c.Int(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        BranchId = c.Int(nullable: false),
                        CustomerAccountId = c.Int(nullable: false),
                        LoanAccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanPrincipal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanDuration = c.Int(nullable: false),
                        MonthlyLoanBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanStartDate = c.DateTime(nullable: false),
                        LoanDueDate = c.DateTime(nullable: false),
                        LoanPaymentSchedule = c.Int(nullable: false),
                        LoanStats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.AccountConfiguration_Loan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitInterestRate = c.Double(nullable: false),
                        InterestIncomeId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.InterestIncomeId, cascadeDelete: false)
                .Index(t => t.InterestIncomeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AccountConfiguration_Savings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SavingsCreditInterestRate = c.Double(nullable: false),
                        SavingsMinimumBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IEAccountId = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateLastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.IEAccountId, cascadeDelete: false)
                .Index(t => t.IEAccountId);
            
            CreateTable(
                "dbo.TellerPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerAccountId = c.Int(nullable: false),
                        GLAccountId = c.Int(nullable: false),
                        DebitAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        TellerPostingType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerAccountId, cascadeDelete: true)
                .ForeignKey("dbo.GLAccounts", t => t.GLAccountId, cascadeDelete: false)
                .Index(t => t.CustomerAccountId)
                .Index(t => t.GLAccountId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitGLAccountId = c.Int(nullable: false),
                        CreditGLAccountId = c.Int(nullable: false),
                        DebitAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreditAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.CreditGLAccountId, cascadeDelete: false)
                .ForeignKey("dbo.GLAccounts", t => t.DebitGLAccountId, cascadeDelete: false)
                .Index(t => t.DebitGLAccountId)
                .Index(t => t.CreditGLAccountId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EncryptedId = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        OtherNames = c.String(nullable: false),
                        UserRole = c.String(),
                        GLAccountId = c.Int(),
                        BranchId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.GLAccounts", t => t.GLAccountId)
                .Index(t => t.GLAccountId)
                .Index(t => t.BranchId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "GLAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Transactions", "DebitGLAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.Transactions", "CreditGLAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.TellerPosts", "GLAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.TellerPosts", "CustomerAccountId", "dbo.CustomerAccounts");
            DropForeignKey("dbo.AccountConfiguration_Savings", "IEAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AccountConfiguration_Loan", "InterestIncomeId", "dbo.GLAccounts");
            DropForeignKey("dbo.LoanAccounts", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.CustomerAccounts", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.AccountConfiguration_Current", "IEAccountId", "dbo.GLAccounts");
            DropForeignKey("dbo.AccountConfiguration_Current", "COTIncomeId", "dbo.GLAccounts");
            DropForeignKey("dbo.GLAccounts", "GLCategoryId", "dbo.GLCategories");
            DropForeignKey("dbo.GLCategories", "MainCategoriesId", "dbo.MainCategories");
            DropForeignKey("dbo.GLAccounts", "BranchId", "dbo.Branches");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "BranchId" });
            DropIndex("dbo.AspNetUsers", new[] { "GLAccountId" });
            DropIndex("dbo.Transactions", new[] { "CreditGLAccountId" });
            DropIndex("dbo.Transactions", new[] { "DebitGLAccountId" });
            DropIndex("dbo.TellerPosts", new[] { "GLAccountId" });
            DropIndex("dbo.TellerPosts", new[] { "CustomerAccountId" });
            DropIndex("dbo.AccountConfiguration_Savings", new[] { "IEAccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AccountConfiguration_Loan", new[] { "InterestIncomeId" });
            DropIndex("dbo.LoanAccounts", new[] { "BranchId" });
            DropIndex("dbo.CustomerAccounts", new[] { "BranchId" });
            DropIndex("dbo.GLCategories", new[] { "MainCategoriesId" });
            DropIndex("dbo.GLAccounts", new[] { "BranchId" });
            DropIndex("dbo.GLAccounts", new[] { "GLCategoryId" });
            DropIndex("dbo.AccountConfiguration_Current", new[] { "COTIncomeId" });
            DropIndex("dbo.AccountConfiguration_Current", new[] { "IEAccountId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Transactions");
            DropTable("dbo.TellerPosts");
            DropTable("dbo.AccountConfiguration_Savings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AccountConfiguration_Loan");
            DropTable("dbo.LoanAccounts");
            DropTable("dbo.EODs");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerAccounts");
            DropTable("dbo.MainCategories");
            DropTable("dbo.GLCategories");
            DropTable("dbo.GLAccounts");
            DropTable("dbo.AccountConfiguration_Current");
            DropTable("dbo.Branches");
        }
    }
}
