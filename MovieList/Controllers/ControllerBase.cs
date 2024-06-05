using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieList.Data;
using MovieList.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.ApiControllers
{
    [Route("api/Movies")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly MovieListContext _context;

        public MoviesApiController(MovieListContext context)
        {
            _context = context;
        }

        // GET: api/MoviesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movie.ToListAsync();
        }

        // GET: api/MoviesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // POST: api/MoviesApi
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieDto movieDto)
        {
            if (movieDto == null || movieDto.Title == null || movieDto.ReleaseYear == null || movieDto.Genre == null)
                return BadRequest();

            _context.Movie.Add(new Movie {
                Title = movieDto.Title,
                ReleaseYear = movieDto.ReleaseYear,
                Genre = movieDto.Genre,
            });
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/MoviesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (movieDto == null || movieDto.Title == null || movieDto.ReleaseYear == null || movieDto.Genre == null)
                return BadRequest();

            var existingMovie = await _context.Movie.FindAsync(id);
            if (existingMovie == null)
            {
                _context.Movie.Add(new Movie {
                Title = movieDto.Title,
                ReleaseYear = movieDto.ReleaseYear,
                Genre = movieDto.Genre,
                });
                await _context.SaveChangesAsync();
                return Ok("Utworzono nowy film");
            }

            existingMovie.Title = movieDto.Title;
            existingMovie.ReleaseYear = movieDto.ReleaseYear;
            existingMovie.Genre = movieDto.Genre;

            await _context.SaveChangesAsync();

            return Ok("Zmieniono film");
        }

        // DELETE: api/MoviesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
