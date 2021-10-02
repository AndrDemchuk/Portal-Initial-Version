using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        public IdentityService()
        {
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
          // todo implement me
          return null;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            // todo implement me
            return false;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            // todo implement me
            return true;
        }
    }
}
