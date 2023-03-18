using Financify_Api.Data.Map;
using Financify_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Financify_Api.Data
{
    public class FinancifyContext : DbContext
    {
        public FinancifyContext(DbContextOptions<FinancifyContext> options)
            : base(options)
        {
        }

        public DbSet<Charge> Charges { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChargeMap());
            modelBuilder.ApplyConfiguration(new AccountMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
