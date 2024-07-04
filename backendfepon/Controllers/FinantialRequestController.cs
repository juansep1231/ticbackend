using backendfepon.Data;
using backendfepon.DTOs.FinantialRequestDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinantialRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinantialRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FinantialRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinantialRequestDTO>>> GetFinantialRequests()
        {

            var finantialRequests = await _context.FinancialRequests
            .Include(p => p.AdministrativeMember)
            .Include(p => p.Financial_Request_State)
           .Select(p => new FinantialRequestDTO
           {
               Request_Id = p.Request_Id,
               AdministrativeMember_Name = p.AdministrativeMember.Student.First_Name,
               Request_Status_Name = p.Financial_Request_State.State_Description,
               Value = p.Value,
               Reason = p.Reason


           })
           .ToListAsync();

            return Ok(finantialRequests);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinantialRequestDTO>> GetFinantialRequest(int id)
        {
            var finantialRequest = await _context.FinancialRequests
            .Include(p => p.AdministrativeMember)
            .Include(p => p.Financial_Request_State)
            .Where(p => p.Request_Id == id)
           .Select(p => new FinantialRequestDTO
           {
               Request_Id = p.Request_Id,
               AdministrativeMember_Name = p.AdministrativeMember.Student.First_Name,
               Request_Status_Name = p.Financial_Request_State.State_Description,
               Value = p.Value,
               Reason = p.Reason

           })
           .FirstOrDefaultAsync();

            if (finantialRequest == null)
            {
                return NotFound();
            }

            return finantialRequest;
        }

        // POST: api/FinancialRequest
        /*
        [HttpPost]
        public async Task<ActionResult<FinantialRequestDTO>> PostFinancialRequest(CreateUpdateFinantialRequestDTO financialRequestDTO)
        {
            // Find the Administrative Member ID based on the name
            var administrativeMember = await _context.AdministrativeMembers.FirstOrDefaultAsync(am => am.Student.Email == financialRequestDTO.AdministrativeMember_Name);
            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member name.");
            }

            // Find the Request Status ID based on the name
            var requestStatus = await _context.RequestStatuses.FirstOrDefaultAsync(rs => rs.Status == financialRequestDTO.Request_Status_Name);
            if (requestStatus == null)
            {
                return BadRequest("Invalid Request Status name.");
            }

            // Map the DTO to the entity
            var financialRequest = _mapper.Map<FinancialRequest>(financialRequestDTO);
            financialRequest.Administrative_Member_Id = administrativeMember.Administrative_Member_Id;
            financialRequest.Request_Status_Id = requestStatus.Request_Status_Id;

            _context.FinancialRequests.Add(financialRequest);
            await _context.SaveChangesAsync();

            var createdFinancialRequestDTO = _mapper.Map<FinancialRequestDTO>(financialRequest);

            return CreatedAtAction(nameof(GetFinancialRequest), new { id = financialRequest.Request_Id }, createdFinancialRequestDTO);
        }*/

        // PUT: api/FinantialRequest/5
        
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutFinantialRequest(int id, CreateUpdateFinantialRequestDTO updatedFinantialRequest)
        {


            var finantialRequest = await _context.FinancialRequests.FindAsync(id);

            if (finantialRequest == null)
            {
                return BadRequest();
            }

            finantialRequest.Administrative_Member_Id = updatedFinantialRequest.AdministrativeMember_Id;
            finantialRequest.Request_Status_Id = updatedFinantialRequest.Request_Status_Id;
            finantialRequest.Value = updatedFinantialRequest.Value;
            finantialRequest.Reason = updatedFinantialRequest.Reason;


            _context.Entry(finantialRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinantialRequestExists(id))
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
        // DELETE: api/FinantialRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinantialRequest(int id)
        {
            var finantialRequest = await _context.FinancialRequests.FindAsync(id);
            if (finantialRequest == null)
            {
                return NotFound();
            }

            _context.FinancialRequests.Remove(finantialRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool FinantialRequestExists(int id)
        {
            return _context.FinancialRequests.Any(e => e.Request_Id == id);
        }
    }
}
