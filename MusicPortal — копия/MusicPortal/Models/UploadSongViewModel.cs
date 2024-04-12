using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class UploadSongViewModel
    {
        public List<Artist> Artists { get; set; }
        public List<Genre> Genres { get; set; }
        public int? SelectedArtistId { get; set; }
        public int? SelectedGenreId { get; set; }
        [Required(ErrorMessage = "Введите название песни")]
        public string SongTitle { get; set; }
    }
}
