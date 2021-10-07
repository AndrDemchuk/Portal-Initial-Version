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

        public int IdUser { get; set; }

        public int IdSocialNetwork { get; set; }

        [ForeignKey(name: "IdUser")]
        public virtual User user { get; set; }

        [ForeignKey(name: "IdSocialNetwork")]
        public virtual SocialNetwork socialNetwork { get; set; }
    }
}
