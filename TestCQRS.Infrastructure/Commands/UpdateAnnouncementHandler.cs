using Mapster;
using MediatR;
using TestCQRS.Application.Commands;
using TestCQRS.Application.Repository.Interface;
using TestCQRS.Domain.Models;

namespace TestCQRS.Infrastructure.Commands
{
    public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand, Guid>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public UpdateAnnouncementHandler(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Guid> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcement = request.Adapt<Announcement>();
            return await _announcementRepository.PutEntity(announcement);
        }
    }
}