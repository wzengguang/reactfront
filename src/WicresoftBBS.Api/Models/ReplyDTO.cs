namespace WicresoftBBS.Api.Models
{
    public class ReplyDTO
    {
        public int Id { get; set; }

        public int? CreatorId { get; set; }

        public User Creator { get; set; }

        public int? PostId { get; set; }

        public Post Post { get; set; }

        public int FloorId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
