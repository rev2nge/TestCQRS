using MediatR;
using TestCQRS.Application.Queries;
using TestCQRS.Application.Repository.Interface;

namespace TestCQRS.Infrastructure.Queries
{
    public class GetAnnouncementByIdHandler : IRequestHandler<GetAnnouncementByIdQuery, GetAnnouncementByIdResult>
    {
        private readonly IAnnouncementRepository _anouncementRepository;

        public GetAnnouncementByIdHandler(IAnnouncementRepository announcementRepository)
        {
            _anouncementRepository = announcementRepository;
        }

        public async Task<GetAnnouncementByIdResult> Handle(GetAnnouncementByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _anouncementRepository.GetEntity(query.Id);

            return new GetAnnouncementByIdResult
            {
                Id = query.Id,
            };
        }
    }
}
