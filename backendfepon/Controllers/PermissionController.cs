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
    public class PermissionController : BaseController
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
            try
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
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible obtener el permiso"));
            }
        }

        // GET: api/Permission/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionDTO>> GetPermission(int id)
        {
            try
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
                    return NotFound(GenerateErrorResponse(404, "Permiso no encontrado."));
                }

                return Ok(permission);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible obtener el permiso"));
            }
        }

        // POST: api/Permission
        [HttpPost]
        public async Task<ActionResult<PermissionDTO>> PostPermission(CreateUpdatePermissionDTO permissionDTO)
        {
            try
            {
                var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == permissionDTO.Event_Name);
                if (eventEntity == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre del evento no válido."));
                }

                var permission = _mapper.Map<Permission>(permissionDTO);
                permission.Event_Id = eventEntity.Event_Id;

                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync();

                var createdPermissionDTO = _mapper.Map<PermissionDTO>(permission);

                return CreatedAtAction(nameof(GetPermission), new { id = permission.Permission_Id }, createdPermissionDTO);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible crear el permiso"));
            }
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, CreateUpdatePermissionDTO updatedPermission)
        {
            try
            {
                var permission = await _context.Permissions.FindAsync(id);
                if (permission == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "ID del permiso no válido."));
                }

                var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Title == updatedPermission.Event_Name);
                if (eventEntity == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre del evento no válido."));
                }

                _mapper.Map(updatedPermission, permission);
                permission.Event_Id = eventEntity.Event_Id;

                _context.Entry(permission).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(id))
                    {
                        return NotFound(GenerateErrorResponse(404, "Permiso no encontrado."));
                    }
                    else
                    {
                        return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error de concurrencia."));
                    }
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible actualizar el permiso"));
            }
        }

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            try
            {
                var permission = await _context.Permissions.FindAsync(id);
                if (permission == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Permiso no encontrado."));
                }

                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible eliminar el permiso"));
            }
        }

        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(e => e.Permission_Id == id);
        }
    }
}
