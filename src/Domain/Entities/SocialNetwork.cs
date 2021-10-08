using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BvAcademyPortal.Domain.Entities
{
    public class SocialNetwork : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public virtual ICollection<SocialNetworkUsers> SocialNetworkUsers { get; set; }
    }
}
