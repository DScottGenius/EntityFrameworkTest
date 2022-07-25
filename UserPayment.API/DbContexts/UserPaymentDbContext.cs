using Microsoft.EntityFrameworkCore;
using UserPayment.API.Entities;

namespace UserPayment.API.DbContexts
{
    public class UserPaymentDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public UserPaymentDbContext(DbContextOptions<UserPaymentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasOne(p => p.AssociatedUser).WithMany(u => u.PaymentsMade).HasForeignKey(p => p.AdeptReference).HasPrincipalKey(u => u.AdeptReference);
            base.OnModelCreating(modelBuilder);

        }
    }
}
