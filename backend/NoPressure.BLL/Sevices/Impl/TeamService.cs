using AutoMapper;
using NoPressure.BLL.Sevices.Abstract;
using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.Team;
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
                throw new Exception($"User with id {userId} was not found");
            }

            var teamEntity = await _uow
                .TeamRepository
                .GetTeamAsync(teamId);

            if (teamEntity is null)
            {
                throw new Exception($"Team with id {teamId} was not found");
            }

            if(teamEntity.Users.FirstOrDefault(u => u.Id == userId) != null)
            {
                throw new Exception($"User with id {userId} is already in team {teamId}");
            }

            teamEntity.Users.Add(userEntity);

            _uow.TeamRepository.Update(teamEntity);

            await _uow.SaveAsync();
        }

        public async Task CreateTeam(NewTeam newTeam)
        {
            var userEntity = await _uow.UserRepository.FindAsync(newTeam.UserId);

            if(userEntity is null)
            {
                throw new Exception($"There is no user with id {newTeam.UserId}");
            }

            var teamEntity = new Team()
            {
                AuthorId = newTeam.UserId,
                Name = newTeam.Name,
                Tags = new List<Tag>(),
                Users = new List<User>(),
                Date = DateTime.UtcNow,
                State = EntityState.Active
            };

            teamEntity.UniqId = CreateUniqId();

            teamEntity.Users.Add(userEntity);

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
                    tag.ActivitiesAll = tag.Activities.Count();
                    tag.ActivitiesDone = tag.Activities.Where(a => a.State == ActivityState.Done).Count();
                }
            }

            return _mapper.Map<List<TeamDTO>>(teamsEntity);
        }

        public async Task RemoveUserFromTeam(int teamId, int userId)
        {
            var userEntity = await _uow
                .UserRepository
                .FindAsync(userId);
            
            if (userEntity is null)
            {
                throw new Exception($"User with id {userId} was not found");
            }

            var teamEntity = await _uow
                .TeamRepository
                .FindAsync(teamId);

            if (teamEntity is null)
            {
                throw new Exception($"Team with id {teamId} was not found");
            }

            if (teamEntity.Users.FirstOrDefault(u => u.Id == userId) is null)
            {
                throw new Exception($"There is no user with id {userId} in team {teamId}");
            }

            teamEntity.Users.Remove(userEntity);

            _uow.TeamRepository.Update(teamEntity);

            await _uow.SaveAsync();
        }

        public async Task<TeamDTO> GetTeamByUniqId(string id)
        {
            var teamEntity = await _uow.TeamRepository.GetTeamByUniqId(id);

            return _mapper.Map<TeamDTO>(teamEntity);
        }

        public async Task RemoveTeam(int teamId)
        {
            await _uow.TeamRepository.RemoveTeam(teamId);
            
            await _uow.SaveAsync();
        }
    }
}
