using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Topics.EventHandlers
{
    public class TopicCompletedEventHandler : INotificationHandler<DomainEventNotification<TopicCompletedEvent>>
    {
        private readonly ILogger<TopicCompletedEventHandler> _logger;

        public TopicCompletedEventHandler(ILogger<TopicCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TopicCompletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("BvAcademyPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
