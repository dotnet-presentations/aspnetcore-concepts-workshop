using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendeeList
{
    [Route("/api/attendees")]
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
        public async Task<IActionResult> Get(int id)
        {
            return OkOrNotFound(await _context.Attendees.SingleOrDefaultAsync(a => a.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Attendee attendee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = attendee.Id }, attendee);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody]Attendee attendee)
        {
            if (attendee == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Update(attendee);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(attendee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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

        private IActionResult OkOrNotFound(object result)
        {
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
