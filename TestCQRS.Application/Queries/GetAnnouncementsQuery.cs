using MediatR;

namespace TestCQRS.Application.Queries
{
    public class GetAnnouncementsQuery : IRequest<IEnumerable<GetAnnouncementsResult>>
    {
        public GetAnnouncementsQuery(string sortBy = "Date", string sortOrder = "asc") 
        {
            SortBy = sortBy;
            SortOrder = sortOrder;
        }

        public string SortBy { get; set; } = "Date";
        public string SortOrder { get; set; } = "asc";
    }
}
