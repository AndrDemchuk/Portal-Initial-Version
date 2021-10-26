﻿using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BvAcademyPortal.Domain.Entities
{
    public class CourseUsers : BaseEntity<int>
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }

        public bool IsMentor { get; set; }

        [ForeignKey(name: nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(name: nameof(CourseId))]
        public virtual Course Course { get; set; }
    }
}
