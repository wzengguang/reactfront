using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IBBSRepo
    {
        Task<int> AddUsers(IList<User> users);
        Task<int> ReFillUsers(IList<User> users);
        List<User> GetUsers();
        User GetUser(int id);
    }
}
