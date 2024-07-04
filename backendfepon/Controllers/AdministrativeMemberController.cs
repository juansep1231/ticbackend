using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.DTOs.AssociationDTOs;
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
        private readonly IMapper _mapper;

        public AdministrativeMembersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AdministrativeMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministrativeMemberDTO>>> GetAdministrativesMembers()
        {
            var administrativeMembers = await _context.AdministrativeMembers
            .Include(e => e.Student)
           .Select(p => new AdministrativeMemberDTO
           {
             Administrative_Member_Id = p.Administrative_Member_Id,
             Student_Name = p.Student.First_Name,
             Student_LastName = p.Student.Last_Name,
             Student_Birthday = p.Student.Birth_Date,
             Student_Phone = p.Student.Phone,
             Student_Faculty = p.Student.Faculty.Faculty_Name,
             Student_Career = p.Student.Career.Career_Name,
             Student_Semester = p.Student.Semester.Semester_Name,
             Student_Email = p.Student.Email,
             Member_Role = p.Role.Role_Name,

           })
           .ToListAsync();

       
            return Ok(administrativeMembers);
        }

        // GET: api/AdministrativeMember/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministrativeMemberDTO>> GetAdministrativeMember(int id)
        {
            var administrativeMember = await _context.AdministrativeMembers
            .Include(e => e.Student)
             .Where(p => p.Administrative_Member_Id == id)
            .Select(p => new AdministrativeMemberDTO
            {
                Administrative_Member_Id = p.Administrative_Member_Id,
                Student_Name = p.Student.First_Name,
                Student_LastName = p.Student.Last_Name,
                Student_Birthday = p.Student.Birth_Date,
                Student_Phone = p.Student.Phone,
                Student_Faculty = p.Student.Faculty.Faculty_Name,
                Student_Career = p.Student.Career.Career_Name,
                Student_Semester = p.Student.Semester.Semester_Name,
                Student_Email = p.Student.Email,
                Member_Role = p.Role.Role_Name,
            })
            .FirstOrDefaultAsync();

            if (administrativeMember == null)
            {
                return NotFound();
            }

            return Ok(administrativeMember);
        }

        // POST: api/AdministrativeMembers
        [HttpPost]
        public async Task<ActionResult<AdministrativeMemberDTO>> PostAdministrativeMember(CreateUpdateAdministrativeMemberDTO administrativeMemberDTO)
        {
            // Find the Student ID based on the email
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == administrativeMemberDTO.Student_Email);
            if (student == null)
            {
                return BadRequest("Invalid Student email.");
            }

            // Find the Role ID based on the email
            var role = await _context.Roles.FirstOrDefaultAsync(s => s.Role_Name == administrativeMemberDTO.Member_Role);
            if (student == null)
            {
                return BadRequest("Invalid Role.");
            }
            // Map the DTO to the entity
            var administrativeMember = _mapper.Map<AdministrativeMember>(administrativeMemberDTO);
            administrativeMember.Student_Id = student.Student_Id;
            administrativeMember.Role_Id = role.Role_Id;

            _context.AdministrativeMembers.Add(administrativeMember);
            await _context.SaveChangesAsync();

            var createdAdministrativeMemberDTO = _mapper.Map<AdministrativeMemberDTO>(administrativeMember);

            return CreatedAtAction(nameof(GetAdministrativeMember), new { id = administrativeMember.Administrative_Member_Id }, createdAdministrativeMemberDTO);
        }

        // PUT: api/AdministrativeMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrativeMember(int id, CreateUpdateAdministrativeMemberDTO updatedAdministrativeMember)
        {
            var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);

            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member ID.");
            }

            // Find the Student ID based on the email
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == updatedAdministrativeMember.Student_Email);
            if (student == null)
            {
                return BadRequest("Invalid Student email.");
            }

            // Find the Role ID based on the email
            var role = await _context.Roles.FirstOrDefaultAsync(s => s.Role_Name == updatedAdministrativeMember.Member_Role);
            if (student == null)
            {
                return BadRequest("Invalid Role.");
            }
            // Map the updated properties to the existing administrative member
            _mapper.Map(updatedAdministrativeMember, administrativeMember);
            administrativeMember.Student_Id = student.Student_Id; // Set the Student_Id manually
            administrativeMember.Role_Id = role.Role_Id;

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
