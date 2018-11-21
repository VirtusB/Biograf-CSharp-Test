using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly BioContext _context;

        public DiscountRepository(BioContext context) {
            _context = context;
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
    }
}