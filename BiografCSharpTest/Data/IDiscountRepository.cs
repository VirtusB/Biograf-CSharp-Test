using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IDiscountRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<List<Discount>> GetDiscounts();
         Task<Discount> GetDiscount(int id);
         Task<int> GetHighestRequiredBookings();
         Task<Discount> GetCurrentDiscountStep(int id);
         Task<Discount> GetNextDiscountStep(int id);
    }
}