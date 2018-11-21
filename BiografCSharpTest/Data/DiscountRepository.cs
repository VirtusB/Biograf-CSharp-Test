using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly BioContext _context;
        private readonly IReservationRepository _reservationRepo;

        public DiscountRepository(BioContext context, IReservationRepository reservationRepo) {
            _context = context;
            this._reservationRepo = reservationRepo;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task<Discount> GetDiscount(int id)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);
            
            return discount;
        }

        public async Task<List<Discount>> GetDiscounts()
        {
            var discounts = await _context.Discounts.ToListAsync();
            
            return discounts;
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Discount> GetCurrentDiscountStep(int id)
        {
            var paidReservations = await _reservationRepo.GetPaidReservationsCount(id);

            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.RequiredBookings <= paidReservations);

            return discount;
        }

        public async Task<int> GetHighestRequiredBookings() {
            var highestStepRequiredBookings = await _context.Discounts.MaxAsync(d => d.RequiredBookings);

            return highestStepRequiredBookings;
        }

        public async Task<Discount> GetNextDiscountStep(int id)
        {
            var paidReservations = await _reservationRepo.GetPaidReservationsCount(id);
            
    
            var discount = await _context.Discounts.OrderBy(d => d.RequiredBookings).FirstOrDefaultAsync(d => d.RequiredBookings >= paidReservations);

            return discount;
        }
    }
}