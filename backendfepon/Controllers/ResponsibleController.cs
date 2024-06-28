using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.ResponsibleDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResponsibleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Responsible
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsibleDTO>>> GetResponsibles()
        {

            var events = await _context.Responsibles
            .Include(p => p.Event)
            .Include(p => p.AdministrativeMember)
           .Select(p => new ResponsibleDTO
           {
               Responsible_Id = p.Responsible_Id,
               AdministrativeMember_Name = p.AdministrativeMember.Student.First_Name,
               Event_Name = p.Event.Title,
               Event_Role = p.Event_Role
           })
           .ToListAsync();

            return Ok(events);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Responsible>> GetResponsible(int id)
        {
            var responsible = await _context.Responsibles.FindAsync(id);

            if (responsible == null)
            {
                return NotFound();
            }

            return responsible;
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Responsible>> PostResponsible(CreateUpdateResponsibleDTO responsibleDTO)
        {
            var responsible = new Responsible
            {
                AdministrativeMember_Id = responsibleDTO.AdministrativeMember_Id,
                Event_Id = responsibleDTO.Responsible_Id,
                Event_Role = responsibleDTO.Event_Role
            };
            _context.Responsibles.Add(responsible);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetResponsible), new { id = responsible.Event_Id }, responsible);
        }


        // PUT: api/Responsible/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsible(int id, CreateUpdateResponsibleDTO updatedResponsible)
        {


            var responsible = await _context.Responsibles.FindAsync(id);

            if (responsible == null)
            {
                return BadRequest();
            }

            responsible.AdministrativeMember_Id = updatedResponsible.AdministrativeMember_Id;
            responsible.Event_Id = updatedResponsible.Event_Id;
            responsible.Event_Role = updatedResponsible.Event_Role;


            _context.Entry(responsible).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsibleExists(id))
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


        // DELETE: api/Responsible/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsible(int id)
        {
            var responsible = await _context.Responsibles.FindAsync(id);
            if (responsible == null)
            {
                return NotFound();
            }

            _context.Responsibles.Remove(responsible);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ResponsibleExists(int id)
        {
            return _context.Responsibles.Any(e => e.Responsible_Id == id);
        }
    }
}
