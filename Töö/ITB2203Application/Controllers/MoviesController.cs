using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly DataContext _context;

    public MoviesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetMovies(string? title = null)
    {
        var query = _context.Movies!.AsQueryable();

        if (title != null)
            query = query.Where(x => x.Title != null && x.Title.ToUpper().Contains(title.ToUpper()));

        return query.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult GetMovie(int id)
    {
        var speaker = _context.Movies!.Find(id);

        if (speaker == null)
        {
            return NotFound();
        }

        return Ok(speaker);
    }

    [HttpPut("{id}")]
    public IActionResult PutMovie(int id, Movie movie)
    {
        var dbMovie = _context.Movies!.AsNoTracking().FirstOrDefault(x => x.Id == movie.Id);
        if (id != movie.Id || dbMovie == null)
        {
            return NotFound();
        }

        _context.Update(movie);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    public ActionResult PostMovie(Movie movie)
    {

        var dbExercise = _context.Movies!.Find(movie.Id);
        if (dbExercise == null)
        {
            _context.Add(movie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMovie), new { Id = movie.Id }, movie);
        }
        else
        {
            return Conflict();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.Movies!.Find(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Remove(movie);
        _context.SaveChanges();

        return NoContent();
    }
}