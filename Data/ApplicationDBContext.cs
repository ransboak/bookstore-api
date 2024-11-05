using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Book> Books {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<Comment> Comments {get; set;}
    }
}