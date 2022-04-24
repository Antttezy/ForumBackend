using ForumBackend.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace ForumBackend.Data.Context;

public class ForumContext : DbContext
{
    public DbSet<ForumUser>? Users { get; set; }
    public DbSet<UserAuth>? Authentication { get; set; }
    public DbSet<ForumPost>? Posts { get; set; }

    public ForumContext(DbContextOptions options): base(options)
    {
        
    }
}