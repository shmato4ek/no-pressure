using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity, TIdentity>
    {
        void Create(TEntity item);
        void Update(TEntity item);
        void Remove(TIdentity id);
        IEnumerable<TEntity> FindAll();
        TEntity Find(TIdentity id);
        Task<TEntity> FindAsync(TIdentity id);
    }
}
