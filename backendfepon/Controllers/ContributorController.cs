using backendfepon.Data;
using backendfepon.DTOs.ContributorDTO;
using backendfepon.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ContributorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contributors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributorDTO>>> GetContributors()
        {

            var contributors = await _context.Contributors
            .Include(t => t.Transaction)
            .Include(t => t.ContributionPlan)
            .Include(t => t.Student)
           .Select(p => new ContributorDTO
           {
               Contributor_Id = p.Contributor_Id,
               Plan_Name = p.ContributionPlan.Name,
               Transaction_Id = p.Transaction_Id,
               Student_Name = p.Student.First_Name
           })
           .ToListAsync();


            return Ok(contributors);
        }

    }
}
