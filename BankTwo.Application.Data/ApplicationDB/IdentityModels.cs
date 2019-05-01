using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BankTwo.Application.Core.Entities;

namespace BankTwo.Application.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Branch> Branch { get; set; }

        public DbSet<MainCategories> MainCategories { get; set; }

        public DbSet<GLCategory> GLCategories { get; set; }

        public DbSet<GLAccount> GLAccounts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerAccount> CustomerAccount { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<LoanAccount> LoanAccounts { get; set; }

        public DbSet<TellerPost> TellerPosts { get; set; }

        public DbSet<EOD> EOD { get; set; }

        public DbSet<AccountConfiguration_Savings> SavingsConfiguration { get; set; }

        public DbSet<AccountConfiguration_Current> CurrentConfiguration { get; set; }

        public DbSet<AccountConfiguration_Loan> LoanConfiguration { get; set; }

        public ApplicationDbContext()
            : base("DatabaseConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
    }
}