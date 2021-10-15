using BvAcademyPortal.Application.SkillTypes.Queries;
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
    public class SkillTypesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SkillTypesVm>> Get()
        {
            return await Mediator.Send(new GetSkillTypesQuery());
        }
    }
}
