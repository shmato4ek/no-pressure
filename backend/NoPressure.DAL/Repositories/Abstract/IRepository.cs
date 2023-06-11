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
        void Remove(TEntity item);
        IEnumerable<TEntity> FindAll();
        TEntity Find(TIdentity id);
    }
}
