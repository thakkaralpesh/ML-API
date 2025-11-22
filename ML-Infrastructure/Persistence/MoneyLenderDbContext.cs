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
        public DbSet<Lender> Lenders => Set<Lender>();

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

            modelBuilder.Entity<Lender>(entity =>
            {
                entity.HasIndex(l => l.UserId).IsUnique();

                entity.Property(l => l.FirstName).HasMaxLength(100);
                entity.Property(l => l.LastName).HasMaxLength(100);
                entity.Property(l => l.MobileNumber).HasMaxLength(20);
                entity.Property(l => l.BusinessName).HasMaxLength(200);
                entity.Property(l => l.LicenseNumber).HasMaxLength(100);
                entity.Property(l => l.Jurisdiction).HasMaxLength(200);
                entity.Property(l => l.BankName).HasMaxLength(200);
                entity.Property(l => l.AccountNumber).HasMaxLength(50);
                entity.Property(l => l.IfscCode).HasMaxLength(20);
                entity.Property(l => l.Branch).HasMaxLength(200);

                entity.HasOne(l => l.User)
                    .WithOne()              // you can change this later to navigation
                    .HasForeignKey<Lender>(l => l.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
