﻿using AutoMapper;
using backendfepon.Cypher;
using backendfepon.Data;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;


namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly byte[]? _key;
        private readonly byte[]? _iv;

        public TransactionController(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;

            _iv = Convert.FromBase64String(_configuration["AESConfig:IV"]);
            _key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(_configuration["AESConfig:KEY"]));
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            CypherAES cy =new CypherAES();
            var transactions = await _context.Transactions
            .Include(p => p.DestinationAccount)
           .Select(p => new TransactionDTO
           {
               id = p.Transaction_Id,
               transactionType= p.TransactionType.Transaction_Type_Name,
               date = p.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
               originAccount = cy.DecryptStringFromBytes_Aes(p.OriginAccount.Account_Name, _key, _iv) ,
               destinationAccount = cy.DecryptStringFromBytes_Aes(p.DestinationAccount.Account_Name, _key, _iv),
               value = p.Value,
               description = p.Reason,

           })
           .ToListAsync();

            return Ok(transactions);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(int id)
        {
            CypherAES cy = new CypherAES();
            var transaction = await _context.Transactions
            .Include(p => p.DestinationAccount)
            .Where(p => p.Transaction_Id == id)
           .Select(p => new TransactionDTO
           {
               id = p.Transaction_Id,
               transactionType = p.TransactionType.Transaction_Type_Name,
               date = p.Date.ToString ("dd/MM/yyyy", CultureInfo.InvariantCulture),
               originAccount = cy.DecryptStringFromBytes_Aes(p.OriginAccount.Account_Name, _key, _iv),
               destinationAccount = cy.DecryptStringFromBytes_Aes(p.DestinationAccount.Account_Name, _key, _iv),
               value = p.Value,
               description = p.Reason,
           })
           .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost]
        [Authorize(Policy = "FinancialOnly")]
        public async Task<ActionResult<TransactionDTO>> PostTransaction(CreateUpdateTransactionDTO transactionDTO)
        {

            CypherAES cy = new CypherAES();

            try
            {
                // Obtén todas las cuentas y desencripta en memoria
                var allAccounts = await _context.CAccountinngAccounts.ToListAsync();
                
                var originAccount = allAccounts.FirstOrDefault(a => cy.DecryptStringFromBytes_Aes(a.Account_Name, _key, _iv) == transactionDTO.originAccount);
                if (originAccount == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre de cuenta de origen no válido."));
                }
                
                var destinationAccount = allAccounts.FirstOrDefault(a => cy.DecryptStringFromBytes_Aes(a.Account_Name, _key, _iv) == transactionDTO.destinationAccount);
                if (destinationAccount == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Nombre de cuenta de destino no válido."));
                }

                var transactionType = await _context.TransactionTypes.FirstOrDefaultAsync(a => a.Transaction_Type_Name == transactionDTO.transactionType);
                if (transactionType == null)
                {
                    return BadRequest(GenerateErrorResponse(400, "Tipo de transacción no válido."));
                }


               decimal originAccountValue =Decimal.Parse(cy.DecryptStringFromBytes_Aes(originAccount.Current_Value, _key, _iv)) - transactionDTO.value;
               decimal destinationAccountValue = Decimal.Parse(cy.DecryptStringFromBytes_Aes(destinationAccount.Current_Value, _key, _iv)) + transactionDTO.value;

               originAccount.Current_Value = cy.EncryptStringToBytes_AES(originAccountValue.ToString(), _key, _iv);
               destinationAccount.Current_Value= cy.EncryptStringToBytes_AES(destinationAccountValue.ToString(), _key, _iv);


                var transaction = _mapper.Map<Transaction>(transactionDTO);
                transaction.Origin_Account = originAccount.Account_Id;
                transaction.Origin_Account = 1;
                transaction.Destination_Account = destinationAccount.Account_Id;
                transaction.Transaction_Type_Id = transactionType.Transaction_Type_Id;

                

                _context.Transactions.Add(transaction);
                _context.Entry(originAccount).State = EntityState.Modified;
                _context.Entry(destinationAccount).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var createdTransactionDTO = _mapper.Map<TransactionDTO>(transaction);
                createdTransactionDTO.originAccount = cy.DecryptStringFromBytes_Aes(originAccount.Account_Name, _key, _iv);
                createdTransactionDTO.destinationAccount = cy.DecryptStringFromBytes_Aes(destinationAccount.Account_Name, _key, _iv);
                return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Transaction_Id }, createdTransactionDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(500, GenerateErrorResponse(500, "Error al actualizar la base de datos, no es posible crear la Transaccion", ex));
            }
            catch (AutoMapperMappingException ex)
            {
                // Handle AutoMapper exceptions
                return StatusCode(500, GenerateErrorResponse(500, "Error en la configuración del mapeo,", ex));
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                return StatusCode(500, GenerateErrorResponse(500, "Ocurrió un error interno del servidor, no es posible crear la Asociación3", ex));
            }
        }
    }
}
