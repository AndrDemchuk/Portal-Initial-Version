using BvAcademyPortal.Application.Common.Models;
using BvAcademyPortal.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Topics.EventHandlers
{
    public class TopicCreatedEventHandler : INotificationHandler<DomainEventNotification<TopicCreatedEvent>>
    {
        private readonly ILogger<TopicCreatedEventHandler> _logger;

        public TopicCreatedEventHandler(ILogger<TopicCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TopicCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("BvAcademyPortal Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
