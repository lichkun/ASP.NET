using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTopMovies.Models;
using RazorPagesMovies.Repositories;
using System.IO;

namespace RazorPagesMovies.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Movie> _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public EditModel(IRepository<Movie> context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.GetById(id.Value);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync( IFormFile uploadedFile)
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
            await _context.Update(Movie.Id,Movie);

            return RedirectToPage("./Index");
        }

       
    }
}
