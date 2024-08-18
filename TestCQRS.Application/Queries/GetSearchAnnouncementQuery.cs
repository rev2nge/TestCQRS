using MediatR;

namespace TestCQRS.Application.Queries
{
    public class GetSearchAnnouncementQuery : IRequest<IEnumerable<GetSearchAnnouncementResult>>
    {
        public GetSearchAnnouncementQuery(GetSearchAnnouncementResult searchParams)
        {
            SearchParams = searchParams;
        }

        public GetSearchAnnouncementResult SearchParams { get; }
    }
}