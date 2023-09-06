using NoPressure.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Unit.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IActivityRepository ActivityRepository { get; }
        IPlanRepository PlanRepository { get; }
        ITagRepository TagRepository { get; }

        Task SaveAsync();
        void Save();
    }
}
