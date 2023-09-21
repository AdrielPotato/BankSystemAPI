using BankSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace BankSystem.Infrastructure.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<Account>()
                .HasKey(i => i.ID);

            builder.Entity<Account>()
                .HasMany(i => i.Transactions).WithOne().HasForeignKey(i => i.AccountID);

            builder.Entity<Transaction>()
                .HasKey(i => i.ID);

            builder.Entity<Transaction>().Property(i => i.Amount).HasPrecision(19, 4);


            base.OnModelCreating(builder);
        }
    }
}
