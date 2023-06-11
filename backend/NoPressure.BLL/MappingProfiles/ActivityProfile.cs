using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.MappingProfiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile() 
        {
            CreateMap<Activity, ActivityDTO>();
            CreateMap<ActivityDTO, Activity>();
        }
    }
}
