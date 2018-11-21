using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IBiografRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<int> GetHighestRequiredBookings();
        Task<Discount> GetCurrentDiscountStep(int id);
        Task<Discount> GetNextDiscountStep(int id);
        Task<Role> GetRole(int id);
        Task<List<Role>> GetRoles();
        Task<List<Discount>> GetDiscounts();
        Task<Discount> GetDiscount(int id);
        Task<List<string>> GetGenres();
        Task<List<Reservation>> GetReservations(int id);
        Task<Reservation> GetReservation(int id);
        Task<int> GetPaidReservationsCount(int id);
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<Movie> GetMovie(int id);
        Task<List<Movie>> GetAllMoviesWithoutPagination();
        Task<PagedList<Movie>> GetMovies(MovieParams movieParams);
    }
}