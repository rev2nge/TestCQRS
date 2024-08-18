using Mapster;
using MediatR;
using TestCQRS.Application.Commands;
using TestCQRS.Application.Repository.Interface;
using TestCQRS.Domain.Models;

namespace TestCQRS.Infrastructure.Commands
{
    public class CreateAnnouncementHandler : IRequestHandler<CreateAnnouncementCommand, Guid>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public CreateAnnouncementHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Guid> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = request.Adapt<Announcement>();
            return await _announcementRepository.PostEntity(announcement);
        }
    }
}