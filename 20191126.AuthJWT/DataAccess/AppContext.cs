using _20191126.AuthJWT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20191126.AuthJWT.DataAccess
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User 
            { 
                Username = "Vasya",
                Password = "123456"
            });
        }
    }
}
