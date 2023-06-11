using Microsoft.EntityFrameworkCore;
using NoPressure.DAL.Context;
using NoPressure.DAL.Repositories.Abstract;

namespace NoPressure.DAL.Repositories.Impl
{
    public abstract class Repository<TEntity, TIdentity> : IRepository<TEntity, TIdentity> where TEntity : class
    {
        protected NoPressureDbContext _context;

        public Repository(NoPressureDbContext context) 
        {
            _context = context;
        }

        public void Create(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
        }

        public virtual TEntity Find(TIdentity id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> FindAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity item)
        {
            _context.Set<TEntity>().Remove(item);
        }

        public void Update(TEntity item) 
        {
            _context.Set<TEntity>().Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        } 
    }
}
