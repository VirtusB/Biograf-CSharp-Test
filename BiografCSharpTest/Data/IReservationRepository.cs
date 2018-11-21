using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IReservationRepository
    {
         Task<List<Reservation>> GetReservations(int id);
        Task<Reservation> GetReservation(int id);
        Task<int> GetPaidReservationsCount(int id);
        Task<bool> SaveAll();
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
    }
}