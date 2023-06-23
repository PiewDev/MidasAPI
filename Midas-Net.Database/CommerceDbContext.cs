using Microsoft.EntityFrameworkCore;
using Midas.Net.Database.Products;
using Midas.Net.Database.Sales;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Database.ProductTypes;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Midas.Net.Database.SaleDetails;
using Midas.Net.Database.Log;
using Microsoft.Extensions.Configuration;

namespace Midas.Net.Database
{

    public class CommerceDbContext : DbContext
    {

        private string _connectionString;
        public DbSet<DbLog> Logs { get; set; }
        public DbSet<DbSale> Sales { get; set; }
        public DbSet<DbSaleDetail> SaleProducts { get; set; }
        public DbSet<DbProductType> ProductTypes { get; set; }
        public DbSet<DbProduct> Products { get; set; }
        public CommerceDbContext() { }

        public CommerceDbContext(DbContextOptions<CommerceDbContext> options) : base(options) { }

        public CommerceDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbSaleConfiguration());
            modelBuilder.ApplyConfiguration(new DbSaleDetailConfiguration());
            modelBuilder.ApplyConfiguration(new DbProductConfiguration());
            modelBuilder.ApplyConfiguration(new DbProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DbLogConfiguration());
        }
    }

}
