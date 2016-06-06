using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendeeList
{
    [Route("/api/[controller]")]
    public class AttendeesApiController : Controller
    {
        private readonly WorkshopContext _context;

        public AttendeesApiController(WorkshopContext context)
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
        public async Task<IActionResult> Post([FromBody]Attendee attendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = attendee.Id }, attendee);
        }
        
        [HttpDelete("{id:int}")]
        public Task Delete(int id)
        {
            var attendee = new Attendee
            {
                Id = id
            };
            _context.Attach(attendee);
            _context.Remove(attendee);
            return _context.SaveChangesAsync();
        }
    }
}
