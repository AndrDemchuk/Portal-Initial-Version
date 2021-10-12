using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Queries.GetTodos
{
    public class UserDto: IMapFrom<User>
    {
        public bool IsAdmin { get; set; }

        public string ProfilePhotoLink { get; set; }

        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string EducationalEstablishment { get; set; }

        public string Faculty { get; set; }

        public EnglishLevel EnglishLevel { get; set; }
    }
}
