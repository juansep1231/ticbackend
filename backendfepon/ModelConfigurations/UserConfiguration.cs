using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("USER");
            modelBuilder.HasKey(p => p.User_Id);

            // Relationships
            modelBuilder.HasOne(e => e.AdministrativeMember)
                .WithOne(c => c.User)
                .HasForeignKey<AdministrativeMember>(e => e.User_Id);

        }

    }
}
