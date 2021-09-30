using BvAcademyPortal.Domain.Common;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
