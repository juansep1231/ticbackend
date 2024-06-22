using backendfepon.Data;
using backendfepon.DTOs.ContributionPlanDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionPlanController : ControllerBase
    {

        private readonly ApplicationDbContext _context;   

        public ContributionPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ContributionPlan

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionPlanDTO>>> GetProducts()
        {
            var contributionPlans = await _context.ContributionPlans
            .Include(p => p.State)
            .Include(p => p.AcademicPeriod)
           .Select(p => new ContributionPlanDTO
           {
               Plan_Id = p.Plan_Id,
               Academic_Period = p.AcademicPeriod.Academic_Period_Name,
               State_Name = p.State.State_Name,
               Name = p.Name,
               Economic_Value = p.Economic_Value,
               Benefits = p.Benefits
               
           })
           .ToListAsync();

            return Ok(contributionPlans);
        }

        // GET: api/ContributionPlan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributionPlan>> GetContributionPlan(int id)
        {
            var contributionPlan = await _context.ContributionPlans.FindAsync(id);

            if (contributionPlan == null)
            {
                return NotFound();
            }

            return contributionPlan;
        }

        // POST: api/ContributionPlan
        [HttpPost]
        public async Task<ActionResult<ContributionPlan>> PostProduct(CreateUpdateContributionPlanDTO contributionPlanDTO)
        {
            var contributionPlan = new ContributionPlan
            {
               
                Name = contributionPlanDTO.Name,
                Benefits = contributionPlanDTO.Benefits,
                State_Id = contributionPlanDTO.State_Id,
                Economic_Value = contributionPlanDTO.Economic_Value,
                Academic_Period_Id = contributionPlanDTO.Academic_Period_Id
                

            };
            _context.ContributionPlans.Add(contributionPlan);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetContributionPlan), new { id = contributionPlan.Plan_Id }, contributionPlan);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateUpdateContributionPlanDTO updatedContributionPlan)
        {


            var contributionPlan = await _context.ContributionPlans.FindAsync(id);

            if (contributionPlan == null)
            {
                return BadRequest();
            }

            contributionPlan.State_Id = updatedContributionPlan.State_Id;
            contributionPlan.Name = updatedContributionPlan.Name;
            contributionPlan.Economic_Value = updatedContributionPlan.Economic_Value;
            contributionPlan.Academic_Period_Id = updatedContributionPlan.Academic_Period_Id;
            contributionPlan.Benefits = updatedContributionPlan.Benefits;



            _context.Entry(contributionPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributionPlanExists(id))
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

        // PATCH: api/ContributionPlan/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchContributionPlanState(int id, [FromBody] int stateId)
        {
            var contributionPlan = await _context.ContributionPlans.FindAsync(id);

            if (contributionPlan == null)
            {
                return NotFound();
            }

            // Update the state of the product
            contributionPlan.State_Id = stateId;

            _context.Entry(contributionPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContributionPlanExists(id))
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

        private bool ContributionPlanExists(int id)
        {
            return _context.ContributionPlans.Any(e => e.Plan_Id == id);
        }


    }
}
