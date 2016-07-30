using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendeeList.Controllers
{
    [Route("/")]
    public class AttendeesController : Controller
    {
        private readonly WorkshopContext _context;

        public AttendeesController(WorkshopContext context)
        {
            _context = context;    
        }

        // GET: /
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attendees.ToListAsync());
        }

        // GET: Attendees/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id);
            if (attendee == null)
            {
                return NotFound();
            }

            return View(attendee);
        }

        [HttpGet("{id:int}/vcard")]
        [Produces("text/vcard")]
        public async Task<IActionResult> VCard(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendee = await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id);
            
            if (attendee == null)
            {
                return NotFound();
            }

            return Ok(attendee);
        }

        // GET: Attendees/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attendees/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(attendee);
        }

        // GET: Attendees/Edit/5
        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            return ViewOrNotFound(await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id));
        }

        // POST: Attendees/Edit/5
        [HttpPost("{id:int}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Attendee attendee)
        {
            if (id != attendee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendee);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(attendee);
        }

        // GET: Attendees/Delete/5
        [HttpGet("{id:int}/delete")]
        public Task<IActionResult> Delete(int id) => Edit(id);

        // POST: Attendees/Delete/5
        [HttpPost("{id:int}/delete"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendee = await _context.Attendees.SingleOrDefaultAsync(m => m.Id == id);

            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private IActionResult ViewOrNotFound(object model)
        {
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendees.Any(e => e.Id == id);
        }
    }
}
