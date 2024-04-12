using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Название жанра обязательно")]
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
