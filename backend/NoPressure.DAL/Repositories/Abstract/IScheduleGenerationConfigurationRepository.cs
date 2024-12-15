using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract;

public interface IScheduleGenerationConfigurationRepository : IRepository<ScheduleGenerationConfiguration, int>
{
    Task<ScheduleGenerationConfiguration> FindConfigurationByUserId(int userId);
}