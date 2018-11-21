using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IMovieRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<List<string>> GetGenres();
        Task<Movie> GetMovie(int id);
        Task<List<Movie>> GetAllMoviesWithoutPagination();
        Task<PagedList<Movie>> GetMovies(MovieParams movieParams);
    }
} 