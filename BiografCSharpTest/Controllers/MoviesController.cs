using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Dtos;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly IMapper _mapper;
        private readonly BioContext _context;

        public MoviesController(IUserRepository userRepo, IMapper mapper, IMovieRepository movieRepo, IFavoriteRepository favoriteRepo, BioContext context)
        {
            this._mapper = mapper;
            this._userRepo = userRepo;
            this._movieRepo = movieRepo;
            this._favoriteRepo = favoriteRepo;
            this._context = context;
        }

        [HttpGet("popular")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiveMostFavorited() {
            List<MovieForPopularListDto> moviesForReturn = new List<MovieForPopularListDto>();

            var mostFavorited = await _context.Favorites
                .GroupBy(f => f.LikeeId)
                .OrderByDescending(gp => gp.Count())
                .Take(5)
                .Select(f => f.Key)
                .ToListAsync();


            for (int i = 0; i < mostFavorited.Count; i++)
            {
                var movie = await _movieRepo.GetMovie(mostFavorited[i]);
                var count = await _favoriteRepo.GetCountOfUsersWhoFavorited(mostFavorited[i]);

                var movieForReturn = _mapper.Map<MovieForPopularListDto>(movie);
                movieForReturn.CountLikedBy = count;

                moviesForReturn.Add(movieForReturn);
            }

            return Ok(moviesForReturn);
        }

        [HttpGet("genres")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenres() {
            var genres = await _movieRepo.GetGenres();

            return Ok(genres);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovie(int id) {
            var movie = await _movieRepo.GetMovie(id);

            if (movie != null) {
                return Ok(movie);
            }

            return NoContent();
        } 


        [HttpPost]
        public async Task<IActionResult> CreateMovie(MovieForCreationDto movieForCreationDto) {
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

            var movie = _mapper.Map<Movie>(movieForCreationDto);

            _movieRepo.Add(movie); 

            if (await _movieRepo.SaveAll()) {
                var movieToReturn = _mapper.Map<MovieForReturnDto>(movie);
                return CreatedAtRoute("GetMovie", new {id = movie.Id}, movieToReturn);
            }

            throw new Exception("Kunne ikke oprette filmen");
        } 

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovies ([FromQuery]MovieParams movieParams) {
            var currentUserId = 0;
            if (User.FindFirst(ClaimTypes.NameIdentifier) != null) {
                currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                movieParams.UserId = currentUserId;
            }
              
            
            var movies = await _movieRepo.GetMovies(movieParams);
            var moviesToReturn = _mapper.Map<IEnumerable<MovieForListDto>>(movies);


            Response.AddPagination(movies.CurrentPage, movieParams.PageSize, movies.TotalCount, movies.TotalPages);

            return Ok (moviesToReturn);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMovies () {
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
            
            var movies = await _movieRepo.GetAllMoviesWithoutPagination();
            var moviesToReturn = _mapper.Map<IEnumerable<MovieForListDto>>(movies);

            return Ok (moviesToReturn);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie (int id, MovieForUpdateByAdminDto movieForUpdateByAdminDto) {
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

            var movieFromRepo = await _movieRepo.GetMovie(id);

            

            _mapper.Map(movieForUpdateByAdminDto, movieFromRepo);

            if (await _movieRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Opdatering af forestilling med {id} fejlede");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id) {
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

            //TODO: slet film uden først at lave et DB kald
            var movieFromRepo = await _movieRepo.GetMovie(id);

            _movieRepo.Delete(movieFromRepo);

            if (await _movieRepo.SaveAll()) {
                return NoContent();
            }

            throw new Exception($"Kunne ikke slette forestilling med ID {id}");
        }

        
    }
}