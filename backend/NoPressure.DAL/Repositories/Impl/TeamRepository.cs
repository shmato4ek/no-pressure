using Microsoft.EntityFrameworkCore;
using NoPressure.Common.Enums;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;
using EntityState = NoPressure.Common.Enums.EntityState;

namespace NoPressure.DAL.Repositories.Impl
{
    public class TeamRepository : Repository<Team, int>, ITeamRepository
    {
        public TeamRepository(NoPressureDbContext context) : base(context) { }

        public async Task<List<Team>> GetUsersTeams(int userId)
        {
            var user = await _context
                .Users
                .FindAsync(userId);

            if (user is null)
            {
                throw new Exception($"There is no user with id {userId}");
            }

            var teams = await _context
                .Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Teams)
                .Include(t => t.Users)
                .Include(t => t.Tags)
                .Include(t => t.Settings)
                .Where(t => t.State == EntityState.Active)
                .ToListAsync();

            return teams;
        }

        public async Task<Team> GetTeamAsync(int id)
        {
            var team = await _context
                .Teams
                .Include(t => t.Tags)
                .Include(t => t.Users)
                .Where(t => t.State == EntityState.Active)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (team is null)
            {
                throw new Exception($"There is no team with id {id}");
            }

            return team;
        }

        public async Task<Team> GetTeamByUniqId(string id)
        {
            var team = await _context
                .Teams
                .Include(t => t.Users)
                .Include(t => t.Tags)
                .Include(t => t.Settings)
                .Where(t => t.State == EntityState.Active)
                .FirstOrDefaultAsync(t => t.UniqId == id);

            if (team is null)
            {
                throw new Exception($"Team with id {id} was not found");
            }

            return team;
        }

        public async Task RemoveTeam(int teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);

            if (team is null)
            {
                throw new Exception($"There is no team with id {teamId}");
            }

            team.State = EntityState.Deleted;

            _context.Teams.Update(team);
        }

        public async Task<Team> GetTeamWithSettings(int teamId)
        {
            var settings = await _context
                .Teams
                .Include(t => t.Settings)
                .FirstOrDefaultAsync(t => t.Id == teamId);
            
            return settings;
        }

        public async Task RemoveUserFromTeam(int teamId, int userId)
        {
            var user = await _context
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new Exception($"User with id {userId} was not found");
            }

            var team = await _context
                .Teams
                .Include(t => t.Users)
                .Include(t => t.Settings)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team is null)
            {
                throw new Exception($"Team with id {teamId} was not found");
            }

            if (team.Users.FirstOrDefault(u => u.Id == userId) is null)
            {
                throw new Exception($"There is no user with id {userId} in team {teamId}");
            }

            team.Users.Remove(user);

            var settings = team.Settings.FirstOrDefault(s => s.UserId == user.Id);

            team.Settings.Remove(settings);

            _context.Teams.Update(team);
        }
    }
}
