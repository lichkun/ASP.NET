using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.Models;
using MusicPortal.Repository;

namespace MusicPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly IService<UserDTO> _userRepo;
        private readonly IService<GenreDTO> _genreRepo;
        private readonly IService<ArtistDTO> _artistRepo;

        public AdminController(IService<UserDTO> userRepo, IService<GenreDTO> genreRepo, IService<ArtistDTO> artistRepo)
        {
            _userRepo = userRepo;
            _genreRepo = genreRepo;
            _artistRepo = artistRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int userId)
        {
            if (await _userRepo.ChangeStatusAsync(userId, 1))
            {
                return RedirectToAction(nameof(ManageUsers));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (await _userRepo.DeleteAsync(userId))
            {
                return RedirectToAction(nameof(ManageUsers));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(string genreName)
        {
            var genre = new GenreDTO { Name = genreName };
            if (await _genreRepo.AddAsync(genre))
            {
                return RedirectToAction(nameof(ManageGenres));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            if (await _genreRepo.DeleteAsync(genreId))
            {
                return RedirectToAction(nameof(ManageGenres));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddArtist(string artistName)
        {
            var artist = new ArtistDTO { Name = artistName };
            if (await _artistRepo.AddAsync(artist))
            {
                return RedirectToAction(nameof(ManageArtists));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArtist(int artistId)
        {
            if (await _artistRepo.DeleteAsync(artistId))
            {
                return RedirectToAction(nameof(ManageArtists));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditGenre(int genreId, string newName)
        {
            var genre = await _genreRepo.GetByIdAsync(genreId);
            if (genre != null)
            {
                genre.Name = newName;
                if (await _genreRepo.UpdateAsync(genreId, genre))
                {
                    return RedirectToAction(nameof(ManageGenres));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditArtist(int artistId, string newName)
        {
            var artist = await _artistRepo.GetByIdAsync(artistId);
            if (artist != null)
            {
                artist.Name = newName;
                if (await _artistRepo.UpdateAsync(artistId, artist))
                {
                    return RedirectToAction(nameof(ManageArtists));
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userRepo.GetAllAsync();
            return View("Users", users);
        }

        public async Task<IActionResult> ManageGenres()
        {
            var genres = await _genreRepo.GetAllAsync();
            return View("Genre", genres);
        }

        public async Task<IActionResult> ManageArtists()
        {
            var artists = await _artistRepo.GetAllAsync();
            return View("Artist", artists);
        }
    }

}
