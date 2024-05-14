using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyTopMovies.Models;
using RazorPagesMovies.Repositories;

namespace RazorPagesMovies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Movie> _context;

        public IndexModel(IRepository<Movie> context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.GetAll();
        }
    }
}
