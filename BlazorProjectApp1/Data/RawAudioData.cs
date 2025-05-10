using System.ComponentModel.DataAnnotations;

namespace BlazorProjectApp1.Data;

internal class RawAudioData
{
    [Key]
    public int PostId { get; set; } // ID which is linked to a post.

    public int AudioId { get; set; }

    [Required]
    public byte[] AudioBinaryData { get; set; } = Array.Empty<byte>(); // Property for RAW audio data.

    public Post RelevantPost { get; set; } = null!; // Property for navigating to the related post.
}