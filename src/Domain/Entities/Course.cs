using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.ValueObjects;
using System.Collections.Generic;

namespace BvAcademyPortal.Domain.Entities
{
    public class Course : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Colour Colour { get; set; } = Colour.White;
        public IList<Topic> Topics { get; private set; } = new List<Topic>();
    }
}
