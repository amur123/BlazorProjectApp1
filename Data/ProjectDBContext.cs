using BlazorProjectApp1.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorProjectApp1.Data;

internal sealed class ProjectDBContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options) { }

    public ProjectDBContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=BlazorProjectApp.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Post[] postsToSeed = new Post[6];

        for (int i = 1; i <= 6; i++)
        {
            postsToSeed[i - 1] = new Post
            {
                PostId = i,
                Title = $"Post {i}",
                Content = $"Content for post {i}"
            };
        }

        modelBuilder.Entity<Post>().HasData(data: postsToSeed);
    }
}
