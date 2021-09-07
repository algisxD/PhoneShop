using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSAPI.Data;
using PSAPI.Data.Entities;
using PSAPI.Data.ViewModels;

namespace PSAPI.Data.Contrtollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzsakymoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UzsakymoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Uzsakymai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uzsakymas>>> GetUzsakymai()
        {
            var entities = await _context.Uzsakymai.ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<UzsakymasViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/Uzsakymas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Uzsakymas>> GetUzsakymas(int id)
        {
            var uzsakymas = await _context.Uzsakymai.Include(e => e.TelefonoModelis).FirstOrDefaultAsync(e => e.Id == id);
            if (uzsakymas == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<UzsakymasViewModel>(uzsakymas);

            return Ok(viewModel);
        }

        // PUT: api/Uzsakymas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> updateUzsakymas(Uzsakymas uzsakymas)
        {
            var entity = await _context.Uzsakymai.FirstOrDefaultAsync(e => e.Id == uzsakymas.Id);

            if (entity is null)
                return BadRequest();

            entity.Id = uzsakymas.Id;
            entity.UzsakymoTipas = uzsakymas.UzsakymoTipas;
            entity.Data = uzsakymas.Data;
            entity.ApmokejimoBusena = uzsakymas.ApmokejimoBusena;
            entity.KlientoId = uzsakymas.KlientoId;
            entity.TelefonoModelisId = uzsakymas.TelefonoModelisId;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UzsakymasExists(uzsakymas.Id))
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

        // POST: api/Uzsakymas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> PostDetale(Uzsakymas uzsakymas)
        {
            _context.Uzsakymai.Add(uzsakymas);
            await _context.SaveChangesAsync();

            return Ok(uzsakymas.Id);
        }

        // DELETE: api/Uzsakymas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Uzsakymas>> DeleteUzsakymas(int id)
        {
            var uzsakymas = await _context.Uzsakymai.FindAsync(id);
            if (uzsakymas == null)
            {
                return NotFound();
            }

            _context.Uzsakymai.Remove(uzsakymas);
            await _context.SaveChangesAsync();

            return uzsakymas;
        }

        private bool UzsakymasExists(int id)
        {
            return _context.Uzsakymai.Any(e => e.Id == id);
        }
    }
}
