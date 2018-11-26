using AutoMapper;
using BiografCSharpTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Models;
using System;

namespace BiografCSharpTest.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public AuthController (IAuthRepository authRepo, IConfiguration config, IMapper mapper, IUserRepository userRepo) {
            this._mapper = mapper;
            this._config = config;
            this._authRepo = authRepo;
            this._userRepo = userRepo;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register (UserForRegisterDto userForRegisterDto) {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower ();

            if (await _authRepo.UserExists (userForRegisterDto.Username)) {
                return BadRequest ("Brugernavn eksisterer allerede");
            }

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            Role standardRole = await _userRepo.GetRoleByName("Standard");
            userToCreate.Role = standardRole;
            

            var createdUser = await _authRepo.Register (userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser); // TODO: create different DTO for clarity, as this has nothing to do with the list, temporary

            return CreatedAtRoute("GetUser", new {controller = "Users", id = createdUser.Id}, userToReturn);
        }


        [HttpPost ("login")]
        public async Task<IActionResult> Login (UserForLoginDto userForLoginDto) {
            var userFromRepo = await _authRepo.Login (userForLoginDto.Username.ToLower (), userForLoginDto.Password);

            if (userFromRepo == null) {
                return Unauthorized ();
            }

            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, userFromRepo.Id.ToString ()),
                new Claim (ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

            var user = _mapper.Map<UserForDetailedDto>(userFromRepo); // TODO: create different DTO for clarity, as this has nothing to do with the list, temporary

            return Ok (new {
                token = tokenHandler.WriteToken (token),
                user
            });
        }
    }
}