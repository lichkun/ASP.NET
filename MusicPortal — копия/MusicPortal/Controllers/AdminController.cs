using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly MusicPortalContext _context;
        private readonly IRepository _repo;


        public AdminController(MusicPortalContext context, IRepository rep)
        {
            _context = context;
            _repo = rep;
        }

        public IActionResult Index()
        {
            AdminViewModel avm = new AdminViewModel
            {
                Users = _context.Users.ToList(),
                Artists = _context.Artists.ToList(),
                Genres = _context.Genres.ToList(),
            };
           
            return View(avm);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.Status = 1; 
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddGenre(string genreName)
        {
            var genre = new Genre { Name = genreName };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddArtist(string artistName)
        {
            var artist = new Artist { Name = artistName };
            _context.Artists.Add(artist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteArtist(int id)
        {
            var artist = _context.Artists.Find(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditGenre(int genreId, string newName)
        {
            var genre = _context.Genres.Find(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = newName;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult EditArtist(int artistId, string newName)
        {
            var artist = _context.Artists.Find(artistId);
            if (artist != null)
            {
                artist.Name = newName;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}
