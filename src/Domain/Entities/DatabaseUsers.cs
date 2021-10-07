using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class DatabaseUsers : AuditableEntity
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdDatabase { get; set; }

        [ForeignKey(name: "IdUser")]
        public virtual User user { get; set; }

        [ForeignKey(name: "IdDatabase")]
        public virtual Database database { get; set; }
    }
}