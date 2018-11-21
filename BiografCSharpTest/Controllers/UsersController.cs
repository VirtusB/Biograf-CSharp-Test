using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Dtos;

namespace BiografCSharpTest.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController (IUserRepository userRepo, IMapper mapper) {
            this._mapper = mapper;
            this._userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers ([FromQuery]UserParams userParams) {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _userRepo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            var users = await _userRepo.GetUsers(userParams);

            var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);

            Response.AddPagination(users.CurrentPage, userParams.PageSize, users.TotalCount, users.TotalPages);

            return Ok (usersToReturn);
        }

        [HttpGet ("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _userRepo.GetUser (id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok (userToReturn);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles() {
            var roles = await _userRepo.GetRoles();

            return Ok(roles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, UserForUpdateDto userForUpdateDto) {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

            var userFromRepo = await _userRepo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _userRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af bruger {id} fejlede");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserByAdmin (int id, UserForUpdateByAdminDto userForUpdateByAdminDto) {
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserByAdmin(int id) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _userRepo.GetUser(userMakingRequestId);

            if (userMakingRequest.Role.Name != "Admin") {
                return Unauthorized();
            }

            //TODO: slet bruger uden først at lave et DB kald
            var userFromRepo = await _userRepo.GetUser(id);

            _userRepo.Delete(userFromRepo);

            if (await _userRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette bruger med ID {id}");
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (id != userMakingRequestId) {
                return Unauthorized();
            }

            //TODO: slet bruger uden først at lave et DB kald
            var userFromRepo = await _userRepo.GetUser(id);

            _userRepo.Delete(userFromRepo);

            if (await _userRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette bruger med ID {id}");
        }
    }
}