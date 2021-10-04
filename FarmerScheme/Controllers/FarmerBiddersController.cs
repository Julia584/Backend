using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmerScheme.Models;

namespace FarmerScheme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerBiddersController : ControllerBase
    {
        private readonly ProjectGladiatorContext _context;

        public FarmerBiddersController(ProjectGladiatorContext context)
        {
            _context = context;
        }

        // GET: api/FarmerBidders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FarmerBidder>>> GetFarmerBidders()
        {
            
            return await _context.FarmerBidders.ToListAsync();
        }
        [HttpGet("farmer/{email}")]
        public IActionResult GetFamerBidder(string email)
        {
            var fa = _context.FarmerBidders.Where(x => x.EmailId == email).FirstOrDefault();

            if (fa == null)
            {
                return NotFound();
            }
            return Ok(fa);
        }
        // GET: api/FarmerBidders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FarmerBidder>> GetFarmerBidder(int id)
        {
            var farmerBidder = await _context.FarmerBidders.FindAsync(id);

            if (farmerBidder == null)
            {
                return NotFound();
            }

            return farmerBidder;
        }

        // PUT: api/FarmerBidders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFarmerBidder(int id, FarmerBidder farmerBidder)
        {
            if (id != farmerBidder.UniqueId)
            {
                return BadRequest();
            }

            _context.Entry(farmerBidder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmerBidderExists(id))
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

        // POST: api/FarmerBidders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FarmerBidder>> PostFarmerBidder(FarmerBidder farmerBidder)
        {
            _context.FarmerBidders.Add(farmerBidder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FarmerBidderExists(farmerBidder.UniqueId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFarmerBidder", new { id = farmerBidder.UniqueId }, farmerBidder);
        }

        // DELETE: api/FarmerBidders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmerBidder(int id)
        {
            var farmerBidder = await _context.FarmerBidders.FindAsync(id);
            if (farmerBidder == null)
            {
                return NotFound();
            }

            _context.FarmerBidders.Remove(farmerBidder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FarmerBidderExists(int id)
        {
            return _context.FarmerBidders.Any(e => e.UniqueId == id);
        }
    }
}
