using FinancialTamkeen_BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTamkeen_BlogAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>();
        }
    }
}
