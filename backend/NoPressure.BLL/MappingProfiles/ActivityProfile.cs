using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile() 
        {
            CreateMap<NewActivity, Activity>();
            CreateMap<Activity, ActivityDTO>().ForMember(o => o.Color, opt => opt.MapFrom(src => src.Tag.Color));
            CreateMap<ActivityDTO, Activity>();
        }
    }
}
