using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSAPI.Data;
using PSAPI.Data.Entities;

namespace PSAPI.Data.Contrtollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DarbuotojoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DarbuotojoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Darbuotojas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Darbuotojas>>> GetDarbuotojai()
        {
            return await _context.Darbuotojai.ToListAsync();
        }

        // GET: api/Darbuotojas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Darbuotojas>> GetDarbuotojas(int id)
        {
            var darbuotojas = await _context.Darbuotojai.FindAsync(id);

            if (darbuotojas == null)
            {
                return NotFound();
            }

            return darbuotojas;
        }

        // PUT: api/Darbuotojas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDarbuotojas(int id, Darbuotojas darbuotojas)
        {
            if (id != darbuotojas.Id)
            {
                return BadRequest();
            }

            _context.Entry(darbuotojas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DarbuotojasExists(id))
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

        // POST: api/Darbuotojas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Darbuotojas>> PostDarbuotojas(Darbuotojas darbuotojas)
        {
            _context.Darbuotojai.Add(darbuotojas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDarbuotojas", new { id = darbuotojas.Id }, darbuotojas);
        }

        // DELETE: api/Darbuotojas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Darbuotojas>> DeleteDarbuotojas(int id)
        {
            var darbuotojas = await _context.Darbuotojai.FindAsync(id);
            if (darbuotojas == null)
            {
                return NotFound();
            }

            _context.Darbuotojai.Remove(darbuotojas);
            await _context.SaveChangesAsync();

            return darbuotojas;
        }

        private bool DarbuotojasExists(int id)
        {
            return _context.Darbuotojai.Any(e => e.Id == id);
        }
    }
}
