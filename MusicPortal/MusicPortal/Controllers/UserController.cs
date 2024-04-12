using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepositoryEntity<Artist> _artistRepo;
        private readonly IRepositoryEntity<Genre> _genreRepo;
        private readonly IRepositoryEntity<User> _userRepo;
        private readonly IRepositoryEntity<Song> _songRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserController(IRepositoryEntity<Artist> artistRepo, IRepositoryEntity<Genre> genreRepo,
                               IRepositoryEntity<User> userRepo, IRepositoryEntity<Song> songRepo,
                               IWebHostEnvironment appEnvironment)
        {
            _artistRepo = artistRepo;
            _genreRepo = genreRepo;
            _userRepo = userRepo;
            _songRepo = songRepo;
            _hostingEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Upload()
        {
            var viewModel = new UploadSongViewModel
            {
                Artists = await _artistRepo.GetAllAsync(),
                Genres = await _genreRepo.GetAllAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadSongViewModel viewModel, IFormFile file)
        {

                if (viewModel.SelectedArtistId == null || viewModel.SelectedGenreId == null || file == null)
                {
                    ModelState.AddModelError("", "Выберите автора, жанр и загрузите файл");
                    viewModel.Artists = await _artistRepo.GetAllAsync();
                    viewModel.Genres = await _genreRepo.GetAllAsync();
                    return View(viewModel);
                }

                var artist = await _artistRepo.GetByIdAsync(viewModel.SelectedArtistId.Value);
                var genre = await _genreRepo.GetByIdAsync(viewModel.SelectedGenreId.Value);

                if (artist == null || genre == null)
                {
                    ModelState.AddModelError("", "Автор или жанр не найден в базе данных");
                    viewModel.Artists = await _artistRepo.GetAllAsync();
                    viewModel.Genres = await _genreRepo.GetAllAsync();
                return View(viewModel);
                }

                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "files", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                int? curId = HttpContext.Session.GetInt32("Id");
                var curUser = await _userRepo.GetByIdAsync(curId.Value);

                var song = new Song
                {
                    Title = viewModel.SongTitle,
                    FilePath = filePath,
                    Artist = artist,
                    Genre = genre,
                    User = curUser
                };

                await _songRepo.AddAsync(song);

            return RedirectToAction("Upload");
        }

        public async Task<IActionResult> Index()
        {
            var songs = await _songRepo.GetAllAsync();
            return View(songs);
        }

        public async Task<IActionResult> Download(int id)
        {
            var song = await _songRepo.GetByIdAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            var filePath = song.FilePath;
            var fileName = Path.GetFileName(filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "audio/mpeg", fileName);
        }

       
    }



}
