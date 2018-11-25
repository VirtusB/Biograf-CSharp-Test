using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Models;
using BiografCSharpTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IShowRepository _showRepo;
        private readonly IDiscountRepository _discountRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly IReservationRepository _reservationRepo;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ReservationsController (
            IUserRepository userRepo, 
            IMapper mapper, 
            IShowRepository showRepo, 
            IMovieRepository movieRepo, 
            IDiscountRepository discountRepo, 
            IReservationRepository reservationRepo,
            IUserService userService
            ) {
            this._mapper = mapper;
            this._userRepo = userRepo;
            this._showRepo = showRepo;
            this._movieRepo = movieRepo;
            this._discountRepo = discountRepo;
            this._reservationRepo = reservationRepo;
            this._userService = userService;
        }

        
        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetReservations(int id) {
            var reservations = await _reservationRepo.GetReservations(id);

            return Ok(reservations);
        }

        [HttpGet("discountstep/{id}")]
        public async Task<IActionResult> GetDiscountStep(int id) {
            var discountStep = await _discountRepo.GetCurrentDiscountStep(id);

            var nextStep = await _discountRepo.GetNextDiscountStep(id);

            var paidCount =  await _reservationRepo.GetPaidReservationsCount(id);
            
            var highestRequiredBookings = await _discountRepo.GetHighestRequiredBookings();

            var all = new {
                discountStep,
                nextStep,
                paidCount,
                highestRequiredBookings
            };

            return Ok(all);

        }

        [HttpGet("paidCount/{id}")]
        public async Task<IActionResult> GetPaidReservationsCount(int id) {
            var reservations = await _reservationRepo.GetPaidReservationsCount(id);

            return Ok(reservations);
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public async Task<IActionResult> GetReservation(int id) {
            var reservation = await _reservationRepo.GetReservation(id);
            var reservationToReturn = _mapper.Map<ReservationForReturnDto>(reservation);

            return Ok(reservationToReturn);
        }

        
        [HttpPost("{userId}/{id}")]
        public async Task<IActionResult> SetBookingState(int userId, int id, [FromBody] ReservationForUpdateDto reservationForUpdateDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            List<string> allowedRoles = new List<string>(new string[] 
            { 
                "Personale",
                "Admin" 
            });

            

            if (!allowedRoles.Contains(userMakingRequest.Role.Name)) {
                return Unauthorized();
            }

              var reservation = await _reservationRepo.GetReservation(id);

             _mapper.Map(reservationForUpdateDto, reservation);

            


            if (await _reservationRepo.SaveAll()) {
                var reservationToReturn = _mapper.Map<ReservationForReturnDto>(reservation);
                return Ok(reservationToReturn);
            }

            return BadRequest("Kunne ikke sætte statussen");
        }

        // før, opret en reservation af gangen
        // [HttpPost("{id}")]
        // public async Task<IActionResult> CreateReservation(int id, ReservationForCreationDto reservationForCreationDto) {
        //     var user = await _userRepo.GetUser(id);
            
            
        //     if (user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
        //         return Unauthorized();
        //     }

        

        //     // TODO: skal laves helt om...
        //     var movie = await _movieRepo.GetMovie(reservationForCreationDto.Show.Movie.Id);
        //     reservationForCreationDto.Show.Movie = movie;
        //     var show = await _showRepo.GetShow(reservationForCreationDto.Show.Id);
        //     reservationForCreationDto.Show = show;


        //     var reservation = _mapper.Map<Reservation>(reservationForCreationDto);

         


        //     _reservationRepo.Add(reservation); 


        //     if (await _reservationRepo.SaveAll()) {
                
                

        //         var reservationToReturn = _mapper.Map<ReservationForReturnDto>(reservation);
        //         return CreatedAtRoute("GetReservation", new {id = reservation.Id}, reservationToReturn);
        //     }

        //     throw new Exception("Kunne ikke oprette reservationen");
        // }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateReservations(int id, [FromBody] List<ReservationForCreationDto> reservationForCreationDto) {
            var user = await _userRepo.GetUser(id);
            
            
            if (user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

            var movie = await _movieRepo.GetMovie(reservationForCreationDto.First().Show.Movie.Id);
            var show = await _showRepo.GetShow(reservationForCreationDto.First().Show.Id);

            var reservations = new List<ReservationForCreationDto>();

            reservationForCreationDto.ForEach(res => {
                res.Show.Movie = movie;
                res.Show = show;
                reservations.Add(res);
            });


            List<Reservation> reservationsToAdd = new List<Reservation>();

            reservations.ForEach( res =>  {
                var reservation = _mapper.Map<Reservation>(res);
                reservationsToAdd.Add(reservation);
            });

         

            reservationsToAdd.ForEach(n => _reservationRepo.Add(n));


            if (await _reservationRepo.SaveAll()) {
                List<ReservationForReturnDto> reservationsForReturn = new List<ReservationForReturnDto>();

                reservationsToAdd.ForEach(res => {
                    var r = _mapper.Map<ReservationForReturnDto>(res);
                    reservationsForReturn.Add(r);
                });

                // hvis kunden har valgt at købe billetter
                if (reservationsToAdd.First().BookingState == 2) {
                    await _userService.UpdateLifetimeAmountSaved(user, reservationsToAdd.First().Show.TicketPrice, reservationsToAdd.Count);
                }
            
                return Ok(reservationsForReturn);
            }

            throw new Exception("Kunne ikke oprette reservationerne");
        }

    }
}