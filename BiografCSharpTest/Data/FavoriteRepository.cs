using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly BioContext _context;
        public FavoriteRepository(BioContext context) {
            this._context = context;
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
        public async Task<Favorite> GetFavorite(int userId, int movieId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(u => u.LikerId == userId && u.LikeeId == movieId);
        }
    }
}