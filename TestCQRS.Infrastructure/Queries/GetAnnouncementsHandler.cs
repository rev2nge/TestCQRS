using MediatR;
using TestCQRS.Application.Queries;
using TestCQRS.Application.Repository.Interface;

namespace TestCQRS.Infrastructure.Queries
{
    public class GetAnnouncementsHandler : IRequestHandler<GetAnnouncementsQuery, IEnumerable<GetAnnouncementsResult>>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public GetAnnouncementsHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<GetAnnouncementsResult>> Handle(GetAnnouncementsQuery query, CancellationToken cancellationToken)
        {
            var announcements = await _announcementRepository.GetEntities(query.SortBy, query.SortOrder);

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
    }
}
