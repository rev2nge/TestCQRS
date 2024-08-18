using Mapster;
using Microsoft.EntityFrameworkCore;
using TestCQRS.Application.Queries;
using TestCQRS.Application.Repository.Interface;
using TestCQRS.Domain.Models;
using TestCQRS.Infrastrucuture.Context;

namespace TestCQRS.Infrastructure.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationContext _context;

        public AnnouncementRepository(ApplicationContext context)
        {
            _context = context;
        }

        private DbSet<Announcement> DbSet => _context.Set<Announcement>();

        public async Task<IEnumerable<GetAnnouncementsResult>> GetEntities(string sortBy, string sortOrder)
        {
            var query = DbSet.AsQueryable();
            var isDescending = string.Equals(sortOrder, "desc", StringComparison.CurrentCultureIgnoreCase);

            query = ApplySorting(query, sortBy, isDescending);

            var announcements = await query.ToListAsync();

            // Преобразуйте `Announcement` в `GetAnnouncementsResult`
            return announcements.Select(a => new GetAnnouncementsResult
            {
                Id = a.Id,
                Number = a.Number,
                Text = a.Text,
                Rate = a.Rate,
                CreateDate = a.CreateDate,
                ExpiryDate = a.ExpiryDate
            });
        }

        private IQueryable<Announcement> ApplySorting(IQueryable<Announcement> query, string sortBy, bool isDescending)
        {
            return sortBy.ToLower() switch
            {
                "number" => isDescending ? query.OrderByDescending(a => a.Number) : query.OrderBy(a => a.Number),
                "userid" => isDescending ? query.OrderByDescending(a => a.UserId) : query.OrderBy(a => a.UserId),
                "text" => isDescending ? query.OrderByDescending(a => a.Text) : query.OrderBy(a => a.Text),
                "rate" => isDescending ? query.OrderByDescending(a => a.Rate) : query.OrderBy(a => a.Rate),
                "createdate" => isDescending ? query.OrderByDescending(a => a.CreateDate) : query.OrderBy(a => a.CreateDate),
                "expirydate" => isDescending ? query.OrderByDescending(a => a.ExpiryDate) : query.OrderBy(a => a.ExpiryDate),
                _ => query.OrderBy(a => a.CreateDate)
            };
        }

        public async Task<Announcement> GetEntity(Guid? id)
        {
            var announcement = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == id);

            if (announcement == null)
            {
                throw new InvalidOperationException();
            }

            return announcement;
        }

        public async Task<Guid> PostEntity(Announcement entity)
        {
            var announcement = await _context.Announcements.AddAsync(entity);
            await _context.SaveChangesAsync();

            return announcement.Entity.Id;
        }

        public async Task<Guid> PutEntity(Announcement entity)
        {
            var announcement = _context.Announcements.Update(entity);
            await _context.SaveChangesAsync();

            return announcement.Entity.Id;
        }

        public async Task DeleteEntity(Guid? id)
        {
            var announcement = await DbSet.FindAsync(id);
            if (announcement != null)
            {
                _context.Remove(announcement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Announcement>> SearchEntities(GetSearchAnnouncementResult searchDto)
        {
            var query = DbSet.AsQueryable();

            if (searchDto.Number.HasValue)
            {
                query = query.Where(a => a.Number == searchDto.Number.Value);
            }

            if (searchDto.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == searchDto.UserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Text))
            {
                query = query.Where(a => a.Text == searchDto.Text);
            }

            if (!string.IsNullOrWhiteSpace(searchDto.Rate))
            {
                query = query.Where(a => a.Rate.Contains(searchDto.Rate));
            }

            if (searchDto.CreateDate.HasValue)
            {
                query = query.Where(a => a.CreateDate == searchDto.CreateDate.Value.Date);
            }

            if (searchDto.ExpiryDate.HasValue)
            {
                query = query.Where(a => a.ExpiryDate == searchDto.ExpiryDate.Value.Date);
            }

            var announcements = await query.ToListAsync();
            return announcements.Adapt<IEnumerable<Announcement>>();
        }

        public async Task<int> GetUserAnnouncementsCount(Guid? userId)
        {
            return await DbSet.CountAsync(a => a.UserId == userId);
        }
    }
}
