using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Helpers;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovie(int id) {
            var movie = await _repo.GetMovie(id);

            if (movie != null) {
                return Ok(movie);
            }

            return NoContent();
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovies ([FromQuery]MovieParams movieParams) {
            
            var movies = await _repo.GetMovies(movieParams);
            var moviesToReturn = _mapper.Map<IEnumerable<MovieForListDto>>(movies);


            Response.AddPagination(movies.CurrentPage, movieParams.PageSize, movies.TotalCount, movies.TotalPages);

            return Ok (moviesToReturn);
        }
    }
}