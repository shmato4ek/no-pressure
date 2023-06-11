using NoPressure.DAL.Context;
using NoPressure.DAL.Repositories.Abstract;
using NoPressure.DAL.Unit.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Unit.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        protected NoPressureDbContext _context;
        private bool _isDisposed = false;

        public IUserRepository UserRepository { get; }
        public IActivityRepository ActivityRepository { get; }

        public UnitOfWork(NoPressureDbContext context, IUserRepository userRepo, IActivityRepository activityRepo)
        {
            UserRepository = userRepo;
            ActivityRepository = activityRepo;
            _context = context;
        }

        public virtual void Dispose(bool disposing) 
        {
            if(!_isDisposed)
            {
                if(disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }

        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
