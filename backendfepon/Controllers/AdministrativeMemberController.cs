using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using backendfepon.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeMembersController : BaseController
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
            try
            {
                var administrativeMembers = await _context.AdministrativeMembers
                    .Include(e => e.Student)
                    .Where(p => p.State_Id == Constants.DEFAULT_STATE)
                    .Select(p => new AdministrativeMemberDTO
                    {
                        Administrative_Member_Id = p.Administrative_Member_Id,
                        Student_Name = p.Student.First_Name,
                        Student_LastName = p.Student.Last_Name,
                        Student_Birthday = p.Student.Birth_Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Student_Phone = p.Student.Phone,
                        Student_Faculty = p.Student.Faculty.Faculty_Name,
                        Student_Career = p.Student.Career.Career_Name,
                        Student_Semester = p.Student.Semester.Semester_Name,
                        Student_Email = p.Student.Email,
                        Member_Role = p.Role.Role_Name
                    })
                    .ToListAsync();

                return Ok(administrativeMembers);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor."));
            }
        }

        // GET: api/AdministrativeMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdministrativeMemberDTO>> GetAdministrativeMember(int id)
        {
            try
            {
                var administrativeMember = await _context.AdministrativeMembers
                    .Include(e => e.Student)
                    .Where(p => p.State_Id == Constants.DEFAULT_STATE && p.Administrative_Member_Id == id)
                    .Select(p => new AdministrativeMemberDTO
                    {
                        Administrative_Member_Id = p.Administrative_Member_Id,
                        Student_Name = p.Student.First_Name,
                        Student_LastName = p.Student.Last_Name,
                        Student_Birthday = p.Student.Birth_Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
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
                    return NotFound(GenerateErrorResponse(404, "Miembro administrativo no encontrado."));
                }

                return Ok(administrativeMember);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "\"Ocurrió un error interno del servidor, no es posible obtenet el miembro administrativo"));
            }
        }

        // POST: api/AdministrativeMembers
        [HttpPost]
        public async Task<ActionResult<AdministrativeMemberDTO>> PostAdministrativeMember(CreateUpdateAdministrativeMemberDTO administrativeMemberDTO)
        {
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == administrativeMemberDTO.Student_Email);
                if (student == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Correo electrónico del estudiante no válido."));
                }

                var role = await _context.Roles.FirstOrDefaultAsync(s => s.Role_Name == administrativeMemberDTO.Member_Role);
                if (role == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Rol no válido."));
                }

                var administrativeMember = _mapper.Map<AdministrativeMember>(administrativeMemberDTO);
                administrativeMember.Student_Id = student.Student_Id;
                administrativeMember.Role_Id = role.Role_Id;
                administrativeMember.State_Id = Constants.DEFAULT_STATE;

                _context.AdministrativeMembers.Add(administrativeMember);
                await _context.SaveChangesAsync();

                var createdAdministrativeMemberDTO = _mapper.Map<AdministrativeMemberDTO>(administrativeMember);

                return CreatedAtAction(nameof(GetAdministrativeMember), new { id = administrativeMember.Administrative_Member_Id }, createdAdministrativeMemberDTO);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "\"Ocurrió un error interno del servidor, no es posible crear el miembro administrativo"));
            }
        }

        // PUT: api/AdministrativeMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrativeMember(int id, CreateUpdateAdministrativeMemberDTO updatedAdministrativeMember)
        {
            try
            {
                var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);
                if (administrativeMember == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "ID del miembro administrativo no válido."));
                }

                var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == updatedAdministrativeMember.Student_Email);
                if (student == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Correo electrónico del estudiante no válido."));
                }

                var role = await _context.Roles.FirstOrDefaultAsync(s => s.Role_Name == updatedAdministrativeMember.Member_Role);
                if (role == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Rol no válido."));
                }

                _mapper.Map(updatedAdministrativeMember, administrativeMember);
                administrativeMember.Student_Id = student.Student_Id;
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
                        return NotFound(GenerateErrorResponse(404, "Miembro administrativo no encontrado."));
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
                return StatusCode(500, GenerateErrorResponse(500, "\"Ocurrió un error interno del servidor, no es posible actualizar el miembro administrativo"));
            }
        }

        // DELETE: api/AdministrativeMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrativeMember(int id)
        {
            try
            {
                var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);
                if (administrativeMember == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Miembro administrativo no encontrado."));
                }

                _context.AdministrativeMembers.Remove(administrativeMember);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible eliminar el miembro administrativo"));
            }
        }


        // PATCH: api/Products/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProductState(int id)
        {
            try
            {
                var administrativeMember = await _context.AdministrativeMembers.FindAsync(id);
                if (administrativeMember == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Miembro admnistrativo no encontrado."));
                }

                administrativeMember.State_Id = Constants.STATE_INACTIVE;
                _context.Entry(administrativeMember).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrativeMemberExists(id))
                    {
                        return NotFound(GenerateErrorResponse(404, "Miembro administrativo no encontrado."));
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
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible actualizar el estado"));
            }
        }

        private bool AdministrativeMemberExists(int id)
        {
            return _context.AdministrativeMembers.Any(e => e.Administrative_Member_Id == id);
        }
    }
}
