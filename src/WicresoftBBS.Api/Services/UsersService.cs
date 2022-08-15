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

        public User GetUser(int id)
        {
            return _context.Users.Where(user => user.Id == id).First();
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

        public async Task<UserDTO> CreateUser(UserDTO userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                UserType = userDto.UserType,
                CreateTime = DateTime.UtcNow,
                Email = userDto.Email,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ItemToDTO(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _context.Users.Select(x => ItemToDTO(x)).ToListAsync();
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            var userDto = ItemToDTO(user);
            userDto.PostsCount = await _context.Posts.CountAsync(x => x.CreatorId == id);
            userDto.RepliesCount = await _context.Replies.CountAsync(x => x.CreatorId == id);
            return userDto;
        }

        public async Task UpdateUser(UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;

            try
            {
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

        private static UserDTO ItemToDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                UserType = user.UserType,
                Email = user.Email,
                CreateTime = user.CreateTime
            };

    }
}
