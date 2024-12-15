using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.Activity;
using NoPressure.Common.Models.Tag;
using NoPressure.DAL.Entities;

namespace NoPressure.BLL.MappingProfiles
{
    public class TagProfile : Profile
    {
        public TagProfile() 
        {
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();
            CreateMap<Tag, TeamTag>();
        }
    }
}
