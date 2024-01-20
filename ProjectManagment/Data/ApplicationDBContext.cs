using Microsoft.EntityFrameworkCore;
using ProjectManagment.Models;
using System.Reflection.Emit;

namespace ProjectManagment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<ProjectAssignment>()
                .HasOne(pa => pa.Project)
                .WithMany(p => p.ProjectAssignments)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<ProjectAssignment>()
               .HasOne(pa => pa.Employee)
               .WithMany(e => e.ProjectAssignments)
               .HasForeignKey(pa => pa.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
