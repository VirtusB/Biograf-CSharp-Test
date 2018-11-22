using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiografCSharpTest.Helpers;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly BioContext _context;

        public UserRepository(BioContext context) {
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

        public async Task<Role> GetRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            
            return role;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
            
            return role;
        }

        

        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            
            return roles;
        }

        


        public async Task<PagedList<User>> GetUsers (UserParams userParams) {
            var users = _context.Users.Include(r => r.Role)
                .OrderByDescending(u => u.LastActive)
                .AsQueryable();

            //TODO: fiks så kun bliver lavet CRUD i repository'en
            if (userParams.Enabled != true) {
                users = users.Where(u => u.Enabled == userParams.Enabled);
            }

            users = users.Where(u => u.Id != userParams.UserId);

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}