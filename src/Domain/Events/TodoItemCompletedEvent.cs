using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
