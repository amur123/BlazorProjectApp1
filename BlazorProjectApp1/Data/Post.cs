using System.ComponentModel.DataAnnotations;

namespace BlazorProjectApp1.Data;

/// <summary>
/// Defines the stricture of a post in the applications database.
/// Includes the properties for the post ID, title, content, image file, OCR text, audio data and username.
/// Associates image file and audio data with the post.
/// Username property is used to link the post to the user creating it.
/// </summary>
internal sealed class Post
{
    [Key]
    public int PostId { get; set; }

    [Required]
    [MaxLength(length: 200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(length: 5000)]
    public string Content { get; set; } = string.Empty;

    public string? ImageFile { get; set; }

    public string? OcrImageText { get; set; }

    public RawAudioData? AudioData { get; set; }

    public string Username { get; set; } = string.Empty; // Property for the username of the post author linking each post to the creating user.
}
