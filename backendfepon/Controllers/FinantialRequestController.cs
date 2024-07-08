using AutoMapper;
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
        private readonly IMapper _mapper;

        public FinantialRequestController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
               AdministrativeMember_Name = p.AdministrativeMember.Name,
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
               AdministrativeMember_Name = p.AdministrativeMember.Name,
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
        
        [HttpPost]
        public async Task<ActionResult<FinantialRequestDTO>> PostFinancialRequest(CreateUpdateFinantialRequestDTO financialRequestDTO)
        {
            // Find the Administrative Member ID based on the name
            var administrativeMember = await _context.AdministrativeMembers.FirstOrDefaultAsync(am => am.Email == financialRequestDTO.AdministrativeMember_Name);
            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member name.");
            }

            // Find the Request Status ID based on the name
            var requestStatus = await _context.FinancialRequestStates.FirstOrDefaultAsync(rs => rs.State_Description == financialRequestDTO.Request_Status_Name);
            if (requestStatus == null)
            {
                return BadRequest("Invalid Request Status name.");
            }

            // Map the DTO to the entity
            var financialRequest = _mapper.Map<FinancialRequest>(financialRequestDTO);
            financialRequest.Administrative_Member_Id = administrativeMember.Administrative_Member_Id;
            financialRequest.Request_Status_Id = requestStatus.Request_State_Id;

            _context.FinancialRequests.Add(financialRequest);
            await _context.SaveChangesAsync();

            var createdFinancialRequestDTO = _mapper.Map<FinantialRequestDTO>(financialRequest);

            return CreatedAtAction(nameof(GetFinantialRequest), new { id = financialRequest.Request_Id }, createdFinancialRequestDTO);
        }


        // PUT: api/FinancialRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinantialRequest(int id, CreateUpdateFinantialRequestDTO updatedFinantialRequest)
        {
            var financialRequest = await _context.FinancialRequests.FindAsync(id);

            if (financialRequest == null)
            {
                return BadRequest("Invalid Financial Request ID.");
            }

            // Find the Administrative Member ID based on the email
            var administrativeMember = await _context.AdministrativeMembers.FirstOrDefaultAsync(am => am.Email == updatedFinantialRequest.AdministrativeMember_Name);
            if (administrativeMember == null)
            {
                return BadRequest("Invalid Administrative Member email.");
            }

            // Find the Request Status ID based on the name
            var requestStatus = await _context.FinancialRequestStates.FirstOrDefaultAsync(rs => rs.State_Description == updatedFinantialRequest.Request_Status_Name);
            if (requestStatus == null)
            {
                return BadRequest("Invalid Request Status name.");
            }

            // Map the updated properties to the existing financial request
            _mapper.Map(updatedFinantialRequest, financialRequest);
            financialRequest.Administrative_Member_Id = administrativeMember.Administrative_Member_Id; // Set the Administrative_Member_Id manually
            financialRequest.Request_Status_Id = requestStatus.Request_State_Id; // Set the Request_Status_Id manually

            _context.Entry(financialRequest).State = EntityState.Modified;

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
        }

        private bool FinantialRequestExists(int id)
        {
            return _context.FinancialRequests.Any(e => e.Request_Id == id);
        }
    }
}
