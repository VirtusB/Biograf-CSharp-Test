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
        private readonly IShowRepository _repo;
        private readonly IBiografRepository _bioRepo;
        private readonly IMapper _mapper;

        public ShowsController(IShowRepository repo, IMapper mapper, IBiografRepository bioRepo)
        {
            this._mapper = mapper;
            this._repo = repo;
            this._bioRepo = bioRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShow(ShowForCreationDto showForCreationDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _bioRepo.GetUser(userMakingRequestId);

            List<string> allowedRoles = new List<string>(new string[] 
            { 
                "Personale",
                "Admin" 
            });

            

            if (!allowedRoles.Contains(userMakingRequest.Role.Name)) {
                return Unauthorized();
            }


            var movieForShow = await _bioRepo.GetMovie(showForCreationDto.Movie.Id);
            showForCreationDto.Movie = movieForShow;

            var show = _mapper.Map<Show>(showForCreationDto);

            _bioRepo.Add(show); 

            if (await _repo.SaveAll()) {
                var showToReturn = _mapper.Map<ShowForReturnDto>(show);
                return CreatedAtRoute("GetShow", new {id = show.Id}, showToReturn);
            }

            throw new Exception("Kunne ikke oprette forestillingen");
        }


        [HttpGet("{id}", Name = "GetShow")]
        [AllowAnonymous]
        public async Task<IActionResult> GetShow(int id) {
            var show = await _repo.GetShow(id);

            if (show != null) {
                return Ok(show);
            }

            return NoContent();
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetShows ([FromQuery]ShowParams showParams) {
            
            var shows = await _repo.GetShows(showParams);
            var showsToReturn = _mapper.Map<IEnumerable<ShowForListDto>>(shows);


            Response.AddPagination(shows.CurrentPage, showParams.PageSize, shows.TotalCount, shows.TotalPages);

            return Ok (showsToReturn);
        }
    }
}