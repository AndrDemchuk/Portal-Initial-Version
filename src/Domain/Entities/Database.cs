﻿using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using BvAcademyPortal.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class Database : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DatabaseUsers> databaseUsers { get; set; }
    }
}
