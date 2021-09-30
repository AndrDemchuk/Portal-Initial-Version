using BvAcademyPortal.Application.Common.Interfaces;
using System;

namespace BvAcademyPortal.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
