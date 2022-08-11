using System.ComponentModel.DataAnnotations.Schema;

namespace WicresoftBBS.Api.Models;

[Table("Posts")]
public class Post
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? CreatorId { get; set; }

    public User Creator { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Title { get; set; }

    public int? PostTypeId { get; set; }

    public PostType PostType { get; set; }

    [Column(TypeName = "text")]
    public string Content { get; set; }

    public int ClickCount { get; set; }

    public IList<Reply> Replies { get; set; }

    public DateTime CreateTime { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeleteTime { get; set; }
}