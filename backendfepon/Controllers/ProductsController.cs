using AutoMapper;
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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            //return await _context.Products.ToListAsync();
            var products = await _context.Products
            .Include(p => p.State)
            .Include(p => p.Provider)
            .Include(p => p.Category)
           .Select(p => new ProductDTO
           {
               Product_Id = p.Product_Id,
               Name = p.Name,
               Description = p.Description,
               Economic_Value = p.Economic_Value,
               Quantity = p.Quantity,
               Label = p.Label,
               State_Name = p.State.State_Name,
               Category_Name = p.Category.Description,
               Provider_Name = p.Provider.Name
           })
           .ToListAsync();

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _context.Products
             .Include(p => p.State)
            .Include(p => p.Provider)
            .Include(p => p.Category)
             .Where(p => p.Product_Id == id)
            .Select(p => new ProductDTO
            {
                Product_Id = p.Product_Id,
                Name = p.Name,
                Description = p.Description,
                Economic_Value = p.Economic_Value,
                Quantity = p.Quantity,
                Label = p.Label,
                State_Name = p.State.State_Name,
                Category_Name = p.Category.Description,
                Provider_Name = p.Provider.Name

            })
            .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateUpdateProductDTO productDTO)
        {
            // Find the State, Category, and Provider IDs based on the names
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == productDTO.State_Name);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Description == productDTO.Category_Name);
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Name == productDTO.Provider_Name);

            // Check if any of the lookups failed
            if (state == null || category == null || provider == null)
            {
                return BadRequest("Invalid State, Category, or Provider name.");
            }

            // Create the product using the IDs
            var product = _mapper.Map<Product>(productDTO);
            product.State_Id = state.State_Id;
            product.Category_Id = category.Category_Id;
            product.Provider_Id = provider.Provider_Id;


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Map the created product to ProductDTO
            var createdProductDTO = _mapper.Map<ProductDTO>(product);
            // Return the created product details
            return CreatedAtAction(nameof(GetProduct), new { id = product.Product_Id }, createdProductDTO);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateUpdateProductDTO updatedProductDTO)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return BadRequest("Product not found.");
            }

            // Find the State, Category, and Provider IDs based on the names
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == updatedProductDTO.State_Name);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Description == updatedProductDTO.Category_Name);
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.Name == updatedProductDTO.Provider_Name);

            // Check if any of the lookups failed
            if (state == null || category == null || provider == null)
            {
                return BadRequest("Invalid State, Category, or Provider name.");
            }

            // Update the product using the IDs
            product.State_Id = state.State_Id;
            product.Provider_Id = provider.Provider_Id;
            product.Name = updatedProductDTO.Name;
            product.Description = updatedProductDTO.Description;
            product.Economic_Value = updatedProductDTO.Economic_Value ?? 0;
            product.Quantity = updatedProductDTO.Quantity;
            product.Label = updatedProductDTO.Label;
            product.Category_Id = category.Category_Id;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PATCH: api/Products/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProductState(int id, [FromBody] string stateName)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == stateName);

            // Check if the lookup failed
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Update the state of the product
            product.State_Id = state.State_Id;

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_Id == id);
        }


        
    }
}
