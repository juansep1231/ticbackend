using backendfepon.Data;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.DTOs.ProviderDTOs;
using backendfepon.DTOs.TransactionDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProviderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDTO>>> GetProviders() { 

            var providers = await _context.Providers
           .Select(p => new ProviderDTO
           {
               Provider_Id = p.Provider_Id,
               Name = p.Name,
               Phone = p.Phone,
               Email = p.Email
           })
           .ToListAsync();

            return Ok(providers);
        }


        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderDTO>> GetProvider(int id)
        {
            var provider = await _context.Providers
            .Where(p => p.Provider_Id == id)
           .Select(p => new ProviderDTO
           {
               Provider_Id = p.Provider_Id,
               Name = p.Name,
               Phone = p.Phone,
               Email = p.Email

           })
           .FirstOrDefaultAsync();

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // POST: api/Provider
        [HttpPost]
        public async Task<ActionResult<Provider>> PostTransaction(CreateUpdateProviderDTO providerDTO)
        {
            var provider = new Provider
            {
                Name = providerDTO.Name,
                Phone = providerDTO.Phone,
                Email = providerDTO.Email
            };
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            // Return the created product details
            return CreatedAtAction(nameof(GetProvider), new { id = provider.Provider_Id }, provider);
        }


        // PUT: api/Providers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(int id, CreateUpdateProviderDTO updatedProvider)
        {


            var provider = await _context.Providers.FindAsync(id);

            if (provider == null)
            {
                return BadRequest();
            }

            provider.Name = updatedProvider.Name;
            provider.Phone = updatedProvider.Phone;
            provider.Email = updatedProvider.Email;


            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
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


        // DELETE: api/Provider/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Any(e => e.Provider_Id == id);
        }
    }
}
