﻿using BvAcademyPortal.Application.Courses.Queries.GetTodos;
using BvAcademyPortal.Domain.Entities;
using BvAcademyPortal.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.IntegrationTests.Courses.Queries
{
    using static Testing;

    public class GetTodosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllListsAndItems()
        {
            await AddAsync(new Course
            {
                Title = "Shopping",
                Colour = Colour.Blue,
                Topics =
                    {
                        new Topic { Title = "Apples", Done = true },
                        new Topic { Title = "Milk", Done = true },
                        new Topic { Title = "Bread", Done = true },
                        new Topic { Title = "Toilet paper" },
                        new Topic { Title = "Pasta" },
                        new Topic { Title = "Tissues" },
                        new Topic { Title = "Tuna" }
                    }
            });

            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
            result.Lists.First().Items.Should().HaveCount(7);
        }
    }
}