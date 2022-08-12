using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<PostDTO>> GetPosts(string searchTerm, int pageIndex, int pageSize, ulong? time, string timeType = "ago", string sortBy = "ReplyTime", string sortType = "desc");
        Task<IEnumerable<PostDTO>> GetPostsByUserId(int userId, int pageIndex, int pageSize);
        Task<PostDTO> GetPostById(int id);
        Task UpdatePost(PostDTO postDto);
        Task<PostDTO> CreatePost(PostDTO postDto);
        Task<int> AddPosts(IList<Post> posts);
        Task DeletePost(int id);
        bool PostExists(int id);
    }
}
