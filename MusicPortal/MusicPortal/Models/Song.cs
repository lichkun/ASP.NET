using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Название песни обязательно")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Путь к файлу обязателен")]
        public string FilePath { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual User User { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
