using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
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

        public PermissionController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }

        // POST: api/Permission
        [HttpPost]
        public async Task<ActionResult<Permission>> PostPermission(CreateUpdatePermissionDTO permissionDTO)
        {
            var permission = new Permission
            {
                Event_Id = permissionDTO.Event_Id,
                Request = permissionDTO.Request,
                Request_Status = permissionDTO.Request_Status

            };
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetPermission), new { id = permission.Event_Id }, permission);
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, CreateUpdatePermissionDTO updatedPermission)
        {


            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return BadRequest();
            }

            permission.Event_Id = updatedPermission.Event_Id;
            permission.Request = updatedPermission.Request;
            permission.Request_Status = updatedPermission.Request_Status;


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
