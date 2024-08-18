using MediatR;

namespace TestCQRS.Application.Queries
{
    public class GetAnnouncementByIdQuery : IRequest<GetAnnouncementByIdResult>
    {
        public GetAnnouncementByIdQuery(Guid id) 
        { 
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
