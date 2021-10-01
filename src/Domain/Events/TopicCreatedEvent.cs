using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Domain.Events
{
    public class TopicCreatedEvent : DomainEvent
    {
        public TopicCreatedEvent(Topic item)
        {
            Item = item;
        }

        public Topic Item { get; }
    }
}
