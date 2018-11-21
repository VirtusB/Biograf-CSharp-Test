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
            #region Users
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForUpdateByAdminDto, User>();
            CreateMap<UserForRegisterDto, User>();
            #endregion
            
            #region Discounts
            CreateMap<DiscountForCreationDto, Discount>();
            CreateMap<DiscountForUpdateByAdminDto, Discount>();
            CreateMap<Discount, DiscountForReturnDto>();
            #endregion

            #region Movies
            CreateMap<MovieForCreationDto, Movie>();
            CreateMap<Movie, MovieForListDto>();
            CreateMap<Movie, MovieForReturnDto>();
            CreateMap<MovieForUpdateByAdminDto, Movie>();
            #endregion

            #region Shows
            CreateMap<Show, ShowForListDto>();
            CreateMap<ShowForCreationDto, Show>();
            CreateMap<ShowForUpdateByAdminDto, Show>();
            CreateMap<Show, ShowForReturnDto>();
            #endregion

            #region Reservations
            CreateMap<ReservationForUpdateDto, Reservation>();
            CreateMap<ReservationForCreationDto, Reservation>();
            CreateMap<Reservation, ReservationForReturnDto>();
            CreateMap<Reservation, ReservationForReturnDto>();
            #endregion
        }
}
}
