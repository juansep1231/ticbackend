using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> modelBuilder)
        {
            modelBuilder.ToTable("TRANSACTION");
            modelBuilder.HasKey(p => p.Transaction_Id);
            modelBuilder.HasOne(t => t.Contributor)
                .WithOne(c => c.Transaction)
                .HasForeignKey<Contributor>(t => t.Contributor_Id);


        }
    }
}
