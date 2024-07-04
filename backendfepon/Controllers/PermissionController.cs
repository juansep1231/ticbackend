using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.EventExpenseDTO;
using backendfepon.DTOs.PermissionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PermissionController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Permission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDTO>>> GetPermissions()
        {

            var permissions = await _context.Permissions
            .Include(p => p.Event)
           .Select(p => new PermissionDTO
           {
              Permission_Id = p.Permission_Id,
              Event_Name = p.Event.Title,
              Request = p.Request,
              Request_Status = p.Request_Status

           })
           .ToListAsync();

            return Ok(permissions);
        }

        // GET: api/Permission/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDTO>> GetPermission(int id)
        {
            var permission = await _context.Permissions
            .Include(p => p.Event)
             .Where(p => p.Permission_Id == id)
            .Select(p => new PermissionDTO
            {
                Permission_Id = p.Permission_Id,
                Event_Name = p.Event.Title,
                Request = p.Request,
                Request_Status = p.Request_Status
            })
            .FirstOrDefaultAsync();

            if (permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        // POST: api/Permission
        [HttpPost]
        public async Task<ActionResult<PermissionDTO>> PostPermission(CreateUpdatePermissionDTO permissionDTO)
        {
            // Find the Event ID based on the name
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == permissionDTO.Event_Name);
            if (eventEntity == null)
            {
                return BadRequest("Invalid Event name.");
            }

            // Map the DTO to the entity
            var permission = _mapper.Map<Permission>(permissionDTO);
            permission.Event_Id = eventEntity.Event_Id;

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            var createdPermissionDTO = _mapper.Map<PermissionDTO>(permission);

            return CreatedAtAction(nameof(GetPermission), new { id = permission.Permission_Id }, createdPermissionDTO);
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, CreateUpdatePermissionDTO updatedPermission)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return BadRequest("Invalid Permission ID.");
            }

            // Find the Event ID based on the name
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == updatedPermission.Event_Name);
            if (eventEntity == null)
            {
                return BadRequest("Invalid Event name.");
            }

            // Map the updated properties to the existing permission
            _mapper.Map(updatedPermission, permission);
            permission.Event_Id = eventEntity.Event_Id; // Set the Event_Id manually

            _context.Entry(permission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(e => e.Permission_Id == id);
        }
    }
}
