using BlazorProjectApp1.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorProjectApp1.Data;

/// <summary>
/// ProjectDBContext is the database context for the Blazor application.
/// Uses Entity Framework Core to manage the database.
/// Uses SQLite as the database provider.
/// Defines the tables for the application.
/// </summary>
internal sealed class ProjectDBContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options) { }

    public ProjectDBContext() { }

    public DbSet<RawAudioData> RawAudioData { get; set; }

    // Configures the database connection string using SQLite.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=BlazorProjectApp.db"); // If the context is not configured it uses SQLite as the database provider.
        }
    }

    // Database model including the tables and their relationships.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RawAudioData>()
            .HasKey(rawAudioData => new { rawAudioData.PostId, rawAudioData.AudioId });     // Tells EF Core that the combination of PostId and AudioId is the primary key for the RawAudioData entity. Required to allow multiple audio entries for single post.

        Post[] postsToSeed = new Post[6];

        for (int i = 1; i <= 6; i++) // For testing purposes 6 posts are seeded.
        {
            postsToSeed[i - 1] = new Post
            {
                PostId = i,
                Title = $"Post {i}",
                Content = $"Content for post {i}",
            };
        }
        modelBuilder.Entity<Post>().HasData(data: postsToSeed);
    }
}
