using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly IMapper _mapper;

        public ShowsController(IShowRepository showRepo, IMapper mapper, IUserRepository userRepo, IMovieRepository movieRepo)
        {
            this._mapper = mapper;
            this._showRepo = showRepo;
            this._userRepo = userRepo;
            this._movieRepo = movieRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShow(ShowForCreationDto showForCreationDto) {
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


            var movieForShow = await _movieRepo.GetMovie(showForCreationDto.Movie.Id);
            showForCreationDto.Movie = movieForShow;

            var show = _mapper.Map<Show>(showForCreationDto);

            _showRepo.Add(show); 

            if (await _showRepo.SaveAll()) {
                var showToReturn = _mapper.Map<ShowForReturnDto>(show);
                return CreatedAtRoute("GetShow", new {id = show.Id}, showToReturn);
            }

            throw new Exception("Kunne ikke oprette forestillingen");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShow (int id, ShowForUpdateByAdminDto showForUpdateByAdminDto) {
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

            var showFromRepo = await _showRepo.GetShow(id);

            // TODO: skal laves om
            var movieForShow = await _movieRepo.GetMovie(showForUpdateByAdminDto.Movie.Id);
            showForUpdateByAdminDto.Movie = movieForShow;

            

            _mapper.Map(showForUpdateByAdminDto, showFromRepo);

            if (await _showRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af forestilling med {id} fejlede");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id) {
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

            //TODO: slet show uden først at lave et DB kald
            var showFromRepo = await _showRepo.GetShow(id);

            _showRepo.Delete(showFromRepo);

            if (await _showRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette forestilling med ID {id}");
        }


        [HttpGet("{id}", Name = "GetShow")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShow(int id) {
            var show = await _showRepo.GetShow(id);

            if (show != null) {
                return Ok(show);
            }

            return NoContent();
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetShows ([FromQuery]ShowParams showParams) {
            
            var shows = await _showRepo.GetShows(showParams);
            var showsToReturn = _mapper.Map<IEnumerable<ShowForListDto>>(shows);


            Response.AddPagination(shows.CurrentPage, showParams.PageSize, shows.TotalCount, shows.TotalPages);

            return Ok (showsToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllShows () {
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
            
            var shows = await _showRepo.GetAllShowsWithoutPagination();
            var showsToReturn = _mapper.Map<IEnumerable<ShowForListDto>>(shows);

            return Ok (showsToReturn);
        }
    }
}