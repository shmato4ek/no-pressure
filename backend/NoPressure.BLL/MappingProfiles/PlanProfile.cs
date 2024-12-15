using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class PlanProfile : Profile
    {
        public PlanProfile() 
        {
            CreateMap<Plan, PlanDTO>();
            CreateMap<PlanDTO, Plan>();
        }
    }
}
