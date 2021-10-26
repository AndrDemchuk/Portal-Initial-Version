using BvAcademyPortal.Application.Common.Interfaces;
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
        
        [HttpGet]
        public async Task<ActionResult<UserShortDto>> GetUser()
        {
            return await Mediator.Send(new GetUserQuery { UserId = _currentUser.UserId });
        }

    }
}
