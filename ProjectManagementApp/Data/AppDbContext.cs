using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectNumber)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
