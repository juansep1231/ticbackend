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

            // Foreign key for OriginAccount
            modelBuilder.HasOne(t => t.OriginAccount)
                .WithMany(a => a.OriginTransactions)
                .HasForeignKey(t => t.Origin_Account);

            // Foreign key for DestinationAccount
            modelBuilder.HasOne(t => t.DestinationAccount)
                .WithMany(a => a.DestinationTransactions)
                .HasForeignKey(t => t.Destination_Account);


        }
    }
}
