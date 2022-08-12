using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IUsersService
    {
        Task<int> AddUsers(IList<User> users);
        Task<int> ReFillUsers(IList<User> users);
        List<User> GetUsers();
        User GetUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
        Task<User> CreateUser(User user);
        Task DeleteUser(int id);
        bool UserExists(int id);
    }
}
