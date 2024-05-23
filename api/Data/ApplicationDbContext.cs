using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            builder.Entity<Portfolio>(x => 
            {
                x.HasKey(p => new { p.AppUserId, p.StockId } );

                x.HasOne(u => u.AppUser)
                .WithMany(x => x.Portfolios)
                .HasForeignKey(p => p.AppUserId);

                x.HasOne(u => u.Stock)
                .WithMany(x => x.Portfolios)
                .HasForeignKey(p => p.StockId);

                x.ToTable("Portfolios");
            });

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}