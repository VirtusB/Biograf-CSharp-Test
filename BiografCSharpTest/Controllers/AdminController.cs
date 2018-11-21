using System;
using System.Collections.Generic;
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
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AdminController(IUserRepository userRepo, IMapper mapper)
        {
            this._mapper = mapper;
            this._userRepo = userRepo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UserForUpdateByAdminDto userForUpdateByAdminDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var userFromRepo = await _userRepo.GetUser(id);

            var role = await _userRepo.GetRole(userForUpdateByAdminDto.Role.Id);
            userForUpdateByAdminDto.Role = role;

            _mapper.Map(userForUpdateByAdminDto, userFromRepo);

            if (await _userRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af bruger {id} fejlede");
        }
    }
}