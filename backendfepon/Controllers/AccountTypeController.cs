using backendfepon.Data;
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
                    Account_Type_Id = s.Account_Type_Id,
                    Account_Id = s.Account_Id,
                    Account_Type_Name = s.Account_Type_Name
                })
                .ToListAsync();

            return Ok(accountTypes);
        }
    }
}
