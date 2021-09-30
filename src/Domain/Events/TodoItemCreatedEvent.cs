using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Entities;

namespace BvAcademyPortal.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
