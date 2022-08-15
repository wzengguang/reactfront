using Microsoft.EntityFrameworkCore;
using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public class RepliesService : IRepliesService
    {
        private readonly BBSDbContext _context;

        public RepliesService(BBSDbContext context)
        {
            _context = context;
        }

        public async Task<ReplyDTO> CreateReply(ReplyDTO replyDto)
        {
            var reply = new Reply
            {
                Content = replyDto.Content,
                Post = replyDto.Post,
                PostId = replyDto.PostId,
                CreateTime = DateTime.UtcNow,
                Creator = replyDto.Creator,
                CreatorId = replyDto.CreatorId,
                FloorId = replyDto.Post.Replies.Count > 0 ? replyDto.Post.Replies.Max(x => x.FloorId) + 1 : 1
            };

            _context.Replies.Add(reply);
            await _context.SaveChangesAsync();

            return ItemToDTO(reply);
        }

        public async Task DeleteReply(int id)
        {
            var reply = await _context.Replies.FindAsync(id);

            reply.IsDeleted = true;
            reply.DeleteTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<RepliesSummary> GetRepliesByPostId(int id, int pageIndex, int pageSize)
        {
            var list = await _context.Replies.Where(x => x.PostId == id).OrderByDescending(x => x.CreateTime).Select(x => ItemToDTO(x)).ToListAsync();
            var repliesSummary = new RepliesSummary
            {
                TotalCount = list.Count,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Replies = list.Skip(pageIndex * pageSize).Take(pageSize).ToList()
            };

            return repliesSummary;
        }

        public async Task<ReplyDTO> GetReplyById(int id)
        {
            var reply = await _context.Replies.FindAsync(id);

            if (reply == null)
            {
                return null;
            }

            return ItemToDTO(reply);
        }

        public async Task UpdateReply(ReplyDTO replyDto)
        {
            var reply = await _context.Replies.FindAsync(replyDto.Id);

            reply.Content = replyDto.Content;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ReplyExists(int id)
        {
            return _context.Replies.Any(e => e.Id == id);
        }

        private static ReplyDTO ItemToDTO(Reply reply) =>
            new ReplyDTO
            {
                Id = reply.Id,
                CreatorId = reply.CreatorId,
                Creator = reply.Creator,
                FloorId = reply.FloorId,
                Content = reply.Content,
                Post = reply.Post,
                PostId = reply.PostId,
                CreateTime = reply.CreateTime
            };
    }
}
