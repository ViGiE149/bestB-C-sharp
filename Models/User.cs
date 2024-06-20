using System.ComponentModel.DataAnnotations;

namespace bestbrigh.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username
        { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }  // e.g., 'Admin', 'Salesperson'

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


      
    }
}
