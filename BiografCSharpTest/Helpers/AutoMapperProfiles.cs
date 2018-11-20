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
            CreateMap<Movie, MovieForReturnDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForUpdateByAdminDto, User>();
            CreateMap<DiscountForUpdateByAdminDto, Discount>();
            CreateMap<ReservationForUpdateDto, Reservation>();
            CreateMap<MovieForCreationDto, Movie>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Movie, MovieForListDto>();
            CreateMap<Show, ShowForListDto>();
            CreateMap<ReservationForCreationDto, Reservation>();
            CreateMap<Reservation, ReservationForReturnDto>();
        }
}
}
