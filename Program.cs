using BlazorProjectApp1.Components;
using BlazorProjectApp1.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/accessdenied";
    }
    );
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddDbContext<ProjectDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Retrieves most recent audio data via AudioId  with associated post. Allowing for streaming of audio directly from the database.
app.MapGet("/audio/{postId:int}", async (int postId, ProjectDBContext dbContext, HttpContext httpAudioContext) =>
{   
    // Queries the database for most recent RawAudioData for the relevant PostId and if multiple exist the highest AudioId is treated as most recent.
    var audioData = await dbContext.RawAudioData
    .Where(rawAudioData => rawAudioData.PostId == postId)
    .OrderByDescending(rawAudioData => rawAudioData.AudioId)
    .FirstOrDefaultAsync();

    // If no audio data is found for PostId then responds with 404.
    if (audioData is null)
    {
        return Results.NotFound();
    }
    httpAudioContext.Response.ContentType = "audio/mpeg";   // Sets the response context to audio/mpeg letting browser know it's an mp3 stream. Modern browsers recognise mpeg as mp3.
    await httpAudioContext.Response.Body.WriteAsync(audioData.AudioBinaryData);     // Writes AudioBinaryData directry to stream from database without saving to local disk.
    return Results.Ok();    // Gives an OK response upon stream completion.
});

app.Run();
