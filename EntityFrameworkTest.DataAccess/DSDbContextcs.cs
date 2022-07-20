using EntityFrameworkTest.Data.Model;
using EntityFrameworkTest.Model.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkTest.DataAccess
{
    class DSDbContextcs : DbContext
    {
        public DSDbContextcs() : base()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

            var connectionString = config.GetConnectionString("mainDB");
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Address>().HasKey(a => new { a.Line1, a.City, a.Postcode });
            modelBuilder.Entity<Payment>().HasOne(p => p.AssociatedUser).WithMany(u => u.PaymentsMade).HasForeignKey(p => p.AdeptReference).HasPrincipalKey(u => u.AdeptReference);


            modelBuilder.Entity<Phone>().HasOne<User>().WithMany(u => u.Phones).HasForeignKey(p => p.owner).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Email>().HasOne<User>().WithMany(u => u.Emails).HasForeignKey(e => e.owner).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>().HasOne<User>().WithMany(u => u.Addresses).HasForeignKey(a => a.Owner).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
