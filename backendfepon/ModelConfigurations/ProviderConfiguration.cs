using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendfepon.ModelConfigurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> modelBuilder)
        {
            modelBuilder.ToTable("PROVIDER");
            modelBuilder.HasKey(p => p.Provider_Id);


        }
    }
}
