using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieList.Data;
using MovieList.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
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
