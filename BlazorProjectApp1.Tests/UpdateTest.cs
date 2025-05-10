using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using BlazorProjectApp1.Data;
using BlazorProjectApp1.Components.Pages;

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

            var context = new ProjectDBContext(options);
            context.Database.EnsureCreated();

            var post = new Post
            {
                PostId = 999999999, // High number to avoid any conflicts during testing.
                Title = "Original Title",
                Content = "Original Content"
            };
            context.Posts.Add(post);
            context.SaveChanges();

            Services.AddSingleton(context);

            // Act renders Update.razor with PostId.
            var component = RenderComponent<Update>(parameters => parameters.Add(updateComponent => updateComponent.PostId, 999999999));

            // Simulates user editing the fields.
            component.Find("#postTitle").Change("Updated Title");
            component.Find("#postContent").Change("Updated Content");

            // Submits the form.
            component.Find("form").Submit();

            // Assert fetches the post from DB and validate it was updated.
            var updated = context.Posts.First(post => post.PostId == 999999999);
            Assert.Equal("Updated Title", updated.Title);
            Assert.Equal("Updated Content", updated.Content);
        }
    }
}
