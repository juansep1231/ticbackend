using backendfepon.Data;
using backendfepon.DTOs.AcademicPeriodDTOs;
using backendfepon.DTOs.CategoryDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicPeriodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AcademicPeriodController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcademicPeriodDTO>>> GetAcademicPeriods()
        {
            var academicPeriods = await _context.AcademicPeriods
                .Select(ap => new AcademicPeriodDTO
                {
                    Academic_Period_Name = ap.Academic_Period_Name
                })
                .ToListAsync();

            return Ok(academicPeriods);
        }

        // GET: api/AcademicPeriod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcademicPeriodDTO>> GetAcademicPeriod(int id)
        {
            var academicPeriod = await _context.AcademicPeriods
            .Where(p => p.Academic_Period_Id == id)
           .Select(p => new AcademicPeriodDTO
           {
               Academic_Period_Name = p.Academic_Period_Name

           })
           .FirstOrDefaultAsync();

            if (academicPeriod == null)
            {
                return NotFound();
            }

            return Ok(academicPeriod);
        }


    }
}
