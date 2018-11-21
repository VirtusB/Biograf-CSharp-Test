using System;
using System.Collections.Generic;
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
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _repo;
        private readonly IBiografRepository _bioRepo;
        private readonly IMapper _mapper;

        public DiscountsController(IDiscountRepository repo, IMapper mapper, IBiografRepository bioRepo)
        {
            this._mapper = mapper;
            this._repo = repo;
            this._bioRepo = bioRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDiscounts() {
            var discounts = await _repo.GetDiscounts();

            return Ok(discounts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _bioRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var discountFromRepo = await _repo.GetDiscount(id);

            _repo.Delete(discountFromRepo);

            if (await _repo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette rabat med ID {id}");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount (int id, DiscountForUpdateByAdminDto discountForUpdateByAdminDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _bioRepo.GetUser(userMakingRequestId);

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

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(DiscountForCreationDto discountForCreationDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _bioRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }


            var discount = _mapper.Map<Discount>(discountForCreationDto);

            _repo.Add(discount); 

            if (await _repo.SaveAll()) {
                var discountToReturn = _mapper.Map<DiscountForReturnDto>(discount);
                return CreatedAtRoute("GetDiscount", new {id = discount.Id}, discountToReturn);
            }

            throw new Exception("Kunne ikke oprette rabat trinet");
        } 

        [HttpGet("{id}", Name = "GetDiscount")]
        public async Task<IActionResult> GetDiscount(int id) {
            var discount = await _repo.GetDiscount(id);

            return Ok(discount);
        }
    }
}