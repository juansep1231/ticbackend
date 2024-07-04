using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.EventExpenseDTO;
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
        private readonly IMapper _mapper;

        public ResponsibleController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Responsible
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsibleDTO>>> GetResponsibles()
        {

            var responsibles = await _context.Responsibles
            .Include(p => p.Event)
            .Include(p => p.AdministrativeMember)
           .Select(p => new ResponsibleDTO
           {
               Responsible_Id = p.Responsible_Id,
               AdministrativeMember_Name = p.AdministrativeMember.Student.First_Name + ' ' + p.AdministrativeMember.Student.Last_Name,
               Event_Name = p.Event.Title,
               Event_Role = p.Event_Role
           })
           .ToListAsync();

            return Ok(responsibles);
        }

        // GET: api/Responsible/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponsibleDTO>> GetResponsible(int id)
        {
            var responsible = await _context.Responsibles
              .Include(p => p.Event)
            .Include(p => p.AdministrativeMember)
             .Where(p => p.Responsible_Id == id)
            .Select(p => new ResponsibleDTO
            {
                Responsible_Id = p.Responsible_Id,
                AdministrativeMember_Name = p.AdministrativeMember.Student.First_Name + ' ' + p.AdministrativeMember.Student.Last_Name,
                Event_Name = p.Event.Title,
                Event_Role = p.Event_Role
            })
            .FirstOrDefaultAsync();

            if (responsible == null)
            {
                return NotFound();
            }

            return Ok(responsible);
        }

        // POST: api/Responsible
        [HttpPost]
        public async Task<ActionResult<ResponsibleDTO>> PostResponsible(CreateUpdateResponsibleDTO responsibleDTO)
        {
            // Find the Administrative Member ID based on the email
            var administrativeMember = await _context.AdministrativeMembers.FirstOrDefaultAsync(am => am.Student.Email == responsibleDTO.AdministrativeMember_Email);
            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member email.");
            }

            // Find the Event ID based on the ID
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == responsibleDTO.Event_Name);
            if (eventEntity == null)
            {
                return BadRequest("Invalid Event ID.");
            }

            // Map the DTO to the entity
            var responsible = _mapper.Map<Responsible>(responsibleDTO);
            responsible.Administrative_Member_Id = administrativeMember.Administrative_Member_Id;
            responsible.Event_Id = eventEntity.Event_Id;

            _context.Responsibles.Add(responsible);
            await _context.SaveChangesAsync();

            var createdResponsibleDTO = _mapper.Map<ResponsibleDTO>(responsible);

            return CreatedAtAction(nameof(GetResponsible), new { id = responsible.Responsible_Id }, createdResponsibleDTO);
        }

        // PUT: api/Responsible/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponsible(int id, CreateUpdateResponsibleDTO updatedResponsible)
        {
            var responsible = await _context.Responsibles.FindAsync(id);

            if (responsible == null)
            {
                return BadRequest("Invalid Responsible ID.");
            }

            // Find the Administrative Member ID based on the email
            var administrativeMember = await _context.AdministrativeMembers.FirstOrDefaultAsync(am => am.Student.Email == updatedResponsible.AdministrativeMember_Email);
            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member email.");
            }

            // Find the Event ID based on the ID
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == updatedResponsible.Event_Name);
            if (eventEntity == null)
            {
                return BadRequest("Invalid Event ID.");
            }

            // Map the updated properties to the existing responsible
            _mapper.Map(updatedResponsible, responsible);
            responsible.Administrative_Member_Id = administrativeMember.Administrative_Member_Id; // Set the AdministrativeMember_Id manually
            responsible.Event_Id = eventEntity.Event_Id; // Set the Event_Id manually

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
