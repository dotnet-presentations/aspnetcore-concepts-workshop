using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendeeList
{
    [Route("/api/[controller]")]
    public class AttendeesController : Controller
    {
        private readonly WorkshopContext _context;
        public AttendeesController(WorkshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Attendee>> Get()
        {
            return _context.Attendees.ToListAsync();
        }
        
        [HttpGet("{id:int}")]
        public Task<Attendee> Get(int id)
        {
            return _context.Attendees.SingleOrDefaultAsync(a => a.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Attendee atendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Attendees.Add(atendee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = atendee.Id }, atendee);

        }
        
        [HttpDelete("{id:int}")]
        public Task Delete(int id)
        {
            var atendee = new Attendee
            {
                Id = id
            };
            _context.Attach(atendee);
            _context.Remove(atendee);
            return _context.SaveChangesAsync();
        }
    }
}
