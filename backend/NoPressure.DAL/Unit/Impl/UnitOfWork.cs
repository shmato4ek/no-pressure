using NoPressure.DAL.Context;
using NoPressure.DAL.Repositories.Abstract;
using NoPressure.DAL.Unit.Abstract;

namespace NoPressure.DAL.Unit.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        protected NoPressureDbContext _context;
        private bool _isDisposed = false;

        public IUserRepository UserRepository { get; }
        public IActivityRepository ActivityRepository { get; }
        public IPlanRepository PlanRepository { get; }
        public ITagRepository TagRepository { get; }

        public UnitOfWork(NoPressureDbContext context, 
                            IUserRepository userRepo,
                            IActivityRepository activityRepo,
                            IPlanRepository planRepo,
                            ITagRepository tagRepo)
        {
            UserRepository = userRepo;
            ActivityRepository = activityRepo;
            PlanRepository = planRepo;
            TagRepository = tagRepo;
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
