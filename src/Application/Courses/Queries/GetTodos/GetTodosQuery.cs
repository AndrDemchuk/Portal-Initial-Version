﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Application.Common.Security;
using BvAcademyPortal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Courses.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = await _context.Courses
                    .AsNoTracking()
                    .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
