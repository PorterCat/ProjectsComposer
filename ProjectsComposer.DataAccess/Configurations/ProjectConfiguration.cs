using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectsComposer.Core.Models;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.DataAccess.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title)
               .HasMaxLength(Project.MaxTitleLength)
               .IsRequired();

        builder.Property(p => p.CustomerCompanyName)
               .HasMaxLength(byte.MaxValue);
        
        builder.Property(p => p.ContractorCompanyName)
               .HasMaxLength(byte.MaxValue);

        builder.Property(p => p.StartDate)
               .HasColumnType("date");
        
        builder.Property(p => p.EndDate)
               .HasColumnType("date")
               .IsRequired(false);

        builder.HasOne(p => p.Leader)
               .WithMany(l => l.LeadingProjects)
               .HasForeignKey(e => e.LeaderId)
               .IsRequired(false);
        
        builder.HasMany(p => p.Employees)
               .WithMany(e => e.Projects);
    }
}