using backendfepon.Cypher;
using backendfepon.Data;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private readonly byte[]? _key;
        private readonly byte[]? _iv;

        public TransactionController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

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
               Transaction_Id = p.Transaction_Id,
               Date = p.Date,
               Origin_Account_Name = cy.DecryptStringFromBytes_Aes(p.OriginAccount.Account_Name, _key, _iv) ,
               Destination_Account_Name = cy.DecryptStringFromBytes_Aes(p.DestinationAccount.Account_Name, _key, _iv),
               Value = p.Value,
               Reason = p.Reason,

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
               Transaction_Id = p.Transaction_Id,
               Date = p.Date,
               Origin_Account_Name = cy.DecryptStringFromBytes_Aes(p.OriginAccount.Account_Name, _key, _iv),
               Destination_Account_Name = cy.DecryptStringFromBytes_Aes(p.DestinationAccount.Account_Name, _key, _iv),
               Value = p.Value,
               Reason = p.Reason,

           })
           .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // POST: api/Transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(CreateUpdateTransactionDTO transactionDTO)
        {
            var transaction = new Transaction
            {
                Date = transactionDTO.Date,
                Origin_Account = transactionDTO.Origin_Account,
                Destination_Account = transactionDTO.Destination_Account,
                Value = transactionDTO.Value,
                Reason = transactionDTO.Reason
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Transaction_Id }, transaction);
        }
    }
}
