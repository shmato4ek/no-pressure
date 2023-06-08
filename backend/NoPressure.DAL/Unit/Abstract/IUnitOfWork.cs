using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.DAL.Unit.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
