using AutoMapper;
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
        private readonly IMapper _mapper;

        public InventoryMovementController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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


        // POST: api/InventoryMovements
        [HttpPost]
        public async Task<ActionResult<InventoryMovementDTO>> PostInventoryMovement(CreateUpdateInventoryMovementDTO inventoryMovementDTO)
        {
            // Find the Product ID based on the name
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == inventoryMovementDTO.Product_Name);
            if (product == null)
            {
                return BadRequest("Invalid Product name.");
            }

            // Find the Inventory Movement Type ID based on the name
            var inventoryMovementType = await _context.InventoryMovementTypes.FirstOrDefaultAsync(imt => imt.Movement_Type_Name == inventoryMovementDTO.Inventory_Movement_Type_Name);
            if (inventoryMovementType == null)
            {
                return BadRequest("Invalid Inventory Movement Type name.");
            }

            // Map the DTO to the entity
            var inventoryMovement = _mapper.Map<InventoryMovement>(inventoryMovementDTO);
            inventoryMovement.Product_Id = product.Product_Id;
            inventoryMovement.Inventory_Movement_Id = inventoryMovementType.Movement_Type_Id;

            _context.InventoryMovements.Add(inventoryMovement);
            await _context.SaveChangesAsync();

            var createdInventoryMovementDTO = _mapper.Map<InventoryMovementDTO>(inventoryMovement);

            return CreatedAtAction(nameof(GetInventoryMovement), new { id = inventoryMovement.Movement_Id }, createdInventoryMovementDTO);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryMovement(int id, CreateUpdateInventoryMovementDTO updatedInventoryMovement)
        {
            var inventoryMovement = await _context.InventoryMovements.FindAsync(id);

            if (inventoryMovement == null)
            {
                return BadRequest("Invalid Inventory Movement ID.");
            }

            // Find the Product ID based on the name
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == updatedInventoryMovement.Product_Name);
            if (product == null)
            {
                return BadRequest("Invalid Product name.");
            }

            // Find the Inventory Movement Type ID based on the name
            var inventoryMovementType = await _context.InventoryMovementTypes.FirstOrDefaultAsync(imt => imt.Movement_Type_Name == updatedInventoryMovement.Inventory_Movement_Type_Name);
            if (inventoryMovementType == null)
            {
                return BadRequest("Invalid Inventory Movement Type name.");
            }

            // Map the updated properties to the existing inventory movement
            _mapper.Map(updatedInventoryMovement, inventoryMovement);
            inventoryMovement.Product_Id = product.Product_Id; // Set the Product_Id manually
            inventoryMovement.Inventory_Movement_Id = inventoryMovementType.Movement_Type_Id; // Set the Inventory_Movement_Id manually

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
