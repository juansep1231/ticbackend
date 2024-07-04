using backendfepon.Data;
using backendfepon.DTOs.FacultyDTOs;
using backendfepon.DTOs.StateDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacultyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultyDTO>>> GetStates()
        {
            var faculties = await _context.Faculties
                .Select(s => new FacultyDTO
                {
                    Faculty_Name = s.Faculty_Name
                })
                .ToListAsync();

            return Ok(faculties);
        }

        // GET: api/State/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacultyDTO>> GetState(int id)
        {
            var faculty = await _context.Faculties
             .Where(p => p.Faculty_Id == id)
            .Select(p => new FacultyDTO
            {
                Faculty_Name = p.Faculty_Name
            })
            .FirstOrDefaultAsync();

            if (faculty == null)
            {
                return NotFound();
            }

            return Ok(faculty);
        }
    }
}
