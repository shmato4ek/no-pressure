using AutoMapper;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.Helpers;
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

        public async Task AddUsersToTeam(AddUsersToTeam users, int userId)
        {
            var teamEntity = await _uow
                .TeamRepository
                .FindAsync(users.TeamId);

            if (teamEntity is null)
            {
                throw new NotFoundException("Team");
            }

            var userEntity = await _uow
                .UserRepository
                .FindAsync(userId);

            if (userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            var newUsers = new List<TeamRequest>();
            var newNotifications = new List<Notification>();

            foreach(var email in users.Users)
            {
                var user = await _uow.UserRepository.FindUserByEmail(email);
                var addUserId = user.Id;
                
                var requestEntity = new TeamRequest() {
                    AuthorId = userId,
                    InvitedUserId = addUserId,
                    TeamId = users.TeamId,
                    
                    Status = TeamRequestStatus.InPending,
                    Date = DateTime.UtcNow
                };

                newUsers.Add(requestEntity);

                var newNotification = new Notification()
                {
                    UserId = addUserId,
                    Title = NotificationTitle.NewTeamInvitation,
                    Data = new NotificationData()
                    {
                        SecondUserName = userEntity.Name,
                        TeamName = teamEntity.Name,
                        Link = NotificationLinkHelper.GetNewTeamRequestLink(teamEntity.UniqId),
                    },
                    Date = DateTime.UtcNow
                };

                newNotifications.Add(newNotification);
            }

            await _uow.TeamRequestRepository.BulkInsert(newUsers);
            await _uow.NotificationRepository.BulkInsert(newNotifications);

            await _uow.SaveAsync();
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
                await _teamService.AddUserToTeam(requestEntity.TeamId, requestEntity.InvitedUserId);
            }

            requestEntity.Status = status;

            _uow.TeamRequestRepository.Update(requestEntity);
            
            await _uow.SaveAsync();
        }

        public async Task CreateTeamRequest(int teamId, int invitedUserId, int authorId)
        {
            var requestEntity = new TeamRequest() {
                AuthorId = authorId,
                InvitedUserId = invitedUserId,
                TeamId = teamId,
                
                Status = TeamRequestStatus.InPending,
                Date = DateTime.UtcNow
            };

            _uow.TeamRequestRepository.Create(requestEntity);

            await _uow.SaveAsync();
        }
    }
}
