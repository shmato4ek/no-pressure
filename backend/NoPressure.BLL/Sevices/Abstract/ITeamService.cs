using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.Team;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface ITeamService
    {
        Task<TeamDTO> GetTeamById(int id);
        Task<List<TeamDTO>> GetTeams(int id);
        Task CreateTeam(NewTeam newTeam);
        Task AddUserToTeam(int teamId, int userId);
        Task RemoveUserFromTeam(int teamId, int userId);
        Task<TeamDTO> GetTeamByUniqId(string id);
        Task RemoveTeam(int teamId);
    }
}
