using Microsoft.EntityFrameworkCore;

namespace WicresoftBBS.Api.Models;

public class BBSDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Reply> Replies { get; set; }

    public DbSet<PostType> PostTypes { get; set; }

    public BBSDbContext(DbContextOptions<BBSDbContext> options)
        : base(options)
    {

    }
}