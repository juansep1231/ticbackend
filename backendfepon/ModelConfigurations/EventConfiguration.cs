using backendfepon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace backendfepon.ModelConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> modelBuilder)
        {
            modelBuilder.ToTable("EVENT");
            modelBuilder.HasKey(p => p.Event_Id);

            // Relationships
            modelBuilder.HasOne(e => e.State)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.State_Id);

        }
    }
}
