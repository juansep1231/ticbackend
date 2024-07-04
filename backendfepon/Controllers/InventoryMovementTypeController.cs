using backendfepon.Data;
using backendfepon.DTOs.AdmnistrativeMemberDTOs;
using backendfepon.DTOs.InventoryMovementTypeDTO;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryMovementTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InventoryMovementType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMovementTypeDTO>>> GetInventoryMovementTypes()
        {
            var inventoryMovementTypes = await _context.InventoryMovementTypes
           .Select(p => new InventoryMovementTypeDTO
           {
               Movement_Type_Name = p.Movement_Type_Name,
           })
           .ToListAsync();


            return Ok(inventoryMovementTypes);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovementTypeDTO>> GetInventoryMovementType(int id)
        {
            var inventoryMovementType = await _context.InventoryMovementTypes
            .Where(p => p.Movement_Type_Id == id)
           .Select(p => new InventoryMovementTypeDTO
           {

               Movement_Type_Name = p.Movement_Type_Name,


           })
           .FirstOrDefaultAsync();

            if (inventoryMovementType == null)
            {
                return NotFound();
            }

            return inventoryMovementType;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<InventoryMovementType>> PostAdministrativeMember(CreateUpdateInventoryMovementTypeDTO inventoryMovementTypeDTO)
        {
            var inventoryMovementType = new InventoryMovementType
            {
                Movement_Type_Name = inventoryMovementTypeDTO.Movement_Type_Name,
            };
            _context.InventoryMovementTypes.Add(inventoryMovementType);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetInventoryMovementType), new { id = inventoryMovementType.Movement_Type_Id }, inventoryMovementType);
        }

        // DELETE: api/AdministrativeMember/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryMovementType(int id)
        {
            var inventoryMovementType = await _context.InventoryMovementTypes.FindAsync(id);

            if (inventoryMovementType == null)
            {
                return NotFound();
            }

            _context.InventoryMovementTypes.Remove(inventoryMovementType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
