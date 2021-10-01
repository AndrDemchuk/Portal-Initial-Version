using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Courses.Commands.PurgeCourses
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanPurge")]
    public class PurgeCoursesCommand : IRequest
    {
    }

    public class PurgeCoursesCommandHandler : IRequestHandler<PurgeCoursesCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeCoursesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PurgeCoursesCommand request, CancellationToken cancellationToken)
        {
            _context.Courses.RemoveRange(_context.Courses);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
