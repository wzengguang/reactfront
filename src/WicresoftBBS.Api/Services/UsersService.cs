using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public class UsersService : IUsersService
    {
        private BBSDbContext _context;
        public UsersService(BBSDbContext wicresoftForumContext)
        {
            _context = wicresoftForumContext;
        }

        public Task<int> AddUsers(IList<User> users)
        {
            _context.Users.AddRange(users);
            return _context.SaveChangesAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(user=>user.Id == id).First();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public Task<int> ReFillUsers(IList<User> users)
        {
            foreach (var user in _context.Users.ToList())
            {
                _context.Users.Remove(user);
            }
            _context.Users.AddRange(users);
            return _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
