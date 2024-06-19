using backendfepon.ModelConfigurations;
using backendfepon.Models;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Set up configuration models 
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());


        }
    }
}
