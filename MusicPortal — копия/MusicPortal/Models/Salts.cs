using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Salts
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Salt { get; set; }
    }
}
