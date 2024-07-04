using backendfepon.Data;
using backendfepon.DTOs.RoleDTOs;
using backendfepon.DTOs.StateDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            var roles = await _context.Roles
                .Select(s => new RoleDTO
                {
                    Role_Name = s.Role_Name
                })
                .ToListAsync();

            return Ok(roles);
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StateDTO>> GetRole(int id)
        {
            var role = await _context.Roles
             .Where(p => p.Role_Id == id)
            .Select(p => new StateDTO
            {
                State_Name = p.Role_Name
            })
            .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
    }

}
