using AutoMapper;
using BvAcademyPortal.Application.Users.Commands.CreateUser;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using BvAcademyPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.WebUI.Configuration
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() : this("MainProfile") { }

        protected AutoMapperConfig(string profileName) : base(profileName)
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(it=>it.Id, opt=>opt.Ignore())
                .ForMember(it => it.SkillsUsers, opt => opt.Ignore())
                .ForMember(it=>it.CourseUsers, opt => opt.Ignore())
                .ForMember(it => it.SocialNetworkUsers, opt => opt.Ignore());

            CreateMap<User, CreateUserCommand>();
            CreateMap<UserDto, User>()
                .ForMember(it => it.SkillsUsers, opt => opt.Ignore())
                .ForMember(it => it.CourseUsers, opt => opt.Ignore())
                .ForMember(it => it.SocialNetworkUsers, opt => opt.Ignore()); 

        }
    }
}
