using NoPressure.Common.DTO;
using NoPressure.Common.Enums;
using NoPressure.Common.Models.Requests;
using NoPressure.Common.Models.Schedule;
using NoPressure.Common.Models.Tag;
using NoPressure.Common.Models.Team;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface ITeamRequestService
    {
        Task ChangeRequestStatus(int id, TeamRequestStatus status);
        Task CreateTeamRequest(int teamId, int invitedUserId, int authorId);
        Task AddUsersToTeam(AddUsersToTeam users, int userId);
    }
}
