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
        private readonly IDiscountRepository _discountRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public DiscountsController(IDiscountRepository discountRepo, IMapper mapper, IUserRepository userRepo)
        {
            this._mapper = mapper;
            this._discountRepo = discountRepo;
            this._userRepo = userRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDiscounts() {
            var discounts = await _discountRepo.GetDiscounts();

            return Ok(discounts);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var discountFromRepo = await _discountRepo.GetDiscount(id);

            _discountRepo.Delete(discountFromRepo);

            if (await _discountRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette rabat med ID {id}");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscount (int id, DiscountForUpdateByAdminDto discountForUpdateByAdminDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var discountFromRepo = await _discountRepo.GetDiscount(id);

            

            _mapper.Map(discountForUpdateByAdminDto, discountFromRepo);

            if (await _discountRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af rabat {id} fejlede");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(DiscountForCreationDto discountForCreationDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }


            var discount = _mapper.Map<Discount>(discountForCreationDto);

            _discountRepo.Add(discount); 

            if (await _discountRepo.SaveAll()) {
                var discountToReturn = _mapper.Map<DiscountForReturnDto>(discount);
                return CreatedAtRoute("GetDiscount", new {id = discount.Id}, discountToReturn);
            }

            throw new Exception("Kunne ikke oprette rabat trinet");
        } 

        [HttpGet("{id}", Name = "GetDiscount")]
        public async Task<IActionResult> GetDiscount(int id) {
            var discount = await _discountRepo.GetDiscount(id);

            return Ok(discount);
        }
    }
}