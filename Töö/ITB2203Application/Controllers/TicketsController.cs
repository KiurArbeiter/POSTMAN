using ITB2203Application.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ITB2203Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly DataContext _context;

        public TicketsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets(string? seatno = null)
        {
            var query = _context.Tickets.AsQueryable();


            if (seatno != null)
                query = query.Where(x => x.SeatNo != null && x.SeatNo.ToUpper().Contains(seatno.ToUpper()));


            return query.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult GetTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public IActionResult PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tickets.Any(e => e.Id == id))
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
        public ActionResult PostTicket(Ticket ticket)
        {
            var existingSession = _context.Sessions.Any(s => s.Id == ticket.SessionId);
            if (!existingSession)
            {
                return BadRequest("Session not found.");
            }

            var seatExists = _context.Tickets.Any(t => t.SessionId == ticket.SessionId && t.SeatNo == ticket.SeatNo);
            if (seatExists)
            {
                return BadRequest("Seat number must be unique within the session.");
            }

            if (ticket.Price <= 0)
            {
                return BadRequest("Ticket price must be a positive number.");
            }

            _context.Tickets.Add(ticket);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to save the ticket.");
            }

            return CreatedAtAction(nameof(GetTicket), new { Id = ticket.Id }, ticket);
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteAttendee(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return NoContent();
        }
    }
}