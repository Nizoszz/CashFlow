using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.Database
{
    internal class CashFlowDbContext : DbContext
    {
        public DbSet<ExpenseEntity> Expenses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=cash_flow;Uid=root;Pwd=verysecret;";
            var serverVersion = new MySqlServerVersion(new Version(9,2,0));
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseEntity>(entity =>
            {
                entity.ToTable("tb_expenses");
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("expense_id");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");
                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");
                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("date");
                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");
                entity.Property(e => e.PaymentType)
                    .HasConversion<int>()
                    .HasColumnName("payment_type");
            });
        }
    }
}
