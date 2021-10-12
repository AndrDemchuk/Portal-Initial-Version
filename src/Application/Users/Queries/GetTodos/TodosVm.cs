using BvAcademyPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Users.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<UserDto> Lists { get; set; }
    }
}
