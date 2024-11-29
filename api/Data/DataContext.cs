using System;
using System.Collections.Generics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class DataContext:IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<stock> stocks {get; set;}

        public DbSet<comment> comments {get; set;}

        public DbSet<Portfolio> portfolios {get; set;}


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
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}))

            builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

            builder.Entity<stock>()
            .HasOne(u => u.stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.stockId);

            List<IdentityRole> roles =  new List<IdentityRole>
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
                }
            };
        }
    }
}