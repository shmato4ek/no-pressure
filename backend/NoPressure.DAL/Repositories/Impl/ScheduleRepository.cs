using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public class ScheduleRepository : Repository<Schedule, DateTime>, IScheduleRepository
    {
        public ScheduleRepository(NoPressureDbContext context) : base(context) { }
    }
}
