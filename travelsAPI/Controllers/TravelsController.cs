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
    public class TravelsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TravelsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Travels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travel>>> GetTravel()
        {
            return await _context.Travel.ToListAsync();
        }

        // GET: api/Travels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Travel>> GetTravel(int id)
        {
            var travel = await _context.Travel.FindAsync(id);

            if (travel == null)
            {
                return NotFound();
            }

            return travel;
        }

        // PUT: api/Travels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravel(int id, Travel travel)
        {

            travel.Id = id;

            _context.Entry(travel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelExists(id))
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

        // POST: api/Travels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travel>> PostTravel(Travel travel)
        {
            _context.Travel.Add(travel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTravel), new { id = travel.Id }, travel);
        }

        // DELETE: api/Travels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel(int id)
        {
            var travel = await _context.Travel.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }

            _context.Travel.Remove(travel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Travel>>> GetFilteredTravels([FromQuery] TravelFilterDto filters)
        {
            var query = _context.Travel
                .Include(t => t.Origin)
                .Include(t => t.Destination)
                .Include(t => t.Operator)
                .Include(t => t.Status)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filters.Name))
                query = query.Where(t => t.Name.Contains(filters.Name));

            if (filters.OriginId.HasValue && filters.OriginId > 0)
                query = query.Where(t => t.OriginId == filters.OriginId);

            if (filters.DestinationId.HasValue && filters.DestinationId > 0)
                query = query.Where(t => t.DestinationId == filters.DestinationId);

            if (filters.OperatorId.HasValue && filters.OperatorId > 0)
                query = query.Where(t => t.OperatorId == filters.OperatorId);

            if (filters.StatusId.HasValue && filters.StatusId > 0)
                query = query.Where(t => t.StatusId == filters.StatusId);

            if (filters.Date.HasValue)
                query = query.Where(t => t.StartDate.Date == filters.Date.Value.Date);

            // Ordenar según OrderBy y OrderDir
            bool asc = string.Equals(filters.OrderDir, "asc", StringComparison.OrdinalIgnoreCase);

            query = filters.OrderBy?.ToLower() switch
            {
                "name" => asc ? query.OrderBy(t => t.Name) : query.OrderByDescending(t => t.Name),
                "origin" => asc ? query.OrderBy(t => t.Origin!.Name) : query.OrderByDescending(t => t.Origin!.Name),
                "destination" => asc ? query.OrderBy(t => t.Destination!.Name) : query.OrderByDescending(t => t.Destination!.Name),
                "operator" => asc ? query.OrderBy(t => t.Operator!.Name) : query.OrderByDescending(t => t.Operator!.Name),
                "date" => asc ? query.OrderBy(t => t.StartDate) : query.OrderByDescending(t => t.StartDate),
                "status" => asc ? query.OrderBy(t => t.Status!.Name) : query.OrderByDescending(t => t.Status!.Name),
                _ => query.OrderBy(t => t.Name)
            };

            var result = await query.ToListAsync();
            return Ok(result);
        }

        private bool TravelExists(int id)
        {
            return _context.Travel.Any(e => e.Id == id);
        }
    }
}
