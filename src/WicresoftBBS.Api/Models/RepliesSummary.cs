namespace WicresoftBBS.Api.Models
{
    public class RepliesSummary
    {
        public List<ReplyDTO> Replies { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
