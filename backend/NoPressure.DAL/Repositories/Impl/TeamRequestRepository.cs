using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NoPressure.Common.Enums;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class TeamRequestRepository : Repository<TeamRequest, int>, ITeamRequestRepository
    {
        public TeamRequestRepository(NoPressureDbContext context) : base(context) { }

        public async Task BulkInsert(List<TeamRequest> requests)
        {
            await _context.BulkInsertAsync(requests);
        }

        public async Task ChangeTeamRequestStatus(int id, TeamRequestStatus status)
        {
            var request = await _context
                .TeamRequests
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request is null)
            {
                throw new Exception($"There is no request with id {id}");
            }

            request.Status = status;

            _context.TeamRequests.Update(request);

            await _context.SaveChangesAsync();
        }

        public async Task<int> CheckTeamRequest(int teamId, int userId)
        {
            var request = await _context
                .TeamRequests
                .Where(request => request.Status == TeamRequestStatus.InPending)
                .Where(request => request.InvitedUserId == userId)
                .FirstOrDefaultAsync(request => request.TeamId == teamId);

            if (request is null)
            {
                return 0;
            }
            
            else
            {
                return request.Id;
            }
        }
    }
}
