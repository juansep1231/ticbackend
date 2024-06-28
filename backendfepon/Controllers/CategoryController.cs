using backendfepon.Data;
using backendfepon.DTOs.CareerDTOs;
using backendfepon.DTOs.CategoryDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDTO
                {
                    Category_Id = c.Category_Id,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(categories);
        }
        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Categories
            .Where(p => p.Category_Id == id)
           .Select(p => new CategoryDTO
           {
               Category_Id = p.Category_Id,
               Description = p.Description

           })
           .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<Category>> PostProduct(CreateCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Description = categoryDTO.Description
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetCategory), new { id = category.Category_Id }, category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
