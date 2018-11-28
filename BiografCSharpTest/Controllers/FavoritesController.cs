using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BiografCSharpTest.Data;
using BiografCSharpTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiografCSharpTest.Controllers
{
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMovieRepository _movieRepo;
        public FavoritesController(IMapper mapper, IFavoriteRepository favoriteRepo, IUserRepository userRepo, IMovieRepository movieRepo)
        {
            this._mapper = mapper;
            this._favoriteRepo = favoriteRepo;
            this._userRepo = userRepo;
            this._movieRepo = movieRepo;
        }

        [HttpGet("{id}/{movieId}")]
        public async Task<IActionResult> GetFavoriteMovie(int id, int movieId) {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

            var like = await _favoriteRepo.GetFavorite(id, movieId);

            if (like == null) {
                return NoContent();
            }

            return Ok(like);
        }   

        [HttpPost("{id}/{movieId}")]
        public async Task<IActionResult> AddFavoriteMovie(int id, int movieId) {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

            var like = await _favoriteRepo.GetFavorite(id, movieId);

            if (like != null) {
                return BadRequest("Filmen er allerede tilføjet som favorit");
            }

            if (await _movieRepo.GetMovie(movieId) == null) {
                return NotFound();
            }

            like = new Favorite {
                LikerId = id,
                LikeeId = movieId
            };

            _favoriteRepo.Add<Favorite>(like); 

            if (await _favoriteRepo.SaveAll()) {
                return Ok();
            }

            return BadRequest("Kunne ikke tilføje filmen som favorit");
        }

        [HttpDelete("{id}/{movieId}")]
        public async Task<IActionResult> RemoveFavoriteMovie(int id, int movieId) {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized();
            }

            var like = await _favoriteRepo.GetFavorite(id, movieId);

            if (like == null) {
                return BadRequest("Filmen er ikke på din liste");
            }

            if (await _movieRepo.GetMovie(movieId) == null) {
                return NotFound("Filmen eksisterer ikke");
            }

            _favoriteRepo.Delete<Favorite>(like); 

            if (await _favoriteRepo.SaveAll()) {
                return Ok();
            }

            return BadRequest("Kunne ikke slette filmen som favorit");
        }
    }
}