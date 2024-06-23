using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class AdministrativeMemberConfiguration : IEntityTypeConfiguration<AdministrativeMember>
    {
        public void Configure(EntityTypeBuilder<AdministrativeMember> modelBuilder)
        {
            modelBuilder.ToTable("ADMINISTRATIVE_MEMBER");
            modelBuilder.HasKey(p => p.Administrative_Member_Id);

            // Relationships
            modelBuilder.HasOne(e => e.Student)
                .WithOne(c => c.AdministrativeMember)
                .HasForeignKey<AdministrativeMember>(e => e.Student_Id);

            modelBuilder.HasOne(e => e.User)
                .WithOne(c => c.AdministrativeMember)
                .HasForeignKey<AdministrativeMember>(e => e.User_Id);





        }
    }
}
