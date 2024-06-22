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
        public DbSet<ContributionPlan> ContributionPlans { get; set; }
        public DbSet<AcademicPeriod> AcademicPeriods { get; set; }
        public DbSet<Career> Careers { get; set;}
        public DbSet<Semester> Semesters { get; set;}
        public DbSet<Contributor> Contributors { get; set;}
        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Student> Students { get; set;}

        public DbSet<AdministrativeMember> AdministrativeMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Permission> Permissions { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Set up configuration models 
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicPeriodConfiguration());
            modelBuilder.ApplyConfiguration(new CareerConfiguration());
            modelBuilder.ApplyConfiguration(new SemesterConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new  StudentConfiguration());
            modelBuilder.ApplyConfiguration(new ContributorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdministrativeMemberConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());


        }
    }
}
