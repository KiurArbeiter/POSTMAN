using System;
using System.Collections.Generic; using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace WorkoutApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Exercises Controller: ControllerBase
    {
        private readonly DataContext _context;
        public ExercisesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetExercises([FromQuery] ExerciseIntensity? intensity)
        {
            if(intensity.HasValue)
            {
                return Ok(_context.ExerciseList?.Where(x => x.Intensity == intensity.Value));
            }
            return Ok(_context.ExerciseList);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int? id)
        {
            var exercise = _context.ExerciseList?.FirstOrDefault(e => e.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Exercise exercise)
        {
            var dbexercise = _context.ExerciseList?.Find(exercise.Id);
            if (dbexercise == null)
            {
                _context.Add(exercise);
                 _context.SaveChanges();

                 return CreatedAtAction(nameof(GetDetails), new { Id = exercise.Id}, exercise);

            }
            else
            {
                return Conflict();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] Exercise exercise)
        {
            if (id != exercise.Id || !_context.ExerciseList!.Any(e => e.Id == id))
            {
                return NotFound();
            }

            _context.Update(exercise);
            _context.SaveChanges();

            return NoContent();
        }


    }
}