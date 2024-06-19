using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


//Setup Product model configurations
namespace backendfepon.ModelConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.ToTable("PRODUCT");
            modelBuilder.HasKey(p => p.Product_Id);
            modelBuilder
               .HasOne(p => p.State)
               .WithMany(s => s.Products)
               .HasForeignKey(p => p.State_Id);
        }
    }
}
