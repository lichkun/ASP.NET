using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Password { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }
    }
}
