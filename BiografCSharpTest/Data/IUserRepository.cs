using System.Collections.Generic;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;

namespace BiografCSharpTest.Data
{
    public interface IUserRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<Role> GetRole(int id);
        Task<Role> GetRoleByName(string name);
        Task<List<Role>> GetRoles();
        Task<PagedList<User>> GetUsers(UserParams userParams);
    }
}