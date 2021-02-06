using LearnApiUsingMiddleware.ActionFilters;
using LearnApiUsingMiddleware.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LearnApiUsingMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = _context.Movies.ToList();
            return Ok(movies);
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "MovieById")]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Movie>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult GetMovie(Guid id)
        {   
            var dbMovie = HttpContext.Items["entity"] as Movie;
            return Ok(dbMovie);

            /*var movie = _context.Movies.SingleOrDefault(x => x.Id.Equals(id));

            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(movie);
            }*/
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Movie>))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Put(Guid id,[FromBody] Movie movie)
        {
            /*if (id != movie.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
*/
            /*var dbMovie = _context.Movies.SingleOrDefault(x => x.Id.Equals(id));
            
            if (dbMovie == null)
            {
                return NotFound();
            }*/

            var dbMovie = HttpContext.Items["entity"] as Movie;

            // dbMovie.Map(movie);

            _context.Movies.Update(dbMovie);
            _context.SaveChanges();

            return Ok(dbMovie);
        }



        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Post([FromBody] Movie movie)
        {
            /*if (movie != null)
             {
                 return BadRequest("Movie Object is null");
             }


             

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtAction("MovieById", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Movie>))]
        public IActionResult DeleteMovie(Guid id)
        {
            var movie = _context.Movies.SingleOrDefault(x => x.Id.Equals(id));
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
