using AutoMapper;
using backendfepon.Data;
using backendfepon.DTOs.AssociationDTOs;
using backendfepon.DTOs.ProductDTOs;
using backendfepon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backendfepon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AssociationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Association
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssociationDTO>>> GetAssociations()
        {
            //return await _context.Products.ToListAsync();
            var associations = await _context.Associations
            .Include(p => p.State)
           .Select(p => new AssociationDTO
           {
               AssociationId = p.Association_Id,
               State_Name = p.State.State_Name,
               Name = p.Name,
               Mission = p.Mission,
               Vision = p.Vision,
               Objective = p.Objective,
               Phone = p.Phone,
               Email = p.Email,
               Address = p.Address

           })
           .ToListAsync();

            return Ok(associations);
        }

        // GET: api/Association/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssociationDTO>> GetAssociation(int id)
        {
            var association = await _context.Associations
             .Include(p => p.State)
             .Where(p => p.Association_Id == id)
            .Select(p => new AssociationDTO
            {
                AssociationId = p.Association_Id,
                State_Name = p.State.State_Name,
                Name = p.Name,
                Mission = p.Mission,
                Vision = p.Vision,
                Objective = p.Objective,
                Phone = p.Phone,
                Email = p.Email,
                Address = p.Address
            })
            .FirstOrDefaultAsync();

            if (association == null)
            {
                return NotFound();
            }

            return Ok(association);
        }

        // POST: api/Association

        [HttpPost]
        public async Task<ActionResult<Association>> PostAssociation (CreateUpdateAssociationDTO associationDTO)
        {
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == associationDTO.State_Name);

            // Check if the lookup failed
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Create the association using the ID
            var association = _mapper.Map<Association>(associationDTO);
            association.State_Id = state.State_Id;

            _context.Associations.Add(association);
            await _context.SaveChangesAsync();

            // Map the created association to AssociationDTO
            var createdAssociationDTO = _mapper.Map<AssociationDTO>(association);

            // Return the created association details
            return CreatedAtAction(nameof(GetAssociation), new { id = association.Association_Id }, createdAssociationDTO);
        }

        // PUT: api/Association/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssociation(int id, CreateUpdateAssociationDTO updatedAssociation)
        {
            var association = await _context.Associations.FindAsync(id);

            if (association == null)
            {
                return BadRequest("Invalid Association ID.");
            }

            // Find the State ID based on the name
            var state = await _context.States.FirstOrDefaultAsync(s => s.State_Name == updatedAssociation.State_Name);

            // Check if the lookup failed
            if (state == null)
            {
                return BadRequest("Invalid State name.");
            }

            // Map the updated properties to the existing association
            _mapper.Map(updatedAssociation, association);
            association.State_Id = state.State_Id; // Set the State_Id manually

            _context.Entry(association).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssociationExists(id))
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

        private bool AssociationExists(int id)
        {
            return _context.Associations.Any(e => e.Association_Id == id);
        }
    }
}
