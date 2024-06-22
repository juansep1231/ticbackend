using backendfepon.Data;
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
                    Semester_Id = s.Semester_Id,
                    Semester_Name = s.Semester_Name
                })
                .ToListAsync();

            return Ok(semesters);
        }
    }
}

