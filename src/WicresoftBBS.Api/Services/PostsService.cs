using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public class PostsService : IPostsService
    {
        private readonly BBSDbContext _context;

        public PostsService(BBSDbContext context)
        {
            _context = context;
        }

        public async Task<PostDTO> CreatePost(PostDTO postDto)
        {
            var post = new Post()
            {
                ClickCount = 0,
                Content = postDto.Content,
                PostType = postDto.PostType,
                PostTypeId = postDto.PostTypeId,
                Replies = null,
                Title = postDto.Title,
                CreateTime = DateTime.UtcNow,
                Creator = postDto.User,
                CreatorId = postDto.UserId,
                IsDeleted = false
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return ItemToDTO(post);
        }

        public async Task DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            post.IsDeleted = true;
            post.DeleteTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<PostDTO> GetPostById(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if(post == null)
            {
                return null;
            }

            return ItemToDTO(post);
        }

        public async Task<IEnumerable<PostDTO>> GetPosts(string searchTerm, int pageIndex, int pageSize, ulong? time, string timeType, string sortBy = "ReplyTime", string sortType = "desc")
        {
            List<PostDTO> result;
            if(time != null)
            {
                DateTime now = DateTime.UtcNow;
                if (timeType == "ago")
                {
                    DateTime startTime = now.AddSeconds(-(double)time);
                    result = await _context.Posts.Where(x => x.CreateTime >= startTime && x.CreateTime <= now
                            && (string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm))))
                        .Skip(pageIndex * pageSize).Take(pageSize)
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
                }
                else
                {
                    DateTime endTime=now.AddSeconds((double)time);
                    result = await _context.Posts.Where(x => x.CreateTime >= now && x.CreateTime <= endTime
                            && (string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm))))
                        .Skip(pageIndex * pageSize).Take(pageSize)
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
                }
            }
            else
            {
                result = await _context.Posts.Where(x => string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm)))
                        .Skip(pageIndex * pageSize).Take(pageSize)
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
            }

            switch (sortBy)
            {
                case "CreateTime":
                    result = result.OrderByDescending(x => x.CreateTime).ToList();
                    break;
                case "RepliesCount":
                    result = result.OrderByDescending(x => x.RepliesCount).ToList();
                    break;
                case "ClickCount":
                    result = result.OrderByDescending(x => x.ClickCount).ToList();
                    break;
                default:
                    result = result.OrderByDescending(x => x.LastReplyTime).ToList();
                    break;
            }

            if (sortType != "desc")
            {
                result.Reverse();
            }

            return result;
        }

        public async Task UpdatePost(PostDTO postDto)
        {
            var post = await _context.Posts.FindAsync(postDto.Id);

            post.ClickCount = postDto.ClickCount;
            post.Content = postDto.Content;
            post.PostType = postDto.PostType;
            post.PostTypeId = postDto.PostTypeId;
            post.Replies = postDto.Replies;
            post.Title = postDto.Title;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private static PostDTO ItemToDTO(Post post) =>
            new PostDTO
            {
                Id = post.Id,
                UserId = post.CreatorId,
                User = post.Creator,
                Title = post.Title,
                PostTypeId = post.PostTypeId,
                PostType = post.PostType,
                Content = post.Content,
                ClickCount = post.ClickCount,
                Replies = post.Replies,
                CreateTime = post.CreateTime
            };
    }
}
