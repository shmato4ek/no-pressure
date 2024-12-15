using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl;

public class ScheduleGenerationConfigurationRepository : Repository<ScheduleGenerationConfiguration, int>, IScheduleGenerationConfigurationRepository
{
    public ScheduleGenerationConfigurationRepository(NoPressureDbContext context) : base(context) { }


    public async Task<ScheduleGenerationConfiguration> FindConfigurationByUserId(int userId)
    {
        return await _context.ScheduleGenerationConfigurations
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}