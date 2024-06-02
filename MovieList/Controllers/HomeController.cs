using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieList.Data;
using MovieList.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieListContext _context;

        public HomeController(ILogger<HomeController> logger, MovieListContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return NotFound();
            }

            var userMovies = await _context.ListItem
                .Where(li => li.IdentityUserId == userId)
                .Select(li => li.MovieId)
                .ToListAsync();

            var movies = await _context.Movie
                .Select(m => new MovieViewModel
                {
                    Movie = m,
                    IsOnWatchlist = userMovies.Contains(m.Id)
                })
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int searchId))
                {
                    movies = movies.Where(s => s.Movie.Id == searchId).ToList();
                }
                else
                {
                    movies = movies.Where(s => s.Movie.Title.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
