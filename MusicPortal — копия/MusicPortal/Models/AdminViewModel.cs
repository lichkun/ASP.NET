namespace MusicPortal.Models
{
    public class AdminViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
    }

}
