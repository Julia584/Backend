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
    public class BiddersController : ControllerBase
    {
        private readonly ProjectGladiatorContext _context;

        public BiddersController(ProjectGladiatorContext context)
        {
            _context = context;
        }

        // GET: api/Bidders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bidder>>> GetBidders()
        {
         /*   
          var bid = from r in _context.CropRequests
                      join v in (
                      from b in _context.Bidders
                      group b by b.CropId into p
                      select new { CropId = p.Key, maxAmount = p.Max(a => a.BidAmount)}
                      ) on r.CropId equals v.CropId

                      select new { r.CropId, r.CropName, r.CropType, r.Quantity,r.Msp,r.Approval, v.maxAmount};
         */
            var x = _context.CropRequests.Where(x => x.Approval == true).ToList();            
            return Ok(x);
        }

        // GET: api/Bidders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bidder>> GetBidder(int id)
        {
            var bidder = await _context.Bidders.FindAsync(id);

            if (bidder == null)
            {
                return NotFound();
            }

            return bidder;
        }

        // PUT: api/Bidders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidder(int id, Bidder bidder)
        {
            if (id != bidder.BiddingId)
            {
                return BadRequest();
            }

            _context.Entry(bidder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidderExists(id))
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

        // POST: api/Bidders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bidder>> PostBidder(Bidder bidder)
        {
            _context.Bidders.Add(bidder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidder", new { id = bidder.BiddingId }, bidder);
        }

        // DELETE: api/Bidders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidder(int id)
        {
            var bidder = await _context.Bidders.FindAsync(id);
            if (bidder == null)
            {
                return NotFound();
            }

            _context.Bidders.Remove(bidder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidderExists(int id)
        {
            return _context.Bidders.Any(e => e.BiddingId == id);
        }
    }
}