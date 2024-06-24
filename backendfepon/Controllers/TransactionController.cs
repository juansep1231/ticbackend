using backendfepon.Data;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {

            var transactions = await _context.Transactions
            .Include(p => p.DestinationAccount)
           .Select(p => new TransactionDTO
           {
               Transaction_Id = p.Transaction_Id,
               Date = p.Date,
               Origin_Account_Name = p.OriginAccount.Account_Name,
               Destination_Account_Name = p.DestinationAccount.Account_Name,
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
            var transaction = await _context.Transactions
            .Include(p => p.DestinationAccount)
            .Where(p => p.Transaction_Id == id)
           .Select(p => new TransactionDTO
           {
               Transaction_Id = p.Transaction_Id,
               Date = p.Date,
               Origin_Account_Name = p.OriginAccount.Account_Name,
               Destination_Account_Name = p.DestinationAccount.Account_Name,
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
