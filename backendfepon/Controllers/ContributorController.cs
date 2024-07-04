using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AssociationDTOs;
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
        private readonly IMapper _mapper;
        public ContributorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
               Plan_Economic_Value = p.ContributionPlan.Economic_Value,
               Transaction_Id = p.Transaction_Id,
               Student_FullName = p.Student.First_Name + ' ' + p.Student.Last_Name,
               Student_Career = p.Student.Career.Career_Name,
               Student_Faculty = p.Student.Faculty.Faculty_Name,
               Student_Email = p.Student.Email,

           })
           .ToListAsync();


            return Ok(contributors);
        }

        // GET: api/Contributor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributorDTO>> GetContributor(int id)
        {
            var contributor = await _context.Contributors
             .Include(t => t.Transaction)
            .Include(t => t.ContributionPlan)
            .Include(t => t.Student)
             .Where(p => p.Contributor_Id == id)
            .Select(p => new ContributorDTO
            {
                Contributor_Id = p.Contributor_Id,
                Plan_Name = p.ContributionPlan.Name,
                Plan_Economic_Value = p.ContributionPlan.Economic_Value,
                Transaction_Id = p.Transaction_Id,
                Student_FullName = p.Student.First_Name + ' ' + p.Student.Last_Name,
                Student_Career = p.Student.Career.Career_Name,
                Student_Faculty = p.Student.Faculty.Faculty_Name,
                Student_Email = p.Student.Email,
            })
            .FirstOrDefaultAsync();

            if (contributor  == null)
            {
                return NotFound();
            }

            return Ok(contributor);
        }

        // POST: api/Contributor
        [HttpPost]
        public async Task<ActionResult<ContributorDTO>> PostContributor(CreateUpdateContributorDTO contributorDTO)
        {
            // Find the Plan ID based on the name
            var plan = await _context.ContributionPlans.FirstOrDefaultAsync(p => p.Name == contributorDTO.Plan_Name);
            if (plan == null)
            {
                return BadRequest("Invalid Plan name.");
            }

            // Find the Student ID based on the email
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == contributorDTO.Student_Email);
            if (student == null)
            {
                return BadRequest("Invalid Student email.");
            }

            // Map the DTO to the entity
            var contributor = _mapper.Map<Contributor>(contributorDTO);
            contributor.Plan_Id = plan.Plan_Id;
            contributor.Student_Id = student.Student_Id;

            _context.Contributors.Add(contributor);
            await _context.SaveChangesAsync();

            var createdContributorDTO = _mapper.Map<ContributorDTO>(contributor);

            return CreatedAtAction(nameof(GetContributor), new { id = contributor.Contributor_Id }, createdContributorDTO);
        }



        // PUT: api/Contributor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContributor(int id, CreateUpdateContributorDTO updatedContributor)
        {
            var contributor = await _context.Contributors.FindAsync(id);

            if (contributor == null)
            {
                return BadRequest("Invalid Contributor ID.");
            }

            // Find the Plan ID based on the name
            var plan = await _context.ContributionPlans.FirstOrDefaultAsync(p => p.Name == updatedContributor.Plan_Name);
            if (plan == null)
            {
                return BadRequest("Invalid Plan name.");
            }

            // Find the Student ID based on the email
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == updatedContributor.Student_Email);
            if (student == null)
            {
                return BadRequest("Invalid Student email.");
            }

            // Map the updated properties to the existing contributor
            _mapper.Map(updatedContributor, contributor);
            contributor.Plan_Id = plan.Plan_Id; // Set the Plan_Id manually
            contributor.Student_Id = student.Student_Id; // Set the Student_Id manually

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
