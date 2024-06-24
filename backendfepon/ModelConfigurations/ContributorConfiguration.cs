using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
    {
        public void Configure(EntityTypeBuilder<Contributor> modelBuilder)
        {
            modelBuilder.ToTable("CONTRIBUTOR");
            modelBuilder.HasKey(p => p.Contributor_Id);

            // Relationships
            modelBuilder.HasOne(cp => cp.Student)
                .WithOne(c => c.Contributor)
                .HasForeignKey<Contributor>(c => c.Contributor_Id);

            // Relationships
            modelBuilder.HasOne(cp => cp.ContributionPlan)
                .WithMany(c => c.Contributors)
                .HasForeignKey(c => c.Plan_Id);

            // Relationships
            modelBuilder.HasOne(cp => cp.Transaction)
                .WithOne(c => c.Contributor)
                .HasForeignKey<Contributor>(c => c.Transaction_Id);


        }
    }
}
