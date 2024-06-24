using backendfepon.Data;
using backendfepon.DTOs.ContributorDTO;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
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

        // GET: api/Contributor
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

        // GET: api/Contributor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contributor>> GetContributor(int id)
        {
            var contributor = await _context.Contributors.FindAsync(id);

            if (contributor  == null)
            {
                return NotFound();
            }

            return contributor;
        }

        // POST: api/Contributor
        [HttpPost]
        public async Task<ActionResult<Contributor>> PostContributor(CreateUpdateContributorDTO contributorDTO)
        {
            var contributor = new Contributor
            {
                Plan_Id = contributorDTO.Plan_Id,
                Transaction_Id= contributorDTO.Transaction_Id,
                Student_Id = contributorDTO.Student_Id
            };
            _context.Contributors.Add(contributor);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetContributor), new { id = contributor.Contributor_Id}, contributor);
        }


        // PUT: api/Contributor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContributor(int id, CreateUpdateContributorDTO updatedContributor)
        {


            var contributor = await _context.Contributors.FindAsync(id);

            if (contributor == null)
            {
                return BadRequest();
            }

            contributor.Plan_Id = updatedContributor.Plan_Id;
            contributor.Transaction_Id = updatedContributor.Transaction_Id;
            contributor.Student_Id = updatedContributor.Student_Id;


            _context.Entry(contributor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributorExists(id))
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

        // DELETE: api/Contributor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributor(int id)
        {
            var contributor = await _context.Contributors.FindAsync(id);
            if (contributor == null)
            {
                return NotFound();
            }

            _context.Contributors.Remove(contributor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContributorExists(int id)
        {
            return _context.Contributors.Any(e => e.Contributor_Id == id);
        }

    }
}
