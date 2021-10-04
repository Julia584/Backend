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
    public class CropRequestsController : ControllerBase
    {
        private readonly ProjectGladiatorContext _context;

        public CropRequestsController(ProjectGladiatorContext context)
        {
            _context = context;
        }

        // GET: api/CropRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropRequest>>> GetCropRequests()
        {
            var bid = from r in _context.CropRequests
                      join v in (
                      from b in _context.Bidders
                      group b by b.CropId into p
                      select new { CropId = p.Key, maxAmount = p.Max(a => a.BidAmount) }
                      ) on r.CropId equals v.CropId
                      select new { r.CropId, r.CropName, r.CropType, r.Msp, r.Quantity, r.Approval,r.UniqueId, v.maxAmount };
            
            return Ok(bid);
        }
               
        [HttpGet("AllCropReqests")]
        public async Task<ActionResult<IEnumerable<CropRequest>>> GetCropRequests2()
        {
            var bid = from r in _context.CropRequests                      
                      select new { r.UniqueId, r.CropId, r.CropName, r.Quantity, r.CropType, r.Msp, r.Approval};
            return Ok(bid);
        }

        // GET: api/CropRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropRequest>> GetCropRequest(int id)
        {
            var cropRequest = await _context.CropRequests.FindAsync(id);

            if (cropRequest == null)
            {
                return NotFound();
            }

            return cropRequest;
        }

        // PUT: api/CropRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropRequest(int id, CropRequest cropRequest)
        {
            if (id != cropRequest.CropId)
            {
                return BadRequest();
            }

            _context.Entry(cropRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropRequestExists(id))
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

        // POST: api/CropRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CropRequest>> PostCropRequest(CropRequest cropRequest)
        {
            _context.CropRequests.Add(cropRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCropRequest", new { id = cropRequest.CropId }, cropRequest);
        }

        // DELETE: api/CropRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCropRequest(int id)
        {
            var cropRequest = await _context.CropRequests.FindAsync(id);
            if (cropRequest == null)
            {
                return NotFound();
            }

            _context.CropRequests.Remove(cropRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("Approval/{id}")]
        public IActionResult PostStatus(int id)
        {
            var a = _context.CropRequests.Where(x => x.CropId == id).FirstOrDefault();
            a.Approval = true;
            _context.SaveChanges();
            return Ok(_context.CropRequests);
        }

        private bool CropRequestExists(int id)
        {
            return _context.CropRequests.Any(e => e.CropId == id);
        }
    }
}
