using NoPressure.DAL.Context;
using NoPressure.DAL.Entities;
using NoPressure.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Repositories.Impl
{
    public class ActivityRepository : Repository<Activity, int>, IActivityRepository
    {
       public ActivityRepository(NoPressureDbContext context) : base(context) { }
    }
}
