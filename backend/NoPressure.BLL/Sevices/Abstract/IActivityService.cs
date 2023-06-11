using NoPressure.Common.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.Sevices.Abstract
{
    public interface IActivityService
    {
        Task CreateActivity(NewActivity newActivity);
    }
}
