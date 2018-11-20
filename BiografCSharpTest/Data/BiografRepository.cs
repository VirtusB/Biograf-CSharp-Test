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

        public async Task<Discount> GetDiscount(int id)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            
            return discount;
        }

        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            
            return roles;
        }

        public async Task<List<Discount>> GetDiscounts()
        {
            var discounts = await _context.Discounts.ToListAsync();
            
            return discounts;
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

            if (movieParams.Stars != 0) {
                movies = movies.Where(s => s.Stars >= movieParams.Stars);
            }

            
            return await PagedList<Movie>.CreateAsync(movies, movieParams.PageNumber, movieParams.PageSize);
        }

        public async Task<PagedList<User>> GetUsers (UserParams userParams) {
            var users = _context.Users.Include(r => r.Role)
                .OrderByDescending(u => u.LastActive)
                .AsQueryable();

            if (userParams.Enabled != true) {
                users = users.Where(u => u.Enabled == userParams.Enabled);
            }

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

        public async Task<List<Reservation>> GetReservations(int id)
        {
            

            var reservations = await _context.Reservations
                .Where(r => r.User.Id == id)
                .OrderByDescending(r => r.Created)
                .Include(s => s.Show)
                .ThenInclude(m => m.Movie)
                .ToListAsync();


            return reservations;
        }

        public async Task<Reservation> GetReservation(int id)
        {
            
            var reservation = await _context.Reservations
                .Where(r => r.Id == id)
                .Include(r => r.User)
                .Include(s => s.Show) 
                .ThenInclude(m => m.Movie)
                .FirstOrDefaultAsync();

      

       


            return reservation;
        }

        public async Task<int> GetPaidReservationsCount(int id)
        {
            // 0 == canceled
            // 1 == reserved
            // 2 == paid
            // 3 == redeemed
            int StatePaid = 2;
            var count = await _context.Reservations.CountAsync(r => r.User.Id == id && r.BookingState == StatePaid);

            return count;
        }

        public async Task<Discount> GetCurrentDiscountStep(int id)
        {
            var paidReservations = await this.GetPaidReservationsCount(id);

            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.RequiredBookings <= paidReservations);

            return discount;
        }

        public async Task<int> GetHighestRequiredBookings() {
            var highestStepRequiredBookings = await _context.Discounts.MaxAsync(d => d.RequiredBookings);

            return highestStepRequiredBookings;
        }

        public async Task<Discount> GetNextDiscountStep(int id)
        {
            var paidReservations = await this.GetPaidReservationsCount(id);
            
    
            var discount = await _context.Discounts.OrderBy(d => d.RequiredBookings).FirstOrDefaultAsync(d => d.RequiredBookings >= paidReservations);

            return discount;
        }

        
    }
}