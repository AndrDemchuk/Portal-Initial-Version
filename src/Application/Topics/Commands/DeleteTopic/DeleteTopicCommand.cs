using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Topics.Commands.DeleteTopic
{
    public class DeleteTopicCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTopicCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Topics.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Topic), request.Id);
            }

            _context.Topics.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
