using AutoMapper;
using NoPressure.BLL.Exceptions;
using NoPressure.BLL.Helpers;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.Team;
using NoPressure.Common.Models.User;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.BLL.Sevices.Impl
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IActivityService _activityService;
        public TeamService (IUnitOfWork uow, IMapper mapper, IActivityService activityService)
        {
            _uow = uow;
            _mapper = mapper;
            _activityService = activityService;
        }

        public async Task AddUserToTeam(int teamId, int userId)
        {
            var userEntity = await _uow
                .UserRepository
                .FindAsync(userId);
            
            if (userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            var teamEntity = await _uow
                .TeamRepository
                .GetTeamAsync(teamId);

            if (teamEntity is null)
            {
                throw new NotFoundException("Team", teamId);
            }

            if(teamEntity.Users.FirstOrDefault(u => u.Id == userId) != null)
            {
                throw new UserInTeamException(userId, teamId);
            }

            teamEntity.Users.Add(userEntity);

            var settings = new TeamSettings()
            {
                UserId = userId,
                UserName = userEntity.Name,
                AddingActivities = TeamAccess.Deny,
                AddingUsers = TeamAccess.Deny
            };

            teamEntity.Settings.Add(settings);

            _uow.TeamRepository.Update(teamEntity);

            var notification = new Notification()
            {
                UserId = teamEntity.AuthorId,
                Title = NotificationTitle.NewTeamJoin,
                Date = DateTime.UtcNow,
                Data = new NotificationData()
                {
                    SecondUserName = userEntity.Name,
                    TeamName = teamEntity.Name,
                    Link = NotificationLinkHelper.GetNewTeamRequestLink(teamEntity.UniqId),
                }
            };

            _uow.NotificationRepository.Create(notification);

            await _uow.SaveAsync();
        }

        public async Task CreateTeam(NewTeam newTeam)
        {
            var userEntity = await _uow.UserRepository.FindAsync(newTeam.UserId);

            if(userEntity is null)
            {
                throw new NotFoundException("User", newTeam.UserId);
            }

            var teamEntity = new Team()
            {
                AuthorId = newTeam.UserId,
                Name = newTeam.Name,
                Tags = new List<Tag>(),
                Users = new List<User>(),
                Date = DateTime.UtcNow,
                State = EntityState.Active,
                Color = newTeam.Color,
                UniqId = CreateUniqId(),
                PrivacyState = TeamPrivacyState.Public,
                Settings = new List<TeamSettings>()
            };

            teamEntity.Users.Add(userEntity);

            var settings = new TeamSettings()
            {
                UserId = userEntity.Id,
                UserName = userEntity.Name,
                AddingUsers = TeamAccess.Allow,
                AddingActivities = TeamAccess.Allow
            };

            teamEntity.Settings.Add(settings);

            _uow.TeamRepository.Create(teamEntity);

            await _uow.SaveAsync();
        }

        private string CreateUniqId()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=","");
            GuidString = GuidString.Replace("+","");
            GuidString = GuidString.Replace("/","");

            return GuidString;
        }

        public async Task<TeamDTO> GetTeamById(int id)
        {
            var teamEntity = await _uow
                .TeamRepository
                .GetTeamAsync(id);

            return _mapper.Map<TeamDTO>(teamEntity);
        }

        public async Task<List<TeamDTO>> GetTeams(int id)
        {
            var teamsEntity = await _uow
                .TeamRepository
                .GetUsersTeams(id);

            var teams = _mapper.Map<List<TeamDTO>>(teamsEntity);

            foreach (var team in teams)
            {
                foreach (var tag in team.Tags)
                {
                    tag.ActivitiesAll = tag.Activities.Count;
                    tag.ActivitiesDone = tag.Activities.Where(a => a.State == ActivityState.Done).Count();
                }
            }

            return teams;
        }

        public async Task RemoveUserFromTeam(int teamId, int userId)
        {
            var teamEntity = await _uow.TeamRepository.FindAsync(teamId);

            if (teamEntity is null)
            {
                throw new NotFoundException("Team", teamId);
            }

            var userEntity = await _uow.UserRepository.FindAsync(userId);

            if (userEntity is null)
            {
                throw new NotFoundException("User", userId);
            }

            await _uow.TeamRepository.RemoveUserFromTeam(teamId, userId);
            await _uow.SaveAsync();
        }

        public async Task<TeamDTO> GetTeamByUniqId(string id, int userId)
        {
            var teamEntity = await _uow.TeamRepository.GetTeamByUniqId(id);

            if (teamEntity is null)
            {
                throw new NotFoundException("Team");
            }
            
            var team = new TeamDTO()
            {
                Id = teamEntity.Id,
                Name = teamEntity.Name,
                Date = teamEntity.Date,
                UniqId = teamEntity.UniqId,
                Color = teamEntity.Color,
                AuthorId = teamEntity.AuthorId,
            };
            
            if (teamEntity.AuthorId == userId)
            {
                team.Role = TeamRole.Owner;
                team.Users = _mapper.Map<List<UserInfo>>(teamEntity.Users);
                team.Tags = _mapper.Map<List<TeamTag>>(teamEntity.Tags);
                var userSettings = teamEntity.Settings.FirstOrDefault(user => user.UserId == userId);
                team.AddingUsers = userSettings.AddingUsers;
            }

            else if (teamEntity.Users.FirstOrDefault(user => user.Id == userId) != null)
            {
                team.Role = TeamRole.Member;
                team.Users = _mapper.Map<List<UserInfo>>(teamEntity.Users);
                team.Tags = _mapper.Map<List<TeamTag>>(teamEntity.Tags);
                
                var userSettings = teamEntity.Settings.FirstOrDefault(user => user.UserId == userId);
                team.AddingUsers = userSettings.AddingUsers;
            }

            else
            {
                team.Role = TeamRole.Visitor;
                if (teamEntity.PrivacyState == TeamPrivacyState.Public)
                {
                    team.Users = _mapper.Map<List<UserInfo>>(teamEntity.Users);
                    team.Tags = _mapper.Map<List<TeamTag>>(teamEntity.Tags);
                }
            }

            team.TeamRequestId = await _uow.TeamRequestRepository.CheckTeamRequest(teamEntity.Id, userId);

            return team;
        }

        public async Task RemoveTeam(int teamId, int userId)
        {
            var teamEntity = await _uow.TeamRepository.FindAsync(teamId);

            if (teamEntity is null)
            {
                throw new NotFoundException("Team", teamId);
            }

            if (userId != teamEntity.AuthorId)
            {
                throw new NoAccessException();
            }

            await _uow.TeamRepository.RemoveTeam(teamId);
            
            await _uow.SaveAsync();
        }

        public async Task<TeamWithSettingsDTO> GetSettings(int teamId)
        {
            var team = await _uow.TeamRepository.GetTeamWithSettings(teamId);

            if(team is null)
            {
                throw new NotFoundException("Team", teamId);

            }
            var teamWithSettings = new TeamWithSettingsDTO()
            {
                Id = team.Id,
                Color = team.Color,
                State = team.PrivacyState,
                Settings = new List<TeamSettingsDTO>()
            };

            foreach(var settings in team.Settings)
            {
                teamWithSettings.Settings.Add(_mapper.Map<TeamSettingsDTO>(settings));                
            }

            return teamWithSettings;
        }

        public async Task UpdateTeamSettings(UpdateTeamSettings settings)
        {
            var team = await _uow.TeamRepository.FindAsync(settings.TeamId);

            if(team is null)
            {
                throw new NotFoundException("Team", settings.TeamId);
            }

            team.PrivacyState = settings.State;
            team.Color = settings.Color;

            foreach(var setting in settings.Settings)
            {
                var userSettings = team.Settings.FirstOrDefault(s => s.Id == setting.Id);

                if(userSettings is null)
                {
                    throw new NotFoundException("Settings", setting.Id);
                }

                userSettings.AddingActivities = setting.AddingActivities;
                userSettings.AddingUsers = setting.AddingUsers;
            }

            _uow.TeamRepository.Update(team);

            await _uow.SaveAsync();
        }
    }
}
