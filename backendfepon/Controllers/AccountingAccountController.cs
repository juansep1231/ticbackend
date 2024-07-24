﻿using AutoMapper;
using backendfepon.Cypher;
using backendfepon.Data;
using backendfepon.DTOs.AccountingAccountDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountingAccountController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly byte[]? _key;
        private readonly byte[]? _iv;

        public AccountingAccountController(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _iv = Convert.FromBase64String(_configuration["AESConfig:IV"]);
            _key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(_configuration["AESConfig:KEY"]));
        }

        // GET: api/AccountingAccount
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<AccountingAccountDTO>>> GetAccountingAccounts()
        {
            try
            {
                CypherAES cy = new CypherAES();
                var dAccounts = new List<AccountingAccountDTO>();
                var CaccountingAccounts = await _context.CAccountinngAccounts
                    .Include(p => p.AccountType)
                    
                    .Select(p => new CAccountingAccountDTOs
                    {
                        Account_Id = p.Account_Id,
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
                        id = cAccount.Account_Id,
                        accountType = cAccount.Account_Type_Name,
                        accountName = cy.DecryptStringFromBytes_Aes(cAccount.Account_Name, _key, _iv),
                        currentValue = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Current_Value, _key, _iv)),
                        date = DateTime.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance_Date, _key, _iv)).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        initialBalance = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance, _key, _iv)),
                        //accountingAccountStatus = cy.DecryptStringFromBytes_Aes(cAccount.Accounting_Account_Status, _key, _iv),
                    };
                    dAccounts.Add(account);
                }

                return Ok(dAccounts);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor."));
            }
        }

        // GET: api/AccountingAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountingAccountDTO>> GetAccountingAccount(int id)
        {
            try
            {
                CypherAES cy = new CypherAES();
                var cAccount = await _context.CAccountinngAccounts
                    .Include(p => p.AccountType)
                    .Where(p => p.Account_Id == id)
                    .Select(p => new CAccountingAccountDTOs
                    {
                        Account_Id = p.Account_Id,
                        Account_Type_Name = p.AccountType.Account_Type_Name,
                        Account_Name = p.Account_Name,
                        Current_Value = p.Current_Value,
                        Initial_Balance_Date = p.Initial_Balance_Date,
                        Initial_Balance = p.Initial_Balance,
                        Accounting_Account_Status = p.Accounting_Account_Status,
                    })
                    .FirstOrDefaultAsync();

                if (cAccount == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Cuenta contable no encontrada."));
                }

                var dAccount = new AccountingAccountDTO
                {
                    id = cAccount.Account_Id,
                    accountType = cAccount.Account_Type_Name,
                    accountName = cy.DecryptStringFromBytes_Aes(cAccount.Account_Name, _key, _iv),
                    currentValue = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Current_Value, _key, _iv)),
                    date = DateTime.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance_Date, _key, _iv)).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    initialBalance = Decimal.Parse(cy.DecryptStringFromBytes_Aes(cAccount.Initial_Balance, _key, _iv)),
                    //accountingAccountStatus = cy.DecryptStringFromBytes_Aes(cAccount.Accounting_Account_Status, _key, _iv),
                };

                return Ok(dAccount);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor."));
            }
        }

        // POST: api/AccountingAccount
        [HttpPost]
        [Authorize(Policy = "FinancialOnly")]
        public async Task<ActionResult<AccountingAccountDTO>> PostAccountingAccount(CreateUpdateAccountingAccountDTO accounntingAccountDTO)
        {
            try
            {
                CypherAES cy = new CypherAES();
                var category = await _context.AccountTypes.FirstOrDefaultAsync(c => c.Account_Type_Name == accounntingAccountDTO.accountType);
                if (category == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre de tipo de cuenta no válido."));
                }

                var accountingA = new CAccountingAccount { 
                    Accounting_Account_Status= cy.EncryptStringToBytes_AES("ACTIVO",_key,_iv),
                    Account_Name= cy.EncryptStringToBytes_AES(accounntingAccountDTO.accountName, _key, _iv),
                    Account_Type_Id=category.Account_Type_Id,
                    Current_Value= cy.EncryptStringToBytes_AES(accounntingAccountDTO.currentValue.ToString(), _key, _iv) ,
                    Initial_Balance= cy.EncryptStringToBytes_AES(accounntingAccountDTO.currentValue.ToString(), _key, _iv) ,
                    Initial_Balance_Date= cy.EncryptStringToBytes_AES(accounntingAccountDTO.date.ToString(), _key, _iv),
                };

                /*
                  var product = _mapper.Map<AccountingAccount>(accounntingAccountDTO);

                  product.State_Id = Constants.DEFAULT_STATE;
                  product.Category_Id = category.Category_Id;
                  product.Provider_Id = provider.Provider_Id;
               */
                _context.CAccountinngAccounts.Add(accountingA);
                await _context.SaveChangesAsync();

                var createdAccountingDTO = new AccountingAccountDTO
                {
                    id= accountingA.Account_Id,
                    accountName = cy.DecryptStringFromBytes_Aes(accountingA.Account_Name, _key, _iv),
                    accountType = accountingA.AccountType.Account_Type_Name,
                    currentValue = Decimal.Parse( cy.DecryptStringFromBytes_Aes(accountingA.Current_Value, _key, _iv)),
                    date = DateTime.Parse( cy.DecryptStringFromBytes_Aes(accountingA.Initial_Balance_Date, _key, _iv)).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    initialBalance= Decimal.Parse(cy.DecryptStringFromBytes_Aes(accountingA.Initial_Balance, _key, _iv))
                };
                return CreatedAtAction(nameof(GetAccountingAccount), new { id = createdAccountingDTO.id}, createdAccountingDTO);
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible crear el producto"));
            }

        }

        // PATCH: api/AccountingAccount/5
        [HttpPatch("{id}")]
        [Authorize(Policy = "FinancialOnly")]
        public async Task<IActionResult> PatchAccountingAccountState(int id, [FromBody] string newStateId)
        {
            try
            {
                CypherAES cy = new CypherAES();
                var accountingAccount = await _context.CAccountinngAccounts.FindAsync(id);

                if (accountingAccount == null)
                {
                    return NotFound(GenerateErrorResponse(404, "Cuenta contable no encontrada."));
                }

                accountingAccount.Accounting_Account_Status = cy.EncryptStringToBytes_AES(newStateId, _key, _iv);
                _context.Entry(accountingAccount).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountingAccountExists(id))
                    {
                        return NotFound(GenerateErrorResponse(404, "Cuenta contable no encontrada."));
                    }
                    else
                    {
                        return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error de concurrencia."));
                    }
                }

                return NoContent();
            }
            catch
            {
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor."));
            }
        }

        private bool AccountingAccountExists(int id)
        {
            return _context.CAccountinngAccounts.Any(e => e.Account_Id == id);
        }
    }
}
