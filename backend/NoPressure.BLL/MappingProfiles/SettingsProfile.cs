using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class SettingsProfile : Profile
    {
        public SettingsProfile() 
        {
            CreateMap<Settings, SettingsDTO>();
            CreateMap<SettingsDTO, Settings>();
        }
    }
}
