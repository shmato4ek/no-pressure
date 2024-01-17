using Microsoft.EntityFrameworkCore;
using NoPressure.Common.Enums;
using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class SettingsRepository : Repository<Settings, int>, ISettingsRepository
    {
        public SettingsRepository(NoPressureDbContext context) : base(context) { }

        public async Task<Settings> FindSettingByUserId(int userId)
        {
            var settings = await _context
                .Settings
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (settings is null)
            {
                throw new Exception();
            }

            return settings;
        }
    }
}
