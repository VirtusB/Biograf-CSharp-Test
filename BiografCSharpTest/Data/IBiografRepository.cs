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
        Task<Role> GetRole(int id);
        Task<List<string>> GetGenres();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<Movie> GetMovie(int id);
        Task<PagedList<Movie>> GetMovies(MovieParams movieParams);
    }
}