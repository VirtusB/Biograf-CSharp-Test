using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class BiografRepository : IBiografRepository
    {
        private readonly BioContext _context;

        public BiografRepository(BioContext context) {
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

        public async Task<Movie> GetMovie(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            
            return movie;
        }

        public async Task<Role> GetRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            
            return role;
        }

        public async Task<List<string>> GetGenres() {
            var genres = await _context.Movies.Select(m => m.Genre).Distinct().ToListAsync();

            return genres;
        }

        public async Task<PagedList<Movie>> GetMovies(MovieParams movieParams)
        {
            var movies = _context.Movies
                .OrderByDescending(m => m.Year)
                .AsQueryable();

            if (movieParams.MinYear != 1920 || movieParams.MaxYear != 2018) {
                movies = movies.Where(u => u.Year >= movieParams.MinYear && u.Year <= movieParams.MaxYear);
            }

            if (movieParams.Genre != null) {
                movies = movies.Where(g => g.Genre == movieParams.Genre);
            }

            
            return await PagedList<Movie>.CreateAsync(movies, movieParams.PageNumber, movieParams.PageSize);
        }

        public async Task<PagedList<User>> GetUsers (UserParams userParams) {
            var users = _context.Users.Include(r => r.Role)
                .OrderByDescending(u => u.LastActive)
                .AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}