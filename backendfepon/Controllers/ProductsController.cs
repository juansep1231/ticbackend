﻿using backendfepon.Data;
using backendfepon.DTOs.ProductDTOs;
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

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            //return await _context.Products.ToListAsync();
            var products = await _context.Products
            .Include(p => p.State)
           .Select(p => new ProductDTO
           {
               Product_Id = p.Product_Id,
               Name = p.Name,
               Description = p.Description,
               Economic_Value = p.Economic_Value,
               Quantity = p.Quantity,
               Label = p.Label,
               State_Name = p.State.State_Name
           })
           .ToListAsync();

            // Print the products
            PrintProducts(products);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

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
            var product = new Product
            {
                State_Id = productDTO.State_Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Economic_Value = productDTO.Economic_Value ?? 0,
                Quantity = productDTO.Quantity,
                Label = productDTO.Label
            };
             _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetProduct), new { id = product.Product_Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateUpdateProductDTO updatedProduct)
        {


            var product = await _context.Products.FindAsync(id);

            if(product == null)
            {
                return BadRequest();
            }

            product.State_Id = updatedProduct.State_Id;
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Economic_Value = updatedProduct.Economic_Value ?? 0;
            product.Quantity = updatedProduct.Quantity;
            product.Label = updatedProduct.Label;


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
        public async Task<IActionResult> PatchProductState(int id, [FromBody] int stateId)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Update the state of the product
            product.State_Id = stateId;

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


        private void PrintProducts(IEnumerable<ProductDTO> products)
        {
            foreach (var product in products)
            {
                System.Console.WriteLine($"Product ID: {product.Product_Id}");
                System.Console.WriteLine($"Name: {product.Name}");
                System.Console.WriteLine($"Description: {product.Description}");
                System.Console.WriteLine($"Economic Value: {product.Economic_Value}");
                System.Console.WriteLine($"Quantity: {product.Quantity}");
                System.Console.WriteLine($"Label: {product.Label}");
                System.Console.WriteLine($"State Name: {product.State_Name}");
                System.Console.WriteLine(new string('-', 50)); // Separator for readability
            }
        }
    }
}
