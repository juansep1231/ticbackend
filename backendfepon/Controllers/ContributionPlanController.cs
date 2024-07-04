using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AssociationDTOs;
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
        private readonly IMapper _mapper;

        public ContributionPlanController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ContributionPlan

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContributionPlanDTO>>> GetContributionPlans()
        {
            var contributionPlans = await _context.ContributionPlans
            .Include(p => p.State)
            .Include(p => p.AcademicPeriod)
           .Select(p => new ContributionPlanDTO
           {
               Plan_Id = p.Plan_Id,
               Academic_Period_Name = p.AcademicPeriod.Academic_Period_Name,
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
        public async Task<ActionResult<ContributionPlanDTO>> GetContributionPlan(int id)
        {
            var contributionPlan = await _context.ContributionPlans
             .Include(p => p.State)
             .Include(p => p.AcademicPeriod)
             .Where(p => p.Plan_Id == id)
            .Select(p => new ContributionPlanDTO
            {
                Plan_Id = p.Plan_Id,
                Academic_Period_Name = p.AcademicPeriod.Academic_Period_Name,
                State_Name = p.State.State_Name,
                Name = p.Name,
                Economic_Value = p.Economic_Value,
                Benefits = p.Benefits
            })
            .FirstOrDefaultAsync();

            if (contributionPlan == null)
            {
                return NotFound();
            }

            return Ok(contributionPlan);
        }

        // POST: api/ContributionPlan
        [HttpPost]
        public async Task<ActionResult<ContributionPlanDTO>> PostContributionPlan(CreateUpdateContributionPlanDTO contributionPlanDTO)
        {
            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == contributionPlanDTO.State_Name);
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Find the AcademicPeriod ID based on the name
            var academicPeriod = await _context.AcademicPeriods.FirstOrDefaultAsync(a => a.Academic_Period_Name == contributionPlanDTO.Academic_Period_Name);
            if (academicPeriod == null)
            {
                return BadRequest("Invalid Academic Period name.");
            }

            // Map the DTO to the entity
            var contributionPlan = _mapper.Map<ContributionPlan>(contributionPlanDTO);
            contributionPlan.State_Id = state.State_Id;
            contributionPlan.Academic_Period_Id = academicPeriod.Academic_Period_Id;

            _context.ContributionPlans.Add(contributionPlan);
            await _context.SaveChangesAsync();

            var createdContributionPlanDTO = _mapper.Map<ContributionPlanDTO>(contributionPlan);

            return CreatedAtAction(nameof(GetContributionPlan), new { id = contributionPlan.Plan_Id }, createdContributionPlanDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContributionPlan(int id, CreateUpdateContributionPlanDTO updatedContributionPlan)
        {
            var contributionPlan = await _context.ContributionPlans.FindAsync(id);

            if (contributionPlan == null)
            {
                return BadRequest("Invalid Contribution Plan ID.");
            }

            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == updatedContributionPlan.State_Name);
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Find the Academic Period ID based on the name
            var academicPeriod = await _context.AcademicPeriods.FirstOrDefaultAsync(a => a.Academic_Period_Name == updatedContributionPlan.Academic_Period_Name);
            if (academicPeriod == null)
            {
                return BadRequest("Invalid Academic Period name.");
            }

            // Map the updated properties to the existing contribution plan
            _mapper.Map(updatedContributionPlan, contributionPlan);
            contributionPlan.State_Id = state.State_Id; // Set the State_Id manually
            contributionPlan.Academic_Period_Id = academicPeriod.Academic_Period_Id; // Set the Academic_Period_Id manually

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
