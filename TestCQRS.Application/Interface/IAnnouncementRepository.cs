using TestCQRS.Application.Queries;
using TestCQRS.Domain.Models;

namespace TestCQRS.Application.Repository.Interface
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<GetAnnouncementsResult>> GetEntities(string sortBy, string sortOrder);
        Task<Announcement> GetEntity(Guid? id);
        Task<Guid> PostEntity(Announcement entity);
        Task<Guid> PutEntity(Announcement entity);
        Task DeleteEntity(Guid? id);
        Task<IEnumerable<Announcement>> SearchEntities(GetSearchAnnouncementResult entity);
        Task<int> GetUserAnnouncementsCount(Guid? userId);
    }
}
