using Microsoft.EntityFrameworkCore;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess;

public class ProjectsComposerDbContext(DbContextOptions<ProjectsComposerDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectsComposerDbContext).Assembly);
    
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
}