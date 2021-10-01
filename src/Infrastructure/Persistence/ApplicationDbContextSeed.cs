using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.ValueObjects;
using BvAcademyPortal.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course
                {
                    Title = "Shopping",
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

                await context.SaveChangesAsync();
            }
        }
    }
}
