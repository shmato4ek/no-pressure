using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles;

public class ScheduleGenerationConfigurationProfile : Profile
{
    public ScheduleGenerationConfigurationProfile() 
    {
        CreateMap<ScheduleGenerationConfiguration, ScheduleGenerationConfigurationDTO>();
        CreateMap<ScheduleGenerationConfigurationDTO, ScheduleGenerationConfiguration>();
    }
}