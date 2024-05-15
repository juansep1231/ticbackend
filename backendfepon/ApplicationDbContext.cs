using backendfepon.Models;
using Microsoft.EntityFrameworkCore;

namespace backendfepon
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Usuario { get; set; }
    }
}
