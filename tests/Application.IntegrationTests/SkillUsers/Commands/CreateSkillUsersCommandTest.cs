using BvAcademyPortal.Application.Common.Exceptions;
using BvAcademyPortal.Application.SkillUsers.Commands.CreateSkillUsers;
using BvAcademyPortal.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.SkillUsers.Commands
{
    public class CreateSkillUsersCommandTest: TestBase
    {
        [TestCaseSource(nameof(ThrowValidationExceptionCases))]
        public void ShouldThrowValidationException(CreateSkillUsersCommand command)
        {
            FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().Throw<ValidationException>();
        }

        static IEnumerable<CreateSkillUsersCommand> ThrowValidationExceptionCases()
        {
            yield return new CreateSkillUsersCommand() { List = null, UserId = null };
            yield return new CreateSkillUsersCommand() { List = null, UserId = "0" };
            yield return new CreateSkillUsersCommand() { List = null, UserId = "id" };
            yield return new CreateSkillUsersCommand() 
            { 
                List = new List<SkillUserCreationDto> 
                {
                    new SkillUserCreationDto() {SkillId = 0, SkillLevel = 0},
                    new SkillUserCreationDto() {SkillId = 0, SkillLevel = (SkillLevel)1},
                    new SkillUserCreationDto() {SkillId = 0, SkillLevel = (SkillLevel)1000},
                }, 
                UserId = "0" };
        }
    }
}
