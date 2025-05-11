using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using Bunit.TestDoubles;
using BlazorProjectApp1.Data;
using BlazorProjectApp1.Components.Pages;

namespace BlazorProjectApp1.Tests
{
    /// <summary>
    /// Unit test for Home.razor component to ensure a post is deleted successfully verifying database logic in Home.razor with post deletion.
    /// The test verifies that clicking the delete button removes the post from the test database.
    /// Utilises a fake auntheticated Administrator user to show the delete button.
    /// Crteates an isolated in-memory EF Core database for testing using a very high number for the PostId to avoid any conflicts with other tests.
    /// Renders the Home component and clicks the delete button.
    /// Waiting for the post to be removed from the rendered table.
    /// Confirms that the post is deleted from the database using EF Core.
    /// </summary>
    public class DeleteTest : TestContext
    {
        [Fact]
        public void DeletePost_ShouldRemovePostFromDatabase()
        {
            // Arrange sets up a fake authenticated user so <AuthorizeView> shows the delete button.
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("TestUser");
            authContext.SetRoles("Administrator");

            // Arrange Set up an isolated in memory EF Core database for testing.
            var options = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase($"TestDb_DeletePost_{Guid.NewGuid()}")
                .Options;
            var dbContext = new ProjectDBContext(options);
            dbContext.Database.EnsureCreated();

            const int testPostId = 999999999; // High number to avoid any conflicts during testing seeded during test which will be deleted.
            dbContext.Posts.Add(new Post
            {
                PostId = testPostId,
                Title = "Post to be delete",
                Content = "Content to be deleted"
            });
            dbContext.SaveChanges();  // Persists the changes to the in-memory database.

            // Register the test DbContext so the component uses this in-memory database.
            Services.AddSingleton(dbContext);

            // Setups up JSInterop so that confirm returns true and alert is not operational thus not returning exception and immediately runs test.
            JSInterop.Setup<bool>("confirm", _ => true).SetResult(true);
            JSInterop.SetupVoid("alert", _ => true);

            // Registers the PostManager service to be used in the component for the test.
            Services.AddScoped<PostManager>();
            // Act renders Home and clicks the delete button.
            var component = RenderComponent<Home>();
            // Finds all delete buttons and deletes the last one 999999999.
            component.FindAll("button.btn-pink").Last().Click();

            // Wait until the row for our post is removed from the rendered table.
            component.WaitForAssertion(() =>
            {
                var allTableRows = component.FindAll("tbody tr");   // By calling "tbody tr" we get all the rows in the table due to every post being in a <tr> element inside <tbody> in Razor.
                Assert.DoesNotContain(allTableRows, tableRow => tableRow.InnerHtml.Contains("This post will be deleted"));
            }, timeout: TimeSpan.FromSeconds(2));

            // Asserts verifies with EF Core that the post has been removed.
            var deleted = dbContext.Posts.AsNoTracking().FirstOrDefault(dbpost => dbpost.PostId == testPostId);
            Assert.Null(deleted);
        }
    }
}
