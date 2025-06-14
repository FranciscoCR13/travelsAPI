using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using travelsAPI.Context;
using travelsAPI.Models;

namespace travelsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelStatusController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TravelStatusController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/TravelStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelStatus>>> GetTravelStatus()
        {
            return await _context.TravelStatus.ToListAsync();
        }

        // GET: api/TravelStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelStatus>> GetTravelStatus(int id)
        {
            var travelStatus = await _context.TravelStatus.FindAsync(id);

            if (travelStatus == null)
            {
                return NotFound();
            }

            return travelStatus;
        }

        // PUT: api/TravelStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravelStatus(int id, TravelStatus travelStatus)
        {
            if (id != travelStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(travelStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelStatusExists(id))
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

        // POST: api/TravelStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TravelStatus>> PostTravelStatus(TravelStatus travelStatus)
        {
            _context.TravelStatus.Add(travelStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTravelStatus), new { id = travelStatus.Id }, travelStatus);
        }

        // DELETE: api/TravelStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelStatus(int id)
        {
            var travelStatus = await _context.TravelStatus.FindAsync(id);
            if (travelStatus == null)
            {
                return NotFound();
            }

            _context.TravelStatus.Remove(travelStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelStatusExists(int id)
        {
            return _context.TravelStatus.Any(e => e.Id == id);
        }
    }
}
