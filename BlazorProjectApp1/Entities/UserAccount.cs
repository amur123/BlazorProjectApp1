using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorProjectApp1.Entities
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Username")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Column("Password")]
        [MaxLength(100)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
