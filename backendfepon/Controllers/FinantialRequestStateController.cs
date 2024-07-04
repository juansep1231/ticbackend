using backendfepon.Data;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.DTOs.FinantialRequestStateDTOs;
using backendfepon.DTOs.StateDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinantialRequestStateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinantialRequestStateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinantialRequestStateDTO>>> GetFinantialStates()
        {
            var states = await _context.FinancialRequestStates
                .Select(s => new FinantialRequestStateDTO
                {
                    State_Description = s.State_Description
                })
                .ToListAsync();

            return Ok(states);
        }

        // GET: api/FinantialRequestState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinantialRequestStateDTO>> GetFinantialState(int id)
        {
            var finantialRequestState = await _context.FinancialRequestStates
            .Select(s => new FinantialRequestStateDTO
            {
                State_Description = s.State_Description
            })
            .FirstOrDefaultAsync();

            if (finantialRequestState == null)
            {
                return NotFound();
            }

            return Ok(finantialRequestState);
        }
    }
}

