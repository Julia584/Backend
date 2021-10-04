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
    public class CropSoldHistoriesController : ControllerBase
    {
        private readonly ProjectGladiatorContext _context;

        public CropSoldHistoriesController(ProjectGladiatorContext context)
        {
            _context = context;
        }

        // GET: api/CropSoldHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropSoldHistory>>> GetCropSoldHistories()
        {
            /* var a = (from r in _context.InsurancePolicies
                     select new {r.UniqueId, r.InsuranceNo, r.Area, r.CropName, r.CropType, r.InsuranceCompany, r.PremiumAmount, r.SharePremium, r.SumInsured, r.ZoneType });

            return Ok(a);*/

            var x = (from r in _context.CropSoldHistories
                     select new { r.DateOfSale, r.CropId, r.CropName, r.Msp, r.Quantity, r.SoldId, r.SoldPrice, r.UniqueId });
            return Ok(x);
        }

        // GET: api/CropSoldHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropSoldHistory>> GetCropSoldHistory(int id)
        {
            var cropSoldHistory = await _context.CropSoldHistories.FindAsync(id);

            if (cropSoldHistory == null)
            {
                return NotFound();
            }

            return cropSoldHistory;
        }

        // PUT: api/CropSoldHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropSoldHistory(int id, CropSoldHistory cropSoldHistory)
        {
            if (id != cropSoldHistory.SoldId)
            {
                return BadRequest();
            }

            _context.Entry(cropSoldHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropSoldHistoryExists(id))
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

        // POST: api/CropSoldHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CropSoldHistory>> PostCropSoldHistory(CropSoldHistory cropSoldHistory)
        {
            _context.CropSoldHistories.Add(cropSoldHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCropSoldHistory", new { id = cropSoldHistory.SoldId }, cropSoldHistory);
        }

        [HttpPost("Sold/{id}")]
        public IActionResult PostStatus(int id)
        {
            //var a = _context.Bidders.GroupBy(x => x.CropId == id ).Select( a=> a.Max() ).FirstOrDefault();
            List<Bidder> lb = new List<Bidder>();
            lb = _context.Bidders.Where(x => x.CropId == id ).ToList();
            Bidder bbbb = null;
            /*int max */
            foreach (Bidder item in lb)
            {
                if (bbbb == null)
                {
                    bbbb = item;
                }
                else
                {
                    if(bbbb.BidAmount < item.BidAmount)
                    {
                        bbbb = item;
                    }
                }

            }


            bbbb.SellStatus = true;
            _context.SaveChanges();
            return Ok(_context.Bidders);
        }

        // DELETE: api/CropSoldHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCropSoldHistory(int id)
        {
            var cropSoldHistory = await _context.CropSoldHistories.FindAsync(id);
            if (cropSoldHistory == null)
            {
                return NotFound();
            }

            _context.CropSoldHistories.Remove(cropSoldHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CropSoldHistoryExists(int id)
        {
            return _context.CropSoldHistories.Any(e => e.SoldId == id);
        }
    }
}
