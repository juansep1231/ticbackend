using backendfepon.Data;
using backendfepon.DTOs.SemesterDTOs;
using backendfepon.DTOs.StateDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDTO>>> GetStates()
        {
            var states = await _context.States
                .Select(s => new StateDTO
                {
                    State_Id = s.State_Id,
                    State_Name = s.State_Name
                })
                .ToListAsync();

            return Ok(states);
        }
    }
}
