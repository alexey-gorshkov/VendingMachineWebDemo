using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Purse> Purses { get; set; }
        public DbSet<PurseCoin> PurseCoins { get; set; }
        public DbSet<UserDeposit> UserDeposits { get; set; }
        public DbSet<VMEntity> VMEntities { get; set; }
        public DbSet<VMCreator> VMCreators { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // Configure StudentId as FK for StudentAddress
            //builder.Entity<ApplicationUser>()
            //            .HasOne(s => s.Purse)
            //            .WithOne(p => p.ApplicationUser)
            //            .HasForeignKey<Purse>(p=>p.ApplicationUserRef);

        }
    }
}
