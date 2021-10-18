using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Courses.Queries.GetTodos;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Controllers
{
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUser;

        [HttpGet("current")]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }
        
        [HttpGet]
        public async Task<ActionResult<GetUserVm>> GetUser()
        {
            return await Mediator.Send(new GetUserQuery { UserId=_currentUser.UserId});

        }

    }
}
