namespace WicresoftBBS.Api.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int UserType { get; set; }

        public int PostsCount { get; set; }

        public int RepliesCount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
