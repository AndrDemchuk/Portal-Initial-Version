using AutoMapper;
using BvAcademyPortal.Application.Users.Commands.CreateUser;
using BvAcademyPortal.Application.Users.Queries.GetTodos;
using BvAcademyPortal.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace BvAcademyPortal.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<CreateUserCommand, User>()
                .ForMember(it => it.Id, opt => opt.Ignore())
                .ForMember(it => it.SkillsUsers, opt => opt.Ignore())
                .ForMember(it => it.CourseUsers, opt => opt.Ignore())
                .ForMember(it => it.SocialNetworkUsers, opt => opt.Ignore());

            CreateMap<User, CreateUserCommand>();
            CreateMap<UserDto, User>()
                .ForMember(it => it.SkillsUsers, opt => opt.Ignore())
                .ForMember(it => it.CourseUsers, opt => opt.Ignore())
                .ForMember(it => it.SocialNetworkUsers, opt => opt.Ignore());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") 
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");
                
                methodInfo?.Invoke(instance, new object[] { this });

            }

        }
    }
}