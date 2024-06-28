using backendfepon.Data;
using backendfepon.DTOs.InventoryMovementDTOs;
using backendfepon.DTOs.InventoryMovementTypeDTO;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryMovementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InventoryMovement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMovementDTO>>> GetInventoryMovements()
        {
            var inventoryMovements = await _context.InventoryMovements
            .Include(p => p.Transaction)
            .Include(p => p.InventoryMovementType)
            .Include(p => p.Product)
           .Select(p => new InventoryMovementDTO
           {
               Movement_Id = p.Movement_Id,
               Transaction_Id = p.Transaction_Id,
               Inventory_Movement_Name = p.InventoryMovementType.Movement_Type_Name,
               Product_Name = p.Product.Name,
               Quantity = p.Quantity,
               Date = p.Date
           })
           .ToListAsync();

            return Ok(inventoryMovements);
        }

        // GET: api/InventoryMovement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovementDTO>> GetInventoryMovement(int id)
        {
            var inventoryMovement = await _context.InventoryMovements
            .Include(p => p.Transaction)
            .Include(p => p.InventoryMovementType)
            .Include(p => p.Product)
           .Select(p => new InventoryMovementDTO
           {
               Movement_Id = p.Movement_Id,
               Transaction_Id = p.Transaction_Id,
               Inventory_Movement_Name = p.InventoryMovementType.Movement_Type_Name,
               Product_Name = p.Product.Name,
               Quantity = p.Quantity,
               Date = p.Date
           })
            .FirstOrDefaultAsync();

            if (inventoryMovement == null)
            {
                return NotFound();
            }

            return Ok(inventoryMovement);
        
        }


        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<InventoryMovement>> PostAdministrativeMember(CreateUpdateInventoryMovementDTO inventoryMovementDTO)
        {
            var inventoryMovement = new InventoryMovement
            {
                Transaction_Id = inventoryMovementDTO.Transaction_Id,
                Product_Id = inventoryMovementDTO.Product_Id,
                Inventory_Movement_Id = inventoryMovementDTO.Inventory_Movement_Id,
                Quantity = inventoryMovementDTO.Quantity,
                Date = inventoryMovementDTO.Date
            };
            _context.InventoryMovements.Add(inventoryMovement);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetInventoryMovement), new { id = inventoryMovement.Movement_Id }, inventoryMovement);
        }
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvenotryMovement(int id, CreateUpdateInventoryMovementDTO updatedInventoryMovement)
        {


            var inventoryMovement = await _context.InventoryMovements.FindAsync(id);

            if (inventoryMovement == null)
            {
                return BadRequest();
            }

            inventoryMovement.Transaction_Id = updatedInventoryMovement.Transaction_Id;
            inventoryMovement.Product_Id = updatedInventoryMovement.Product_Id;
            inventoryMovement.Quantity = updatedInventoryMovement.Quantity;
            inventoryMovement.Date = updatedInventoryMovement.Date;
            inventoryMovement.Inventory_Movement_Id = updatedInventoryMovement.Inventory_Movement_Id;


            _context.Entry(inventoryMovement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryMovementExists(id))
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
        // DELETE: api/AdministrativeMember/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryMovement(int id)
        {
            var inventoryMovement = await _context.InventoryMovements.FindAsync(id);

            if (inventoryMovement == null)
            {
                return NotFound();
            }

            _context.InventoryMovements.Remove(inventoryMovement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryMovementExists(int id)
        {
            return _context.InventoryMovements.Any(e => e.Movement_Id == id);
        }
    }
}
