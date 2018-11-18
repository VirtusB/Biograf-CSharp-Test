using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IShowRepository
    {
        Task<bool> SaveAll();
        Task<Show> GetShow(int id);
        Task<PagedList<Show>> GetShows(ShowParams showParams);
    }
}