using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> modelBuilder)
        {
            modelBuilder.ToTable("STUDENT");
            modelBuilder.HasKey(p => p.Student_Id);

            // Relationships
            modelBuilder.HasOne(e => e.AdministrativeMember)
                .WithOne(c => c.Student)
                .HasForeignKey<AdministrativeMember>(e => e.Student_Id);

        }
    }
}
