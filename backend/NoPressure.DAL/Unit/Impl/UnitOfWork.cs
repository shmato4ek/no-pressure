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

        public IUserRepository UserRepository { get; }

        private bool _isDisposed = false;
        public UnitOfWork(NoPressureDbContext context, IUserRepository userRepo)
        {
            UserRepository = userRepo;
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
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
