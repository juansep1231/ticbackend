using backendfepon.Data;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.EventExpenseDTO;
using backendfepon.DTOs.EventIncomeDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventIncomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventIncomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventIncome
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventIncomeDTO>>> GetEventIncomes()
        {

            var eventIncomes = await _context.EventIncomes
            .Include(p => p.Transaction)
            .Include(e => e.Event)
           .Select(p => new EventIncomeDTO
           {
               Income_Id = p.Income_Id,
               Transaction_Name = p.Transaction.Reason,
               Event_Name = p.Event.Title
           })
           .ToListAsync();

            return Ok(eventIncomes);
        }

        // GET: api/EventIncome/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventIncomeDTO>> GetEventIncome(int id)
        {
            var eventIncome = await _context.EventIncomes
           .Include(p => p.Transaction)
            .Include(e => e.Event)
             .Where(p => p.Income_Id == id)
            .Select(p => new EventIncomeDTO
            {
                Income_Id = p.Income_Id,
                Transaction_Name = p.Transaction.Reason,
                Event_Name = p.Event.Title
            })
            .FirstOrDefaultAsync();

            if (eventIncome == null)
            {
                return NotFound();
            }

            return Ok(eventIncome);
        }


    }
}
