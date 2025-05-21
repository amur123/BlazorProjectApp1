using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlazorProjectApp1.Data;

namespace BlazorProjectApp1.Entities
{
    /// <summary>
    /// Defines the structure of a user account in the application database.
    /// Represents a user with properties for ID, username, password and role with validation attributes.
    /// Navigation property for the posts created by the user.
    /// Database context will use this class to create the UserAccount table.
    /// </summary>
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("Username")]
        [MaxLength(100)]
        [Required]
        public string Username { get; set; }

        [Column("Password")]
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        internal ICollection<Post> Posts { get; set; } = new List<Post>(); // Navigation property for the posts created by the user posts are associated with a user account.
    }
}
