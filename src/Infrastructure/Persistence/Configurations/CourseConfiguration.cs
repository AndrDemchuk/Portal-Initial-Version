using BvAcademyPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BvAcademyPortal.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.StartDate)
                .HasDefaultValue(new DateTime(2021, 8, 15));
            builder.Property(t => t.EndDate)
                .HasDefaultValue(new DateTime(2021, 11, 15));
            builder.Property(t => t.Description)
                .IsRequired();

        }
    }
}