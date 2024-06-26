using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieList.Data;
using MovieList.Models;

namespace MovieList.Controllers
{
    [Authorize (Roles = "User")]
    public class ListController : Controller
    {
        private readonly MovieListContext _context;

        public ListController(MovieListContext context)
        {
            _context = context;
        }

        // GET: List
        
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }
            var query = _context.ListItem.Where(m => m.IdentityUserId == userId);
            var list = query.Select(m => new ListIndexViewModel
            {
                Id = m.Id,
                Title = m.Movie.Title,
                ReleaseYear = m.Movie.ReleaseYear,
                Genre = m.Movie.Genre,
                Note = m.Note
            });
            
            return View(await list.ToListAsync());
        }

        // GET: List/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // GET: List/Create
        
        public IActionResult Create(int id)
        {
            ViewData["MovieId"] = id;
            ViewData["MovieTitle"] = _context.Movie.Find(id)?.Title;
            return View();
        }

        // POST: List/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("MovieId, Note")] ListItem listItem)
        {
            ModelState.Clear();
            
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return NotFound();
            }
            listItem.IdentityUserId = userId;
            
            listItem.IdentityUser = (await _context.Users.FindAsync(userId))!;
            
            Movie? movie = _context.Movie.Find(listItem.MovieId);
            if(movie == null)
            {
                return NotFound();
            }
            listItem.Movie = movie;
            
            if (TryValidateModel(listItem))
            {
                _context.Add(listItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listItem);
        }

        // GET: List/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItem.FindAsync(id);
            if (listItem == null)
            {
                return NotFound();
            }
            return View(listItem);
        }

        // POST: List/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,IdentityUserId,Note")] ListItem listItem)
        {
            ModelState.Clear();
            
            if (id != listItem.Id)
            {
                return NotFound();
            }
            
            listItem.IdentityUser = (await _context.Users.FindAsync(listItem.IdentityUserId))!;
            
            Movie? movie = _context.Movie.Find(listItem.MovieId);
            if(movie == null)
            {
                return NotFound();
            }
            listItem.Movie = movie;
            
            if (TryValidateModel(listItem))
            {
                try
                {
                    _context.Update(listItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListItemExists(listItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(listItem);
        }

        // GET: List/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // POST: List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listItem = await _context.ListItem.FindAsync(id);
            if (listItem != null)
            {
                _context.ListItem.Remove(listItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListItemExists(int id)
        {
            return _context.ListItem.Any(e => e.Id == id);
        }
    }
}
