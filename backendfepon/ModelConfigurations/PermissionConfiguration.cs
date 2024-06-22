using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> modelBuilder)
        {
            modelBuilder.ToTable("PERMISSION");
            modelBuilder.HasKey(p => p.Permission_Id);

            // Relationships
            modelBuilder.HasOne(e => e.Event)
                .WithOne(c => c.Permission)
                .HasForeignKey<Permission>(e => e.Event_Id);

        }
    }
}
