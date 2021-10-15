using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    [Authorize]
    public class SkillUsersController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUser;

        public SkillUsersController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<ActionResult<List<int>>> Create(IEnumerable<SkillUserCreationDto> list)
        {
            return await Mediator.Send(new CreateSkillUsersCommand() 
            { 
                List = list, 
                UserId = _currentUser.UserId
             });
        }
    }
}
