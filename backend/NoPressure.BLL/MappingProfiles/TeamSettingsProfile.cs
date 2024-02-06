using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class TeamSettingsProfile : Profile
    {
        public TeamSettingsProfile() 
        {
            CreateMap<TeamSettings, TeamSettingsDTO>();
            CreateMap<TeamSettingsDTO, TeamSettings>();
        }
    }
}
