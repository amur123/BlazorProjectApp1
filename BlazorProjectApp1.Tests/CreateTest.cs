using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using BlazorProjectApp1.Data;
using BlazorProjectApp1.Components.Pages;


namespace BlazorProjectApp1.Tests
{
    /// <summary>
    /// Unit test for the Create.razor component which tests the component with real services.
    /// The test verifies the Create.razor component when the input form is submitted adding it to the test database.
    /// Thus proving the components functionality and the database context is working correctly.
    /// </summary>
    public class CreateTest : TestContext
    {
        /// <summary>
        /// Test to check if a post is added to the database when the form is submitted.
        /// </summary>
        [Fact]
        public void CreatePost_ShouldAddPostToDatabase()
        {
            // Sets up isolated database in memory with EF Core thus using this test DB for the test instead of real DB thus not making any changes during test.
            var options = new DbContextOptionsBuilder<ProjectDBContext>().UseInMemoryDatabase(databaseName: "TestDb_CreatePost").Options; // Sets up a temporary in memory database for testing.

            var context = new ProjectDBContext(options);
            context.Database.EnsureCreated();

            // Injestcs in-memory database into the test context making the test DB context available to the component during the test.
            Services.AddSingleton<ProjectDBContext>(context);

            // Arrange
            var createComponent = RenderComponent<Create>(); // Creates new instance of object to test simulating Create.razor in test browser.

            // Act
            createComponent.Find("#postTitle").Change("Test Title");            // Sets Title field.
            createComponent.Find("#postContent").Change("Test Content");        // Sets Content field.
            createComponent.Find("Form").Submit();                              // Calls CreatePostAsync().

            // Assert
            var result = context.Posts.FirstOrDefault(post => post.Title == "Test Title");  // Retrieves newly added post.
            Assert.NotNull(result);                                                   // Asserts the post exists.
            Assert.Equal("Test Content", result.Content);                             // Asserts the content matches.
        }
    }
}
