using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле 'FilePath' обязательно для заполнения.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле 'FilePath' обязательно для заполнения.")]
        public string FilePath { get; set; }
        public string Genre { get; set; }
        public string User { get; set; }
        public string Artist { get; set; }
        public int GenreId { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
    }
}
