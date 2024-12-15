using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Team;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile() 
        {
            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();
            CreateMap<Team, TeamInfo>();
        }
    }
}
