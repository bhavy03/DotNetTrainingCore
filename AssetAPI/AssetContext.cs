using AssetAPI.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;

namespace AssetAPI
{
    public class AssetContext : DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }
        //It does 3 things:
        //Accepts configuration options(like connection string, logging, etc.) from Startup.cs.
        //Passes them to the base class (DbContext), so Entity Framework knows how to configure itself.
        //Avoids hardcoding configuration inside the context class.

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AssetMapping> AssetMappings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<SoftwareLicense> Softwares { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AssetsDB;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetMapping>().
                Property(e => e.Status).
                HasConversion<string>();

            modelBuilder.Entity<Customer>().Property(e => e.Role).HasConversion<string>();
        }
    }
}
