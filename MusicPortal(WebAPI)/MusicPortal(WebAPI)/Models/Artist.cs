using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Artist
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Name { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
