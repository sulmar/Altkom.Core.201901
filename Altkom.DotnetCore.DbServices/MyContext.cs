using Altkom.DotnetCore.DbServices.Configurations;
using Altkom.DotnetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.DbServices
{
    // add package Microsoft.EntityFrameworkCore
    public class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Service> Services { get; set; }


        public MyContext(DbContextOptions<MyContext> options)
            :base(options)
        {

#if DEBUG
          //  this.Database.EnsureDeleted();

            this.Database.EnsureCreated();

#endif

            // this.Database.M
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // add package Microsoft.EntityFrameworkCore.SqlServer
            // optionsBuilder.UseSqlServer()

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new OrderConfiguration())
                .ApplyConfiguration(new ArticleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
