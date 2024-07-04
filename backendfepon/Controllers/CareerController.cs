using backendfepon.Data;
using backendfepon.DTOs.AcademicPeriodDTOs;
using backendfepon.DTOs.CareerDTOs;
using backendfepon.DTOs.CategoryDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CareerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerDTO>>> GetCareers()
        {
            var careers = await _context.Careers
                .Select(c => new CareerDTO
                {
                    Career_Name = c.Career_Name
                })
                .ToListAsync();

            return Ok(careers);
        }

        // GET: api/Career/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CareerDTO>> GetCareer(int id)
        {
            var career = await _context.Careers
            .Where(p => p.Career_Id == id)
           .Select(p => new CareerDTO
           {
               Career_Name = p.Career_Name

           })
           .FirstOrDefaultAsync();

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }
    }
}
