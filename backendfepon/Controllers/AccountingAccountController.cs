using backendfepon.Cypher;
using backendfepon.Data;
using backendfepon.DTOs.AccountingAccountDTOs;
using backendfepon.DTOs.EventDTOs;
using backendfepon.DTOs.EventExpenseDTO;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingAccountController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private readonly byte[]? _key;
        private readonly byte[]? _iv;

        public AccountingAccountController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            _iv = Convert.FromBase64String(_configuration["AESConfig:IV"]);
            _key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(_configuration["AESConfig:KEY"]));
        }

        // GET: api/AccountingAccount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountingAccountDTO>>> GetAccountingAccounts()
        {
            CypherAES cy = new CypherAES();
            var dAccounts = new List<AccountingAccountDTO>();
            var CaccountingAccounts = await _context.CAccountinngAccounts
            .Include(p => p.AccountType)
           .Select(p => new CAccountingAccountDTOs
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

            foreach (var cAccount in CaccountingAccounts)
            {
                var account = new AccountingAccountDTO
                {
                    Account_Id = cAccount.Account_Id,
                    Account_Type_Name = cAccount.Account_Type_Name,
                    Account_Name =  cy.DecryptStringFromBytes_Aes(cAccount.Account_Name, _key, _iv),
                    Current_Value = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Current_Value, _key, _iv)),
                    Initial_Balance_Date = DateTime.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance_Date, _key, _iv)),
                    Initial_Balance = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance, _key, _iv)),
                    Accounting_Account_Status = cy.DecryptStringFromBytes_Aes(cAccount.Accounting_Account_Status, _key, _iv),

                };
                dAccounts.Add(account);
            }

            return Ok(dAccounts);
        }

        
        // GET: api/AccountingAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CAccountingAccount>> GetAccountingAccount(int id)
        {
            var product = await _context.CAccountinngAccounts.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/AccountingAccount
        [HttpPost]
        public async Task<ActionResult<CAccountingAccount>> PostAccountingAccount(CreateUpdateAccountingAccountDTO accounntingAccountDTO)
        {
            CypherAES cy = new CypherAES();
            var accountingAccount = new CAccountingAccount
            {
              Account_Type_Id = accounntingAccountDTO.Account_Type_Id,
              Account_Name = cy.EncryptStringToBytes_AES(accounntingAccountDTO.Account_Name, _key, _iv) ,
              Current_Value = cy.EncryptStringToBytes_AES(accounntingAccountDTO.Current_Value.ToString(), _key, _iv),
              Initial_Balance_Date = cy.EncryptStringToBytes_AES(accounntingAccountDTO.Initial_Balance_Date.ToString(), _key, _iv),
              Initial_Balance = cy.EncryptStringToBytes_AES(accounntingAccountDTO.Initial_Balance.ToString(), _key, _iv),
              Accounting_Account_Status = cy.EncryptStringToBytes_AES(accounntingAccountDTO.Accounting_Account_Status, _key, _iv) 
            };
            _context.CAccountinngAccounts.Add(accountingAccount);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetAccountingAccount), new { id = accountingAccount.Account_Id }, accountingAccount);
        }

        
        // PUT: api/AccountingAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountingAccount(int id, CreateUpdateAccountingAccountDTO updatedAccountingAccount)
        {

            CypherAES cy = new CypherAES();
            var accountingAccount = await _context.CAccountinngAccounts.FindAsync(id);

            if (accountingAccount == null)
            {
                return BadRequest();
            }

            accountingAccount.Account_Type_Id = updatedAccountingAccount.Account_Type_Id;
            accountingAccount.Account_Name = cy.EncryptStringToBytes_AES(updatedAccountingAccount.Account_Name, _key, _iv);
            accountingAccount.Current_Value = cy.EncryptStringToBytes_AES(updatedAccountingAccount.Current_Value.ToString(), _key, _iv);
            accountingAccount.Initial_Balance_Date = cy.EncryptStringToBytes_AES(updatedAccountingAccount.Initial_Balance_Date.ToString(), _key, _iv) ;
            accountingAccount.Initial_Balance = cy.EncryptStringToBytes_AES(updatedAccountingAccount.Initial_Balance.ToString(), _key, _iv) ;
            accountingAccount.Accounting_Account_Status = cy.EncryptStringToBytes_AES(updatedAccountingAccount.Accounting_Account_Status.ToString(), _key, _iv) ;



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
            var accountingAccount = await _context.CAccountinngAccounts.FindAsync(id);

            CypherAES cy  = new CypherAES();

            if (accountingAccount == null)
            {
                return NotFound();
            }

            // Update the state of the product
            accountingAccount.Accounting_Account_Status = cy.EncryptStringToBytes_AES(newStateId,_key,_iv) ;

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
        /**/
        private bool AccountingAccountExists(int id)
        {
            return _context.CAccountinngAccounts.Any(e => e.Account_Id == id);
        }
        
    }
}


