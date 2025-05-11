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
    /// Unit test for the View.razor component which verifies that a post can be viewed and the Play button triggers audio playback via JSInterop.
    /// Verifies that post can be retrieved from the database and that the audio data is correctly associated with the post.
    /// Triggers the ReadOcrTextAsync method to ensure that the audio data is correctly retrieved and played.
    /// Verified that database dbContext is working correctly and that the audio data is correctly associated with the post.
    /// </summary>
    public class ViewTest : TestContext
    {
        [Fact]
        public void ViewPost_ShouldTriggerAudioPlay()
        {
            // Set up in memmory test database with EF Core isolated from real DB.
            var options = new DbContextOptionsBuilder<ProjectDBContext>().UseInMemoryDatabase(databaseName: $"TestDb_ViewPost_{Guid.NewGuid()}").Options;

            var dbContext = new ProjectDBContext(options);
            dbContext.Database.EnsureCreated();

            var testPostId = 999999999; // Using an extremely high number to avoid collision with seeded data.

            // Seed the post adding a post and related data to the test database. Ensuring real data is used for the test in View.razor.
            dbContext.Posts.Add(new Post
            {
                PostId = testPostId,
                Title = "Test Post",
                Content = "Test Content",
                OcrImageText = "Test OCR Text Content",
                Username = "TestUser999999999"
            });

            // Seed RawAudioData so ReadOcrTextAsync doesn't generate a new one.
            dbContext.RawAudioData.Add(new RawAudioData
            {
                PostId = testPostId,
                AudioId = 1,
                AudioBinaryData = new byte[] { 1, 2, 3 } // Placeholder binary for audio test.
            });
            dbContext.SaveChanges();

            // Inject the test database dbContext to the component for testing.
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

            // Mock simulation of audioPlayer.play verifying if audio.Player.play is called.
            JSInterop.SetupVoid("audioPlayer.play", _ => true);
            JSInterop.SetupVoid("alert", _ => true); // For fallback errors.

            // Render view component
            var component = RenderComponent<View>(parameters => parameters.Add(viewComponent => viewComponent.PostId, testPostId));

            // Act trigger the Play button
            component.Find("button.btn-blue").Click();

            // Assert JS was called
            JSInterop.VerifyInvoke("audioPlayer.play");

            // Assert audio was found confirming audio data exists and has no empty binary data.
            var audio = dbContext.RawAudioData.FirstOrDefault(audioRecord => audioRecord.PostId == testPostId);
            Assert.NotNull(audio);
            Assert.True(audio.AudioBinaryData?.Length > 0);
        }
    }
}