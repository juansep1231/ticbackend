using backendfepon.Data;
using backendfepon.DTOs.EventExpenseDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventExpense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventExpenseDTO>>> GetEventExpenses()
        {

            var eventExpenses = await _context.EventExpenses
            .Include(p => p.Transaction)
            .Include(p => p.Providers)
            .Include(p => p.Responsible)
            .Include(p => p.Event)
           .Select(p => new EventExpenseDTO
           {
               Expense_Id = p.Expense_Id,
               Transaction_Id = p.Transaction_Id,
               Event_Name = p.Event.Title,
               Responsible_Name = p.Responsible.AdministrativeMember.Student.Last_Name,
               Provider_Names = p.Providers.Select(provider => provider.Name).ToList()
           })
           .ToListAsync();

            return Ok(eventExpenses);
        }
    }
}
