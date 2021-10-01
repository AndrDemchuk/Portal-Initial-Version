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
                    Colour = Colour.Blue,
                    Topics =
                    {
                        new Topic { Title = "Apples", Done = true },
                        new Topic { Title = "Milk", Done = true },
                        new Topic { Title = "Bread", Done = true },
                        new Topic { Title = "Toilet paper" },
                        new Topic { Title = "Pasta" },
                        new Topic { Title = "Tissues" },
                        new Topic { Title = "Tuna" },
                        new Topic { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
