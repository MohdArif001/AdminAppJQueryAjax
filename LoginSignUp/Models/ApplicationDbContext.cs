using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginSignUp.Models
{
    public class ApplicationDbContext:DbContext
    {
        
        //public AppDbContext() : base() { }
        public DbSet<Users> Users { get; set; }
        public DbSet<VerifyAccount> VerifyAccount { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConnection.ConnectionStr);
        }
    }
}
