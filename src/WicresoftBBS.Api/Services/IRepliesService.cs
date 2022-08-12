using WicresoftBBS.Api.Models;

namespace WicresoftBBS.Api.Services
{
    public interface IRepliesService
    {
        Task<RepliesSummary> GetRepliesByPostId(int id, int pageIndex, int pageSize);
        Task<ReplyDTO> GetReplyById(int id);
        Task UpdateReply(ReplyDTO replyDto);
        Task<ReplyDTO> CreateReply(ReplyDTO replyDto);
        Task DeleteReply(int id);
        bool ReplyExists(int id);
    }
}
