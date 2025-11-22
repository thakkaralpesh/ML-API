using Microsoft.EntityFrameworkCore;
using ML_Domain.Entities;

namespace ML_Infrastructure.Persistence
{
    public class MoneyLenderDbContext:DbContext
    {
        public MoneyLenderDbContext(DbContextOptions<MoneyLenderDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.MobileNumber).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.Role).HasMaxLength(30);
            });
        }
    }
}
