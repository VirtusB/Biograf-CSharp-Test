using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly BioContext _context;
        public ShowRepository(BioContext context)
        {
            this._context = context;
        }
        public async Task<Show> GetShow(int id)
        {
            var show = await _context.Shows.Include(m => m.Movie).FirstOrDefaultAsync(s => s.Id == id);

            return show;
        }

        public async Task<PagedList<Show>> GetShows(ShowParams showParams)
        {
            var shows = _context.Shows
                .Include(m => m.Movie)
                .OrderByDescending(s => s.StartDate)
                .AsQueryable();

            //TODO: fiks så kun CRUD bliver lavet i repository'en
            if (showParams.MaxTicketPrice != 500) {
                shows = shows.Where(s => s.TicketPrice < showParams.MaxTicketPrice);
            }

            if (showParams.Stars != 0) {
                shows = shows.Where(s => s.Movie.Stars >= showParams.Stars);
            }

            if (showParams.HallNumber != 0) {
                shows = shows.Where(s => s.HallNumber == showParams.HallNumber);
            }

            if (showParams.MovieId != 0) {
                shows = shows.Where(s => s.Movie.Id == showParams.MovieId);
            }
            
            return await PagedList<Show>.CreateAsync(shows, showParams.PageNumber, showParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<List<Show>> GetAllShowsWithoutPagination() {
            var shows = await _context.Shows.Include(s => s.Movie).ToListAsync();

            return shows;
        }
    }
}