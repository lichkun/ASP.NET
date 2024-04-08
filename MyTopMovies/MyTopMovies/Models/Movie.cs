using MyTopMovies.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyTopMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the title")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the director")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Director must be between 1 and 50 characters")]
        public string Director { get; set; }

        [Range(1900, 3000, ErrorMessage = "Please enter a valid year between 1900 and 3000")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please upload a poster")]
        public string Poster { get; set; }

        [Required(ErrorMessage = "Please enter at least one genre")]
        [GenresValidation('/', "Action", "Adventure", "Comedy", "Drama", "Fantasy", "Horror", "Science Fiction")]
        public string Genres { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; }
    }
}
