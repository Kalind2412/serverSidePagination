using NimapInfotechDemo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NimapInfotechDemo.DAL
{
    public class NimapInfotechContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}