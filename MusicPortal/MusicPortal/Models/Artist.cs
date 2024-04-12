using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя исполнителя обязательно")]
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
