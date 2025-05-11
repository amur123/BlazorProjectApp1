using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlazorProjectApp1.Data;

/// <summary>
/// PostManager is a service that manages posts in the application based on current user and role.
/// Provides data access logic for user owner and admin permissions.
/// <summary>

internal class PostManager
{
    private readonly ProjectDBContext _dbContext;
    private readonly AuthenticationStateProvider _authProvider;

    // Gets the database context and authentication state provider via dependency injection.
    public PostManager(ProjectDBContext dbContext, AuthenticationStateProvider authProvider)
    {
        _dbContext = dbContext;
        _authProvider = authProvider;
    }

    // Gets all posts visible for the current user or all posts are visible if the user is an administrator.
    public async Task<List<Post>> GetPostsForCurrentUserAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Admin can see everything.
        if (user.IsInRole("Administrator"))
        {
            return await _dbContext.Posts.ToListAsync();
        }

        // Non-admin users can only see their own posts.
        var username = user.Identity?.Name;
        return await _dbContext.Posts
            .Where(p => p.Username == username)
            .ToListAsync();
    }

    // Gets a specific post if the user is an administrator or the author of the post. Getting the post by Id only if the user is an administrator or the author of the post.
    public async Task<Post?> GetPostIfAuthorisedAsync(int postId)
    {
        var post = await _dbContext.Posts.FindAsync(postId);
        if (post is null) return null;

        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // If the user is an administrator or the author of the post it returns the post.
        if (user.IsInRole("Administrator") || post.Username == user.Identity?.Name)
            return post;

        return null; // Not authorised to view the post.
    }

    // Ties the post to the user creating it by setting the username property. Uses testuser999999999 for unit tests.
    public async Task<bool> CreatePostAsync(Post post)
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Sets the username property of the post to the current user name or uses testuser999999999 for tests.
        post.Username = user.Identity?.IsAuthenticated == true
            ? user.Identity.Name!
            : "testuser999999999"; // For unit tests assigns a default user/unauthenticated state

        _dbContext.Posts.Add(post);
        var saved = await _dbContext.SaveChangesAsync();
        return saved > 0;
    }
}