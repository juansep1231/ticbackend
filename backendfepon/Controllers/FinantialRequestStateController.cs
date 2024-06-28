using backendfepon.Data;
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
                    Request_State_Id = s.Request_State_Id,
                    State_Description = s.State_Description
                })
                .ToListAsync();

            return Ok(states);
        }
    }
}

