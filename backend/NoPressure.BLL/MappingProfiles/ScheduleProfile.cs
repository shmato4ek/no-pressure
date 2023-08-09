using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile() 
        {
            CreateMap<Schedule, ScheduleDTO>();
        }
    }
}
