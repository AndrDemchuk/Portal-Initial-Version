using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Domain.Events
{
    public class TopicCompletedEvent : DomainEvent
    {
        public TopicCompletedEvent(Topic item)
        {
            Item = item;
        }

        public Topic Item { get; }
    }
}
