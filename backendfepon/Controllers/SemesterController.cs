using backendfepon.Data;
using backendfepon.DTOs.EventExpenseDTO;
using backendfepon.DTOs.SemesterDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SemesterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetSemesters()
        {
            var semesters = await _context.Semesters
                .Select(s => new SemesterDTO
                {
                    Semester_Name = s.Semester_Name
                })
                .ToListAsync();

            return Ok(semesters);
        }

        // GET: api/Semester/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SemesterDTO>> GetSemester(int id)
        {
            var semester = await _context.Semesters
             .Where(p => p.Semester_Id == id)
            .Select(p => new SemesterDTO
            {
                Semester_Name = p.Semester_Name
            })
            .FirstOrDefaultAsync();

            if (semester == null)
            {
                return NotFound();
            }

            return Ok(semester);
        }
    }
}

