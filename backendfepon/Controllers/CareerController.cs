using backendfepon.Data;
using backendfepon.DTOs.AcademicPeriodDTOs;
using backendfepon.DTOs.CareerDTOs;
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
                    Career_Id = c.Career_Id,
                    Career_Name = c.Career_Name
                })
                .ToListAsync();

            return Ok(careers);
        }
    }
}
