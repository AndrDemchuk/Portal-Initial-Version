using BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUser;
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
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSkillUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
