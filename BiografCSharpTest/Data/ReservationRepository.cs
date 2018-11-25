using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly BioContext _context;
        public ReservationRepository(BioContext context) {
            this._context = context;
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
    }
}