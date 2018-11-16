using System.Linq;
using AutoMapper;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Movie, MovieForListDto>();
        }
}
}
