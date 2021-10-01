using BvAcademyPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BvAcademyPortal.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .OwnsOne(b => b.Colour);
        }
    }
}