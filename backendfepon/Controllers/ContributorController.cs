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
    public class ContributorController : BaseController
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
            try
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
                        Student_FullName = p.Student.First_Name + " " + p.Student.Last_Name,
                        Student_Career = p.Student.Career.Career_Name,
                        Student_Faculty = p.Student.Faculty.Faculty_Name,
                        Student_Email = p.Student.Email,
                    })
                    .ToListAsync();

                return Ok(contributors);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible obtener los aportantes"));
            }
        }

        // GET: api/Contributor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContributorDTO>> GetContributor(int id)
        {
            try
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
                        Student_FullName = p.Student.First_Name + " " + p.Student.Last_Name,
                        Student_Career = p.Student.Career.Career_Name,
                        Student_Faculty = p.Student.Faculty.Faculty_Name,
                        Student_Email = p.Student.Email,
                    })
                    .FirstOrDefaultAsync();

                if (contributor == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Aportante no encontrado."));
                }

                return Ok(contributor);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible obtener los aportantes"));
            }
        }

        // POST: api/Contributor
        [HttpPost]
        public async Task<ActionResult<ContributorDTO>> PostContributor(CreateUpdateContributorDTO contributorDTO)
        {
            try
            {
                var plan = await _context.ContributionPlans.FirstOrDefaultAsync(p => p.Name == contributorDTO.Plan_Name);
                if (plan == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre del plan no válido."));
                }

                var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == contributorDTO.Student_Email);
                if (student == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Correo electrónico del estudiante no válido."));
                }

                var contributor = _mapper.Map<Contributor>(contributorDTO);
                contributor.Plan_Id = plan.Plan_Id;
                contributor.Student_Id = student.Student_Id;

                _context.Contributors.Add(contributor);
                await _context.SaveChangesAsync();

                var createdContributorDTO = _mapper.Map<ContributorDTO>(contributor);

                return CreatedAtAction(nameof(GetContributor), new { id = contributor.Contributor_Id }, createdContributorDTO);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible crear aportantes"));
            }
        }

        // PUT: api/Contributor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContributor(int id, CreateUpdateContributorDTO updatedContributor)
        {
            try
            {
                var contributor = await _context.Contributors.FindAsync(id);
                if (contributor == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "ID del contribuyente no válido."));
                }

                var plan = await _context.ContributionPlans.FirstOrDefaultAsync(p => p.Name == updatedContributor.Plan_Name);
                if (plan == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre del plan no válido."));
                }

                var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == updatedContributor.Student_Email);
                if (student == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Correo electrónico del estudiante no válido."));
                }

                _mapper.Map(updatedContributor, contributor);
                contributor.Plan_Id = plan.Plan_Id;
                contributor.Student_Id = student.Student_Id;

                _context.Entry(contributor).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContributorExists(id))
                    {
                        return NotFound(GenerateErrorResponse(404, "Aportante no encontrado."));
                    }
                    else
                    {
                        return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error de concurrencia."));
                    }
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible actualizar los aportantes"));
            }
        }

        // DELETE: api/Contributor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContributor(int id)
        {
            try
            {
                var contributor = await _context.Contributors.FindAsync(id);
                if (contributor == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Aportante no encontrado."));
                }

                _context.Contributors.Remove(contributor);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible eliminar el aportante"));
            }
        }

        private bool ContributorExists(int id)
        {
            return _context.Contributors.Any(e => e.Contributor_Id == id);
        }
    }
}
