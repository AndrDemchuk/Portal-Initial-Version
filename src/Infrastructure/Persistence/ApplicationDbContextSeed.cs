using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.ValueObjects;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BvAcademyPortal.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            var areCourses = await context.Courses.AnyAsync().ConfigureAwait(false);
            if (!areCourses)
            {
                context.Courses.Add(new Course
                {
                    Title = ".NET",
                    Description = "Course for .NET Developers",

                });

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
