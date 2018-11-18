using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IBiografRepository _repo;
        private readonly IShowRepository _showRepo;
        private readonly IMapper _mapper;
        public ReservationsController (IBiografRepository repo, IMapper mapper, IShowRepository showRepo) {
            this._mapper = mapper;
            this._repo = repo;
            this._showRepo = showRepo;
        }

        // [HttpGet("paidCount/{id}")]
        [HttpGet("all/{id}")]
        public async Task<IActionResult> GetReservations(int id) {
            var reservations = await _repo.GetReservations(id);

            return Ok(reservations);
        }

        [HttpGet("paidCount/{id}")]
        public async Task<IActionResult> GetPaidReservationsCount(int id) {
            var reservations = await _repo.GetPaidReservationsCount(id);

            return Ok(reservations);
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public async Task<IActionResult> GetReservation(int id) {
            var reservation = await _repo.GetReservation(id);

            return Ok(reservation);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreateReservation(int id, ReservationForCreationDto reservationForCreationDto) {
            var user = await _repo.GetUser(id);
            
            if (user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

        

            // TODO: skal laves helt om...
            var movie = await _repo.GetMovie(reservationForCreationDto.Show.Movie.Id);
            reservationForCreationDto.Show.Movie = movie;
            var show = await _showRepo.GetShow(reservationForCreationDto.Show.Id);
            reservationForCreationDto.Show = show;


            var reservation = _mapper.Map<Reservation>(reservationForCreationDto);

         


            _repo.Add(reservation); 


            if (await _repo.SaveAll()) {
                var reservationToReturn = _mapper.Map<ReservationForReturnDto>(reservation);
                return CreatedAtRoute("GetReservation", new {id = reservation.Id}, reservationToReturn);
            }

            throw new Exception("Kunne ikke oprette reservationen");
        }

    }
}