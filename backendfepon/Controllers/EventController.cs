using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.DTOs.EventDTOs;
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
        private readonly IMapper _mapper;

        public EventController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<ActionResult<EventDTO>> GetEvent(int id)
        {
            var singleEvent = await _context.Events
             .Include(p => p.State)
             .Where(p => p.State_Id == id)
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
            .FirstOrDefaultAsync();

            if (singleEvent == null)
            {
                return NotFound();
            }

            return Ok(singleEvent);
        }


        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<EventDTO>> PostEvent(CreateUpdateEventDTO eventDTO)
        {
            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == eventDTO.State_Name);
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Map the DTO to the entity
            var newEvent = _mapper.Map<Event>(eventDTO);
            newEvent.State_Id = state.State_Id;

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            var createdEventDTO = _mapper.Map<EventDTO>(newEvent);

            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Event_Id }, createdEventDTO);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, CreateUpdateEventDTO updatedEvent)
        {
            var oldEvent = await _context.Events.FindAsync(id);

            if (oldEvent == null)
            {
                return BadRequest("Invalid Event ID.");
            }

            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == updatedEvent.State_Name);
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Map the updated properties to the existing event
            _mapper.Map(updatedEvent, oldEvent);
            oldEvent.State_Id = state.State_Id; // Set the State_Id manually

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
