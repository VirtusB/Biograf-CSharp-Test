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
    public class MoviesController : ControllerBase
    {
        private readonly IBiografRepository _repo;
        private readonly IMapper _mapper;

        public MoviesController(IBiografRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet("genres")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenres() {
            var genres = await _repo.GetGenres();

            return Ok(genres);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovie(int id) {
            var movie = await _repo.GetMovie(id);

            if (movie != null) {
                return Ok(movie);
            }

            return NoContent();
        } 


        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieForCreationDto movieForCreationDto) {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _repo.GetUser(userMakingRequestId);

            List<string> allowedRoles = new List<string>(new string[] 
            { 
                "Personale",
                "Admin" 
            });

            

            if (!allowedRoles.Contains(userMakingRequest.Role.Name)) {
                return Unauthorized();
            }

            var movie = _mapper.Map<Movie>(movieForCreationDto);

            _repo.Add(movie); 

            if (await _repo.SaveAll()) {
                var movieToReturn = _mapper.Map<MovieForReturnDto>(movie);
                return CreatedAtRoute("GetMovie", new {id = movie.Id}, movieToReturn);
            }

            throw new Exception("Kunne ikke oprette filmen");
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovies ([FromQuery]MovieParams movieParams) {
            
            var movies = await _repo.GetMovies(movieParams);
            var moviesToReturn = _mapper.Map<IEnumerable<MovieForListDto>>(movies);


            Response.AddPagination(movies.CurrentPage, movieParams.PageSize, movies.TotalCount, movies.TotalPages);

            return Ok (moviesToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMovies () {
            var userMakingRequestId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userMakingRequest = await _repo.GetUser(userMakingRequestId);

            List<string> allowedRoles = new List<string>(new string[] 
            { 
                "Personale",
                "Admin" 
            });

            
            if (!allowedRoles.Contains(userMakingRequest.Role.Name)) {
                return Unauthorized();
            }
            
            var movies = await _repo.GetAllMoviesWithoutPagination();
            var moviesToReturn = _mapper.Map<IEnumerable<MovieForListDto>>(movies);

            return Ok (moviesToReturn);
        }
    }
}