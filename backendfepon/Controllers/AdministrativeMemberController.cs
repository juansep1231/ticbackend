using backendfepon.Data;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdministrativeMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdministrativeMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministrativeMemberDTO>>> GetAdministrativesMembers()
        {
            var administrativeMembers = await _context.AdministrativeMembers
            .Include(p => p.User)
            .Include(e => e.Student)
           .Select(p => new AdministrativeMemberDTO
           {
               Administrative_Member_Id = p.Administrative_Member_Id,
               User_Id = p.User_Id,
               Student_Name = p.Student.First_Name,
               Photo = p.Photo,
           })
           .ToListAsync();

       
            return Ok(administrativeMembers);
        }

        // GET: api/AdministrativeMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministrativeMember>> GetAdministrativeMember(int id)
        {
            var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);

            if (administrativeMember == null)
            {
                return NotFound();
            }

            return administrativeMember;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<AdministrativeMember>> PostProduct(CreateUpdateAdministrativeMemberDTO administrativeMemberDTO)
        {
            var administrativeMember = new AdministrativeMember
            {
                User_Id = administrativeMemberDTO.User_Id,
                Student_Id = administrativeMemberDTO.Student_Id,
                Photo = administrativeMemberDTO.Photo,
            };
            _context.AdministrativeMembers.Add(administrativeMember);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetAdministrativeMember), new { id = administrativeMember.Administrative_Member_Id }, administrativeMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateUpdateAdministrativeMemberDTO updatedAdministrativeMember)
        {


            var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);

            if (administrativeMember == null)
            {
                return BadRequest();
            }

            administrativeMember.User_Id = updatedAdministrativeMember.User_Id;
            administrativeMember.Student_Id = updatedAdministrativeMember.Student_Id;
            administrativeMember.Photo = updatedAdministrativeMember.Photo;

           
            _context.Entry(administrativeMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrativeMemberExists(id))
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


        // DELETE: api/AdministrativeMember/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrativeMember(int id)
        {
            var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);
            if (administrativeMember == null)
            {
                return NotFound();
            }

            _context.AdministrativeMembers.Remove(administrativeMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

            private bool AdministrativeMemberExists(int id)
        {
            return _context.AdministrativeMembers.Any(e => e.Administrative_Member_Id == id);
        }
    }
}
