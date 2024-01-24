using NoPressure.Common.Enums;
using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ITeamRequestRepository : IRepository<TeamRequest, int>
    {
        Task ChangeTeamRequestStatus(int id, TeamRequestStatus status);
    }
}
