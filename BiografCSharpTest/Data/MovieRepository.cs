using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly BioContext _context;

        public MovieRepository(BioContext context) {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<string>> GetGenres() {
            var genres = await _context.Movies.Select(m => m.Genre).Distinct().ToListAsync();

            return genres;
        }

        public async Task<List<Movie>> GetAllMoviesWithoutPagination() {
            var movies = await _context.Movies.ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            
            return movie;
        }

        public async Task<PagedList<Movie>> GetMovies(MovieParams movieParams)
        {
            var movies = _context.Movies
                .OrderByDescending(m => m.Year)
                .AsQueryable();

            //TODO: repository bør ikke være stedet hvor movieParams tjekkes, da det ikke er CRUD

            if (movieParams.MinYear != 1920 || movieParams.MaxYear != 2018) {
                movies = movies.Where(u => u.Year >= movieParams.MinYear && u.Year <= movieParams.MaxYear);
            }

            if (movieParams.Genre != null) {
                movies = movies.Where(g => g.Genre == movieParams.Genre);
            }

            if (movieParams.Stars != 0) {
                movies = movies.Where(s => s.Stars >= movieParams.Stars);
            }

            if (movieParams.Likees) {
                var userLikees = await GetUserFavorites(movieParams.UserId); 
                movies = movies.Where(u => userLikees.Contains(u.Id));
            }

            
            return await PagedList<Movie>.CreateAsync(movies, movieParams.PageNumber, movieParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserFavorites(int id) {
            var user = await _context.Users
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Likees
                    .Where(u => u.LikerId == id)
                    .Select(i => i.LikeeId);
        }
    }
}