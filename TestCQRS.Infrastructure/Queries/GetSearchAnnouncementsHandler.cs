using MediatR;
using TestCQRS.Application.Queries;
using TestCQRS.Application.Repository.Interface;

namespace TestCQRS.Infrastructure.Queries
{
    public class GetSearchAnnouncementsHandler : IRequestHandler<GetSearchAnnouncementQuery, IEnumerable<GetSearchAnnouncementResult>>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public GetSearchAnnouncementsHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<GetSearchAnnouncementResult>> Handle(GetSearchAnnouncementQuery query, CancellationToken cancellationToken)
        {
            var announcements = await _announcementRepository.SearchEntities(query.SearchParams);

            return announcements.Select(a => new GetSearchAnnouncementResult
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
