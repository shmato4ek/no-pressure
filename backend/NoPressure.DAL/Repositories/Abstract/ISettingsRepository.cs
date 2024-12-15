using NoPressure.DAL.Entities;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface ISettingsRepository : IRepository<Settings, int>
    {
        Task<Settings> FindSettingByUserId(int userId);
    }
}
