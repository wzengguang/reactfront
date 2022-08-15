using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public class PostTypesService : IPostTypesService
    {
        private readonly BBSDbContext _context;

        public PostTypesService(BBSDbContext context)
        {
            _context = context;
        }

        public Task<int> AddPostTypes(IList<PostType> postTypes)
        {
            _context.PostTypes.AddRange(postTypes);
            return _context.SaveChangesAsync();
        }

        public async Task<PostType> CreatePostType(PostType postType)
        {
            _context.PostTypes.Add(postType);
            await _context.SaveChangesAsync();

            return postType;
        }

        public async Task DeletePostType(int id)
        {
            var postType = await _context.PostTypes.FindAsync(id);

            _context.PostTypes.Remove(postType);
            await _context.SaveChangesAsync();
        }

        public async Task<PostType> GetPostTypeById(int id)
        {
            var postType = await _context.PostTypes.FindAsync(id);

            return postType;
        }

        public async Task<IEnumerable<PostType>> GetPostTypes()
        {
            return await _context.PostTypes.ToListAsync();
        }

        public bool PostTypeExists(int id)
        {
            return _context.PostTypes.Any(e => e.Id == id);
        }

        public async Task UpdatePostType(PostType postType)
        {
            try
            {
                _context.Entry(postType).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
