using backendfepon.Data;
using backendfepon.DTOs.AcademicPeriodDTOs;
using backendfepon.DTOs.AccountTypeDTOs;
using backendfepon.DTOs.StateDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTypeDTO>>> GetStates()
        {
            var accountTypes = await _context.AccountTypes
                .Select(s => new AccountTypeDTO
                {
                    Account_Type_Name = s.Account_Type_Name
                })
                .ToListAsync();

            return Ok(accountTypes);
        }

        // GET: api/AcademicPeriod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountTypeDTO>> GetAccountType(int id)
        {
            var accountType = await _context.AccountTypes
            .Where(p => p.Account_Type_Id == id)
           .Select(p => new AccountTypeDTO
           {
               Account_Type_Name = p.Account_Type_Name

           })
           .FirstOrDefaultAsync();

            if (accountType == null)
            {
                return NotFound();
            }

            return Ok(accountType);
        }
    }
}
