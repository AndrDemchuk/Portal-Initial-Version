using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class SocialNetworkUsers : AuditableEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int SocialNetworkId { get; set; }

        [ForeignKey(name: nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(name: nameof(SocialNetworkId))]
        public virtual SocialNetwork SocialNetwork { get; set; }
    }
}
