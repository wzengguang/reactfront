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
                PostTypeId = postDto.PostTypeId,
                Title = postDto.Title,
                CreateTime = DateTime.UtcNow,
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

            if (post == null)
            {
                return null;
            }

            post.ClickCount++;
            await _context.SaveChangesAsync();

            return ItemToDTO(post);
        }

        public async Task<PostsSummary> GetPosts(string searchTerm, int pageIndex, int pageSize, ulong? time, string timeType = "ago", string sortBy = "ReplyTime", string sortType = "desc")
        {
            List<PostDTO> list;
            if (time != null)
            {
                DateTime now = DateTime.UtcNow;
                if (timeType.ToLower().Trim().Equals("ago"))
                {
                    DateTime startTime = now.AddSeconds(-(double)time);
                    list = await _context.Posts.Where(x => x.CreateTime >= startTime && x.CreateTime <= now
                            && (string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm))))
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
                }
                else
                {
                    DateTime endTime = now.AddSeconds((double)time);
                    list = await _context.Posts.Where(x => x.CreateTime >= now && x.CreateTime <= endTime
                            && (string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm))))
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
                }
            }
            else
            {
                list = await _context.Posts.Where(x => string.IsNullOrEmpty(searchTerm) ? true : (x.Title.Contains(searchTerm) || x.Content.Contains(searchTerm)))
                        .Select(x => ItemToDTO(x))
                        .ToListAsync();
            }


            switch (sortBy.ToLower().Trim())
            {
                case "createtime":
                    list = list.OrderByDescending(x => x.CreateTime).ToList();
                    break;
                case "repliescount":
                    list = list.OrderByDescending(x => x.RepliesCount).ToList();
                    break;
                case "clickcount":
                    list = list.OrderByDescending(x => x.ClickCount).ToList();
                    break;
                default:
                    list = list.OrderByDescending(x => x.LastReplyTime).ToList();
                    break;
            }

            if (!sortType.ToLower().Trim().Equals("desc"))
            {
                list.Reverse();
            }

            var postsSummary = new PostsSummary
            {
                TotalCount = list.Count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Posts = list.Skip(pageIndex * pageSize).Take(pageSize).ToList()
            };

            return postsSummary;
        }

        public async Task<PostsSummary> GetPostsByUserId(int userId, int pageIndex, int pageSize)
        {
            var list = await _context.Posts.Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.CreateTime)
                .Select(x => ItemToDTO(x))
                .ToListAsync();

            var postsSummmary = new PostsSummary
            {
                TotalCount = list.Count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Posts = list.Skip(pageIndex * pageSize).Take(pageSize).ToList()
            };

            return postsSummmary;
        }

        public async Task UpdatePost(PostDTO postDto)
        {
            var post = await _context.Posts.FindAsync(postDto.Id);

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

        public Task<int> AddPosts(IList<Post> posts)
        {
            _context.Posts.AddRange(posts);
            return _context.SaveChangesAsync();
        }
    }
}
