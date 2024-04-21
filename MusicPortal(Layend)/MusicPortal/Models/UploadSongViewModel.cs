using MusicPortal.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class UploadSongViewModel
    {
        public List<ArtistDTO> Artists { get; set; }
        public List<GenreDTO> Genres { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выберите автора")]
        public int? SelectedArtistId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, выберите жанр")]
        public int? SelectedGenreId { get; set; }
        [Required(ErrorMessage = "Введите название песни")]
        public string SongTitle { get; set; }
    }
}
