using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTopMovies.Models;
using RazorPagesMovies.Repositories;

namespace RazorPagesMovies.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Movie> _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CreateModel(IRepository<Movie> context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadedFile)
         {
            string path = "";
            if (uploadedFile != null)
             {
                path = "/img/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }

            Movie.Poster = path;
            await _context.Add(Movie);
            return RedirectToPage("./Index");
        }
    }
}
