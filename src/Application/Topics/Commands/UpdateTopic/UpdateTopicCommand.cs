using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Topics.Commands.UpdateTopic
{
    public class UpdateTopicCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTopicCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Topics.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Topic), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
