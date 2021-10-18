using BvAcademyPortal.Application.Common.Mappings;
using BvAcademyPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Queries.GetTodos
{
    public class UserShortDto: IMapFrom<User>
    {

        public string ProfilePhotoLink { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
