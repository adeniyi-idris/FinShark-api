using System;
using System.Collections.Generics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> stocks {get; set;}

        public DbSet<Comment> comments {get; set;}

        public DbSet<Portfolio> portfolios {get; set;}
    }
}