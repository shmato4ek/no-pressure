using NoPressure.Common.Enums;
using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ITeamRepository : IRepository<Team, int>
    {
        Task<List<Team>> GetUsersTeams(int userId);
        Task<Team> GetTeamAsync(int id);
        Task<Team> GetTeamByUniqId(string id);
        Task RemoveTeam(int teamId);
    }
}
