using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using BlazorProjectApp1.Data;
using BlazorProjectApp1.Components.Pages;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorProjectApp1.Tests
{
    /// <summary>
    /// Unit test for Update.razor component to ensure a post is updated successfully.
    /// Confirms that post is correctly loaded.
    /// Submitting the form updates the post in the database.
    /// Uses test database in memort to simulate and isolate during tests
    /// Micmicks real database interactions verifying Update.razor component work with EF Core.
    /// Confirms writing between the component and the database is working correctly.
    /// </summary>
    public class UpdateTest : TestContext
    {
        [Fact]
        public void UpdatePost_ShouldModifyPostInDatabase()
        {
            // Arrange Setup in memory test DB.
            var options = new DbContextOptionsBuilder<ProjectDBContext>().UseInMemoryDatabase($"TestDb_UpdatePost_{Guid.NewGuid()}").Options;

            var dbContext = new ProjectDBContext(options);
            dbContext.Database.EnsureCreated();

            dbContext.Posts.Add(new Post
            {
                PostId = 999999999, // High number to avoid any conflicts during testing.
                Title = "Original Title",
                Content = "Original Content",
                Username = "TestUser999999999" // For unit tests assigns a default user/unauthenticated state
            });
            dbContext.SaveChanges();

            Services.AddSingleton<ProjectDBContext>(dbContext);

            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("TestUser999999999");
            authContext.SetRoles("User");

            // Registers PostManager with the fake authentication provider for the test and makes sure to get the test DbContext making Create.razor component available via [Inject] PostManager.
            Services.AddScoped<PostManager>(authServicesProvider => // Uses IServiceProvider to get the AuthenticationStateProvider to resolve authentication.
            {
                var authProvider = authServicesProvider.GetRequiredService<AuthenticationStateProvider>();
                return new PostManager(dbContext, authProvider);
            });

            // Act renders Update.razor with PostId.
            var component = RenderComponent<Update>(parameters => parameters.Add(updateComponent => updateComponent.PostId, 999999999));

            // Simulates user editing the fields.
            component.Find("#postTitle").Change("Updated Title");
            component.Find("#postContent").Change("Updated Content");
            component.Find("form").Submit();

            // Assert fetches the post from DB and validate it was updated.
            var updated = dbContext.Posts.First(post => post.PostId == 999999999);
            Assert.Equal("Updated Title", updated.Title);
            Assert.Equal("Updated Content", updated.Content);
        }
    }
}
