namespace WicresoftBBS.Api.Models
{
    public class PostsSummary
    {
        public List<PostDTO> Posts { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
