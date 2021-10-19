using BvAcademyPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<SkillType> SkillTypes { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<SkillUser> SkillUsers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
