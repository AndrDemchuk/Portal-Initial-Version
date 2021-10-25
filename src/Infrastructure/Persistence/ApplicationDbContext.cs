using System.Linq;
using BvAcademyPortal.Application.Common.Interfaces;
using BvAcademyPortal.Domain.Common;
using BvAcademyPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BvAcademyPortal.Infrastructure.Extensions;

namespace BvAcademyPortal.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SkillType> SkillTypes { get; set; }

        public DbSet<SkillUser> SkillUsers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries().Where(e => e is IAuditable || e is IUseSoftDelete);
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            if (entry.Entity is IAuditable addedEntity)
                            {
                                addedEntity.CreatedBy = _currentUserService.UserId;
                                addedEntity.Created = _dateTime.Now;
                            }

                            break;
                        }


                    case EntityState.Modified:
                        {
                            if (entry.Entity is IAuditable modifiedEntity)
                            {
                                modifiedEntity.LastModifiedBy = _currentUserService.UserId;
                                modifiedEntity.LastModified = _dateTime.Now;
                            }

                            break;
                        }

                    case EntityState.Deleted:
                        {
                            if (entry.Entity is IUseSoftDelete softDeleteEntity)
                            {
                                entry.State = EntityState.Modified;
                                softDeleteEntity.IsDeleted = true;
                            }

                            if (entry.Entity is IAuditable deletedEntity)
                            {
                                deletedEntity.LastModifiedBy = _currentUserService.UserId;
                                deletedEntity.LastModified = _dateTime.Now;
                            }

                            break;
                        }
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyGlobalFilters<IUseSoftDelete>(e => e.IsDeleted != true);

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
