using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IBiografRepository _repo;
        private readonly IMapper _mapper;

        public DiscountsController(IBiografRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDiscounts() {
            var discounts = await _repo.GetDiscounts();

            return Ok(discounts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount (int id, DiscountForUpdateByAdminDto discountForUpdateByAdminDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _repo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var discountFromRepo = await _repo.GetDiscount(id);

            

            _mapper.Map(discountForUpdateByAdminDto, discountFromRepo);

            if (await _repo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af rabat {id} fejlede");
        }
    }
}