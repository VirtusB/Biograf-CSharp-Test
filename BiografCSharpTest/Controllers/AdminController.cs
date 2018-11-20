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
        private readonly IBiografRepository _repo;
        private readonly IMapper _mapper;

        public AdminController(IBiografRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UserForUpdateByAdminDto userForUpdateByAdminDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _repo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);

            var role = await _repo.GetRole(userForUpdateByAdminDto.Role.Id);
            userForUpdateByAdminDto.Role = role;

            _mapper.Map(userForUpdateByAdminDto, userFromRepo);

            if (await _repo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af bruger {id} fejlede");
        }
    }
}