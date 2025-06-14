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
    public class OperatorsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OperatorsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Operators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operator>>> GetOperator(
            [FromQuery] bool? isActive,
            [FromQuery] int? includeId = null)
        {
            IQueryable<Operator> query = _context.Operator;

            if (isActive.HasValue)
            {
                if (includeId.HasValue)
                {
                    query = query.Where(o => o.IsActive == isActive.Value || o.Id == includeId.Value);
                }
                else
                {
                    query = query.Where(o => o.IsActive == isActive.Value);
                }
            }else if (includeId.HasValue)
            {
                query = query.Where(o => o.IsActive || o.Id == includeId.Value);
            }

            return await query.ToListAsync();
        }

        // GET: api/Operators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operator>> GetOperator(int id)
        {
            var @operator = await _context.Operator.FindAsync(id);

            if (@operator == null)
            {
                return NotFound();
            }

            return @operator;
        }

        // PUT: api/Operators/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperator(int id, Operator @operator)
        {
            if (id != @operator.Id)
            {
                return BadRequest();
            }

            _context.Entry(@operator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatorExists(id))
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

        // POST: api/Operators
        [HttpPost]
        public async Task<ActionResult<Operator>> PostOperator(Operator @operator)
        {
            _context.Operator.Add(@operator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperator), new { id = @operator.Id }, @operator);
        }

        // DELETE: api/Operators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperator(int id)
        {
            var @operator = await _context.Operator.FindAsync(id);
            if (@operator == null)
            {
                return NotFound();
            }

            _context.Operator.Remove(@operator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperatorExists(int id)
        {
            return _context.Operator.Any(e => e.Id == id);
        }
    }
}
