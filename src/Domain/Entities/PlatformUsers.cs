using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class PlatformUsers : AuditableEntity
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdPlatform { get; set; }

        [ForeignKey(name: "IdUser")]
        public virtual User user { get; set; }

        [ForeignKey(name: "IdPlatform")]
        public virtual Platform platform { get; set; }
    }
}