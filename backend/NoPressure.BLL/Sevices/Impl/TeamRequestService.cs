using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Team;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class TeamRequestService : ITeamRequestService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;
        public TeamRequestService (IUnitOfWork uow, IMapper mapper, ITeamService teamService)
        {
            _uow = uow;
            _mapper = mapper;
            _teamService = teamService;
        }

        public async Task ChangeRequestStatus(int id, TeamRequestStatus status)
        {
            var requestEntity = await _uow
                .TeamRequestRepository
                .FindAsync(id);

            if (requestEntity.Status == status || requestEntity.Status != TeamRequestStatus.InPending)
            {
                return;
            }

            if(status == TeamRequestStatus.Accepted)
            {
                await _teamService.AddUserToTeam(requestEntity.Id, requestEntity.InvitedUserId);
            }

            requestEntity.Status = status;

            _uow.TeamRequestRepository.Update(requestEntity);
            
            await _uow.SaveAsync();
        }

        public async Task CreateTeamRequest(NewTeamRequest request, int authorId)
        {
            var requestEntity = new TeamRequest() {
                AuthorId = authorId,
                InvitedUserId = request.InvitedUserId,
                
                Status = TeamRequestStatus.InPending,
                Date = DateTime.UtcNow
            };

            _uow.TeamRequestRepository.Create(requestEntity);

            await _uow.SaveAsync();
        }
    }
}
