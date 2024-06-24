using backendfepon.Data;
using backendfepon.DTOs.AccountingAccountDTOs;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.EventExpenseDTO;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingAccountController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AccountingAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountingAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountingAccountDTO>>> GetAccountingAccounts()
        {

            var accountingAccounts = await _context.AccountingAccounts
            .Include(p => p.AccountType)
           .Select(p => new AccountingAccountDTO
           {
               Account_Id= p.Account_Id,
               Account_Type_Name = p.AccountType.Account_Type_Name,
               Account_Name = p.Account_Name,
               Current_Value = p.Current_Value,
               Initial_Balance_Date = p.Initial_Balance_Date,
               Initial_Balance = p.Initial_Balance,
               Accounting_Account_Status = p.Accounting_Account_Status,
           })
           .ToListAsync();

            return Ok(accountingAccounts);
        }

        // GET: api/AccountingAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingAccount>> GetAccountingAccount(int id)
        {
            var product = await _context.AccountingAccounts.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/AccountingAccount
        [HttpPost]
        public async Task<ActionResult<AccountingAccount>> PostAccountingAccount(CreateUpdateAccountingAccountDTO accounntingAccountDTO)
        {
            var accountingAccount = new AccountingAccount
            {
              Account_Type_Id = accounntingAccountDTO.Account_Type_Id,
              Account_Name = accounntingAccountDTO.Account_Name,
              Current_Value = accounntingAccountDTO.Current_Value,
              Initial_Balance_Date = accounntingAccountDTO.Initial_Balance_Date,
              Initial_Balance = accounntingAccountDTO.Initial_Balance,
              Accounting_Account_Status = accounntingAccountDTO.Accounting_Account_Status
            };
            _context.AccountingAccounts.Add(accountingAccount);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetAccountingAccount), new { id = accountingAccount.Account_Id }, accountingAccount);
        }


        // PUT: api/AccountingAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingAccount(int id, CreateUpdateAccountingAccountDTO updatedAccountingAccount)
        {


            var accountingAccount = await _context.AccountingAccounts.FindAsync(id);

            if (accountingAccount == null)
            {
                return BadRequest();
            }

            accountingAccount.Account_Type_Id = updatedAccountingAccount.Account_Type_Id;
            accountingAccount.Account_Name = updatedAccountingAccount.Account_Name;
            accountingAccount.Current_Value = updatedAccountingAccount.Current_Value;
            accountingAccount.Initial_Balance_Date = updatedAccountingAccount.Initial_Balance_Date;
            accountingAccount.Initial_Balance = updatedAccountingAccount.Initial_Balance;
            accountingAccount.Accounting_Account_Status = updatedAccountingAccount.Accounting_Account_Status;



            _context.Entry(accountingAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingAccountExists(id))
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


        // PATCH: api/AccountingAccount/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAccountingAccountState(int id, [FromBody] string newStateId)
        {
            var accountingAccount = await _context.AccountingAccounts.FindAsync(id);

            if (accountingAccount == null)
            {
                return NotFound();
            }

            // Update the state of the product
            accountingAccount.Accounting_Account_Status = newStateId;

            _context.Entry(accountingAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountingAccountExists(id))
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
        private bool AccountingAccountExists(int id)
        {
            return _context.AccountingAccounts.Any(e => e.Account_Id == id);
        }

    }
}


