using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.Models;

namespace MusicPortal.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Genre> genres, int genre, string singer)
        {
            genres.Insert(0, new Genre { Name = "All", Id = 0 });
            Genres = new SelectList(genres, "Id", "Name", genre);
            SelectedGenre = genre;
            SelectedPosition = singer;
        }
        public SelectList Genres { get; } 
        public int SelectedGenre { get; } 
        public string SelectedPosition { get; } 
    }
}
