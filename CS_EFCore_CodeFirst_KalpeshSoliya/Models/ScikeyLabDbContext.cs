using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EFCore_CodeFirst_KalpeshSoliya.Models
{
    public class ScikeyLabDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Vender> Venders { get; set; }
        public DbSet<VendorProduct> VendorProduct { get; set; }

        public ScikeyLabDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SRKSUR5210LT;Initial Catalog=Company;User Id=sa;Password=Kds@12345;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Set One-To-One Relationship from Product-To-Category
            //Set One-To-Many relationshipe from Category-To-Product
            modelBuilder.Entity<Products>()
                .HasOne(c => c.Categtory) //One-to-One
                .WithMany(p => p.Products) //Many-To-Many
                .HasForeignKey(f => f.CategoryRowId); //Foreign Key

            //Many to Many
            modelBuilder.Entity<VendorProduct>()
                .HasOne(p => p.Product)
                .WithMany(vp => vp.VendorProducts)
                .HasForeignKey(f => f.ProductRowId); //Foreign Key

            //Indirect Many to Many
            modelBuilder.Entity<VendorProduct>()
                .HasOne(p => p.Vender)
                .WithMany(vp => vp.VendorProducts)
                .HasForeignKey(f => f.VenderRowId); //Foreign Key

            base.OnModelCreating(modelBuilder);
        }
    }
}
