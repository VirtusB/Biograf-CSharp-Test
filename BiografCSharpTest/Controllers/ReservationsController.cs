using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
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
        private readonly IMapper _mapper;
        public ReservationsController (IBiografRepository repo, IMapper mapper) {
            this._mapper = mapper;
            this._repo = repo;
        }

        // [HttpGet("paidCount/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservations(int id) {
            var reservations = await _repo.GetReservations(id);

            return Ok(reservations);
        }

        [HttpGet("paidCount/{id}")]
        public async Task<IActionResult> GetPaidReservationsCount(int id) {
            var reservations = await _repo.GetPaidReservationsCount(id);

            return Ok(reservations);
        }

    }
}