using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public class BBSRepo : IBBSRepo
    {
        private BBSDbContext _context;
        public BBSRepo(BBSDbContext wicresoftForumContext)
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
            return _context.Users.Where(user=>user.Id == id).First();
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
    }
}
