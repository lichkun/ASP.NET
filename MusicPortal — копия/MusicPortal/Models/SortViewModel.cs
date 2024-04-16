namespace MusicPortal.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; } 
        public SortState GenreSort { get; set; }    
        public SortState ArtistSort { get; set; }    
        public SortState Current { get; set; }    

        public SortViewModel(SortState sortOrder)
        {
            NameSort = SortState.NameAsc;
            GenreSort = SortState.GenreAsc;
            ArtistSort = SortState.ArtistAsc;

            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            ArtistSort = sortOrder == SortState.ArtistAsc ? SortState.ArtistDesc : SortState.ArtistAsc;
            Current = sortOrder;
        }
    }
}
