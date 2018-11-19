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
            CreateMap<Reservation, ReservationForReturnDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<ReservationForUpdateDto, Reservation>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Movie, MovieForListDto>();
            CreateMap<Show, ShowForListDto>();
            CreateMap<ReservationForCreationDto, Reservation>();
            CreateMap<Reservation, ReservationForReturnDto>();
        }
}
}
