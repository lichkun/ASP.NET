using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTopMovies.Models;
using RazorPagesMovies.Repositories;

namespace RazorPagesMovies.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Movie> _context;

        public DetailsModel(IRepository<Movie> context)
        {
            _context = context;
        }

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
    }
}
