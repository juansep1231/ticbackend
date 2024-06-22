using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents()
        {

            var events = await _context.Events
            .Include(p => p.State)
           .Select(p => new EventDTO
           {
               Event_Id = p.Event_Id,
               State_Name = p.State.State_Name,
               Title = p.Title,
               Description = p.Description,
               Start_Date = p.Start_Date,
               End_Date = p.End_Date,
               Budget = p.Budget,
               Status = p.Status,
               Budget_Status = p.Budget_Status,
               Event_Location = p.Event_Location,
               Hiring = p.Hiring
           })
           .ToListAsync();

            return Ok(events);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var singleEvent = await _context.Events.FindAsync(id);

            if (singleEvent == null)
            {
                return NotFound();
            }

            return singleEvent;
        }


        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(CreateUpdateEventDTO eventDTO)
        {
            var newEvent = new Event
            {
                State_Id = eventDTO.State_Id,
                Title = eventDTO.Title,
                Description = eventDTO.Description,
                Start_Date = eventDTO.Start_Date,
                End_Date= eventDTO.End_Date,
                Budget = eventDTO.Budget,
                Status = eventDTO.Status,
                Budget_Status= eventDTO.Budget_Status,
                Event_Location= eventDTO.Event_Location,
                Hiring = eventDTO.Hiring



            };
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Event_Id },newEvent);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, CreateUpdateEventDTO updatedEvent)
        {


            var oldEvent = await _context.Events.FindAsync(id);

            if (oldEvent == null)
            {
                return BadRequest();
            }

            oldEvent.State_Id = updatedEvent.State_Id;
            oldEvent.Title = updatedEvent.Title;
            oldEvent.Description = updatedEvent.Description;
            oldEvent.Start_Date = updatedEvent.Start_Date;
            oldEvent.End_Date = updatedEvent.End_Date;
            oldEvent.Budget = updatedEvent.Budget;
            oldEvent.Status = updatedEvent.Status;
            oldEvent.Budget_Status = updatedEvent.Budget_Status;
            oldEvent.Event_Location = updatedEvent.Event_Location;
            oldEvent.Hiring = updatedEvent.Hiring;


            _context.Entry(oldEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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


        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var oldEvent = await _context.Events.FindAsync(id);
            if (oldEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(oldEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Event_Id == id);
        }

    }


}
