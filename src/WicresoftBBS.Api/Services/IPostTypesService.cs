using System.Threading.Tasks;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IPostTypesService
    {
        Task<IEnumerable<PostType>> GetPostTypes();
        Task<PostType> GetPostTypeById(int id);
        Task UpdatePostType(PostType postType);
        Task<PostType> CreatePostType(PostType postType);
        Task DeletePostType(int id);
        bool PostTypeExists(int id);
    }
}
