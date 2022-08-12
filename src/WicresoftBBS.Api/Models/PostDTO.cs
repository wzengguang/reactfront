namespace WicresoftBBS.Api.Models
{
    public class PostDTO
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public int? PostTypeId { get; set; }

        public PostType PostType { get; set; }

        public string Content { get; set; }

        public int ClickCount { get; set; }

        public IList<Reply> Replies { get; set; }

        public DateTime? LastReplyTime { get { return RepliesCount > 0 ? Replies.Max(x => x.CreateTime) : null; } }

        public int RepliesCount { get { return Replies == null ? 0 : Replies.Count; } }

        public DateTime CreateTime { get; set; }
    }
}
