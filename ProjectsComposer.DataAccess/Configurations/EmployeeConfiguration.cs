using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.ToTable("Employees");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .HasMaxLength(byte.MaxValue);
        
        builder.Property(e => e.UserName)
            .HasMaxLength(byte.MaxValue);
        
        builder.HasMany(e => e.Projects)
            .WithMany(p => p.Employees);
    }
}