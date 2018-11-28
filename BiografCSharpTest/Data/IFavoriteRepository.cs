using System.Threading.Tasks;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IFavoriteRepository
    {
        Task<Favorite> GetFavorite(int userId, int movieId);
        void Add<T>(T entity) where T: class;
        Task<bool> SaveAll();
        void Delete<T>(T entity) where T: class;
    }
}