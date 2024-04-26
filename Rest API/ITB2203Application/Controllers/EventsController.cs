using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITB2203Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents(string? name = null, string? location = null, int? speakerId = null)
        {
            IQueryable<Event> query = _context.Events;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.ToUpper().Contains(name.ToUpper()));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(x => x.Location.ToUpper().Contains(location.ToUpper()));

            if (speakerId != null)
                query = query.Where(x => x.SpeakerId == speakerId);

            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            var testEvent = _context.Events.Find(id);

            if (testEvent == null)
            {
                return NotFound();
            }

            return Ok(testEvent);
        }

        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event testEvent)
        {
            if (id != testEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(testEvent).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public ActionResult<Event> PostEvent(Event testEvent)
        {
            
            var existingSpeaker = _context.Speakers.Any(s => s.Id == testEvent.SpeakerId);

            
            if (!existingSpeaker)
            {
                return NotFound();
            }

            _context.Events.Add(testEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { id = testEvent.Id }, testEvent);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var testEvent = _context.Events.Find(id);

            if (testEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(testEvent);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
