using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bunit;
using BlazorProjectApp1.Data;
using BlazorProjectApp1.Components.Pages;

namespace BlazorProjectApp1.Tests
{
    /// <summary>
    /// Unit test for the View.razor component which verifies that a post can be viewed and the Play button triggers audio playback via JSInterop.
    /// Verifies that post can be retrieved from the database and that the audio data is correctly associated with the post.
    /// Triggers the ReadOcrTextAsync method to ensure that the audio data is correctly retrieved and played.
    /// Verified that database context is working correctly and that the audio data is correctly associated with the post.
    /// </summary>
    public class ViewTest : TestContext
    {
        [Fact]
        public void ViewPost_ShouldTriggerAudioPlay()
        {
            // Set up in memmory test database with EF Core isolated from real DB.
            var options = new DbContextOptionsBuilder<ProjectDBContext>().UseInMemoryDatabase(databaseName: $"TestDb_ViewPost_{Guid.NewGuid()}").Options;

            var context = new ProjectDBContext(options);
            context.Database.EnsureCreated();

            var testPostId = 999999999; // Using an extremely high number to avoid collision with seeded data.

            // Seed the post adding a post and related data to the test database. Ensuring real data is used for the test in View.razor.
            var post = new Post
            {
                PostId = testPostId,
                Title = "Test Post",
                Content = "Test post content",
                OcrImageText = "Test OCR text content"
            };
            context.Posts.Add(post);

            // Seed RawAudioData so ReadOcrTextAsync doesn't generate a new one.
            context.RawAudioData.Add(new RawAudioData
            {
                PostId = testPostId,
                AudioId = 1,
                AudioBinaryData = new byte[] { 1, 2, 3 } // Placeholder binary for audio test.
            });

            context.SaveChanges();

            // Inject the test database context to the component for testing.
            Services.AddSingleton(context);

            // Mock simulation of audioPlayer.play verifying if audio.Player.play is called.
            JSInterop.SetupVoid("audioPlayer.play", _ => true);
            JSInterop.SetupVoid("alert", _ => true); // For fallback errors.

            // Render view component
            var component = RenderComponent<View>(parameters => parameters
                .Add(viewComponent => viewComponent.PostId, testPostId));

            // Act trigger the Play button
            component.Find("button.btn-blue").Click();

            // Assert JS was called
            JSInterop.VerifyInvoke("audioPlayer.play");

            // Assert audio was found confirming audio data exists and has no empty binary data.
            var audio = context.RawAudioData.FirstOrDefault(audioRecord => audioRecord.PostId == testPostId);
            Assert.NotNull(audio);
            Assert.True(audio.AudioBinaryData?.Length > 0);
        }
    }
}