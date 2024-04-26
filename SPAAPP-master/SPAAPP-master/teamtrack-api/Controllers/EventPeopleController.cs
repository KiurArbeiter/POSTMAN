using System;
using Microsoft.AspNetCore.Mvc;
using teamtrack_api.Model;

namespace teamtrack_api.Controllers;

[ApiController] [Route("api/[controller]")] public class EventPeopleController : ControllerBase {
    private readonly DataContext context;
    public EventPeopleController(DataContext c)  {
        context = c;
    }
    [HttpGet] public IActionResult GetEvents() {
        return Ok(context.EventPeopleList);
    }   
    [HttpPost] public IActionResult Create([FromBody] EventPeople e) {
        var dbEvent = context.PeopleList?.Find(e.Id); 
        if (dbEvent == null) {
            context.EventPeopleList?.Add(e); 
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEvents), new { e.Id }, e);
        }
        return Conflict();
    }
    [HttpPut("{id}")] public IActionResult Update(int? id, [FromBody] EventPeople e) {
        if (id != e.Id || !context.EventPeopleList!.Any(e => e.Id == id)) return NotFound();
        context.Update(e);
        context.SaveChanges();
        return NoContent();
    }
    [HttpDelete("{id}")] public IActionResult Delete(int id) {
        var eventToDelete = context.EventPeopleList?.Find(id);
        if (eventToDelete == null) return NotFound();
        context.EventPeopleList?.Remove(eventToDelete);
        context.SaveChanges();
        return NoContent();
    }
}

