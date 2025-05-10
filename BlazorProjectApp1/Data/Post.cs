using System.ComponentModel.DataAnnotations;

namespace BlazorProjectApp1.Data;

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
}
