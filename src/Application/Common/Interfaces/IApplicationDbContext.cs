using BvAcademyPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Course> Courses { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
