using AutoMapper;
using NoPressure.Common.DTO;
using NoPressure.Common.Models.User;
using NoPressure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPressure.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<NewUser, User>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UserInfo>();
        }
    }
}
