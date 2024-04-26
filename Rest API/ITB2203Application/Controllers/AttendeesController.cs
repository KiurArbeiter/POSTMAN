using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ITB2203Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly DataContext _context;

        public AttendeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Attendee>> GetAttendees(string? name = null, string? email = null, int? daysbefore = null)
        {
            var query = _context.Attendees.AsQueryable();


            if (name != null)
                query = query.Where(x => x.Name != null && x.Name.ToUpper().Contains(name.ToUpper()));

            if (email != null)
                query = query.Where(x => x.Email != null && x.Email.ToUpper().Contains(email.ToUpper()));

            if (daysbefore != null)
            {
                query = query.Where(x=>(_context!.Events!.FirstOrDefault((e) => e.Id == x.EventId)!.Date!.Value - x.RegistrationTime).TotalDays > daysbefore);
            }
                
            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Attendee> GetAttendee(int id)
        {
            var attendee = _context.Attendees.Find(id);

            if (attendee == null)
            {
                return NotFound();
            }

            return Ok(attendee);
        }

        [HttpPut("{id}")]
        public IActionResult PutAttendee(int id, Attendee attendee)
        {
            if (id != attendee.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendee).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Attendees.Any(e => e.Id == id))
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
        public ActionResult<Attendee> PostAttendee(Attendee attendee)
        {

            var existingEvent = _context.Events.Any(e => e.Id == attendee.EventId);

            
            if (!existingEvent)
            {
                return NotFound("Event not found.");
            }

            
            if (string.IsNullOrEmpty(attendee.Email) || !attendee.Email.Contains("@"))
            {
                return BadRequest("Invalid email format.");
            }

            
            var existingAttendeeWithEmail = _context.Attendees.FirstOrDefault(a => a.Email == attendee.Email);
            if (existingAttendeeWithEmail != null)
            {
                return BadRequest("An attendee with the same email already exists.");
            }

            
            var speakerWithEmail = _context.Speakers.FirstOrDefault(s => s.Email == attendee.Email);
            if (speakerWithEmail != null)
            {
                return BadRequest("A speaker with the same email already exists for the event.");
            }

            var event1 = _context.Events.FirstOrDefault(x => x.Id == attendee.EventId);
            if (event1.Date < attendee.RegistrationTime)
            {
                return BadRequest();
            }

            


            _context.Attendees.Add(attendee);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAttendee), new { Id = attendee.Id }, attendee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttendee(int id)
        {
            var attendee = _context.Attendees.Find(id);
            if (attendee == null)
            {
                return NotFound();
            }

            _context.Attendees.Remove(attendee);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
