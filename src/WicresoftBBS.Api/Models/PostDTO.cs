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
        public DateTime LastReplyTime { get { return Replies.Max(x => x.CreateTime); } }

        public int RepliesCount { get { return Replies.Count; } }

        public DateTime CreateTime { get; set; }
    }
}
