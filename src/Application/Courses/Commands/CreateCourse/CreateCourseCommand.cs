using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCourseCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Course();

            entity.Title = request.Title;

            _context.Courses.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
