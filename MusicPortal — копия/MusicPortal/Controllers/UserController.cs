using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly MusicPortalContext _context;
        IWebHostEnvironment _hostingEnvironment;

        public UserController(MusicPortalContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _hostingEnvironment = appEnvironment;
        }
        public IActionResult Upload()
        {
            var viewModel = new UploadSongViewModel
            {
                Artists = _context.Artists.ToList(),
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadSongViewModel viewModel, IFormFile file)
        {
            if (viewModel.SelectedArtistId == null || viewModel.SelectedGenreId == null || file == null)
            {
                ModelState.AddModelError("", "Выберите автора, жанр и загрузите файл");
                return View(viewModel);
            }

            var artist = await _context.Artists.FindAsync(viewModel.SelectedArtistId);
            var genre = await _context.Genres.FindAsync(viewModel.SelectedGenreId);

            if (artist == null || genre == null)
            {
                ModelState.AddModelError("", "Автор или жанр не найден в базе данных");
                return View(viewModel);
            }

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "files", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            int? curId = HttpContext.Session.GetInt32("Id");
            var curUser = _context.Users.Find(curId)!;

            var song = new Song
            {
                Title = viewModel.SongTitle,
                FilePath = filePath, 
                Artist = artist,
                Genre = genre,
                User = curUser
            };


            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return RedirectToAction("Upload"); 
        }

        public IActionResult Index()
        {
            return View(_context.Songs.ToList());
        }
    }
}
