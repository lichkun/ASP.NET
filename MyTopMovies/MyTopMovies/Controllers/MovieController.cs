using Microsoft.AspNetCore.Mvc;
using MyTopMovies.Models;

namespace MyTopMovies.Controllers
{
    public class MovieController : Controller
    {
        MyTopMovieContext db;
        public MovieController(MyTopMovieContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Movies);
            ViewBag.Movies = movies;
            return View();
        }
    }
}
