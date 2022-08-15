using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IUsersService
    {
        Task<int> AddUsers(IList<User> users);
        Task<int> ReFillUsers(IList<User> users);
        List<User> GetUsers();
        User GetUser(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(int id);
        Task UpdateUser(UserDTO user);
        Task<UserDTO> CreateUser(UserDTO user);
        Task DeleteUser(int id);
        bool UserExists(int id);
    }
}
