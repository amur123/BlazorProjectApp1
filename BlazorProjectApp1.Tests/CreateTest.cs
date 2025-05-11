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
    /// Unit test for the Create.razor component which tests the component with real services.
    /// The test verifies the Create.razor component when the input form is submitted adding it to the test database.
    /// Thus proving the components functionality and the database context is working correctly.
    /// </summary>
    public class CreateTest : TestContext
    {
        [Fact]
        public void CreatePost_ShouldAddPostToDatabase()
        {
            // Sets up the test databases
            var options = new DbContextOptionsBuilder<ProjectDBContext>().UseInMemoryDatabase("TestDb_CreatePost").Options;

            var dbContext = new ProjectDBContext(options);
            dbContext.Database.EnsureCreated();

            // Registers the database context before anything else
            Services.AddSingleton(dbContext);

            // Arrange sets up fake authentication so PostManager assigns a username
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("TestUser");
            authContext.SetRoles("User");

            // Registers PostManager with the fake authentication provider for the test and makes sure to get the test DbContext making Create.razor component available via [Inject] PostManager.
            Services.AddScoped<PostManager>(authServicesProvider => // Uses IServiceProvider to get the AuthenticationStateProvider to resolve authentication.
            {
                var authProvider = authServicesProvider.GetRequiredService<AuthenticationStateProvider>();
                return new PostManager(dbContext, authProvider);
            });

            // Act renders Create.razor and simulates user input and submit.
            var createComponent = RenderComponent<Create>();
            createComponent.Find("#postTitle").Change("Test Title");            // Sets Title field.
            createComponent.Find("#postContent").Change("Test Content");        // Sets Content field.
            createComponent.Find("Form").Submit();                              // Calls CreatePostAsync().

            // Assert verifies post was added to database
            var result = dbContext.Posts.FirstOrDefault(p => p.Title == "Test Title");
            Assert.NotNull(result);
            Assert.Equal("Test Content", result.Content);
            Assert.Equal("TestUser", result.Username); // Confirms username is stored.
        }
    }
}
