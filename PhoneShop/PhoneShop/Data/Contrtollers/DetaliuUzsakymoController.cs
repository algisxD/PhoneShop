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
    public class DetaliuUzsakymoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetaliuUzsakymoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Detales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detale>>> GetDetales()
        {
            var entities = await _context.Detales.Include(e => e.TelefonoModelis).ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<DetaleViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/Detales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detale>> GetDetale(int id)
        {
            var detale = await _context.Detales.Include(e => e.TelefonoModelis).FirstOrDefaultAsync(e => e.Id == id);//.FindAsync(id);

            if (detale == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<DetaleViewModel>(detale);

            return Ok(viewModel);
        }

        // PUT: api/Detales/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutDetale(Detale detale)
        {
            ////if (id != detale.Id)
            ////{
            ////    return BadRequest();
            ////}

            var entity = await _context.Detales.FirstOrDefaultAsync(e => e.Id == detale.Id);

            if (entity is null)
                return BadRequest();

            entity.KilmesSalis = detale.KilmesSalis;
            entity.PagaminimoData = detale.PagaminimoData;
            entity.Pavadinimas = detale.Pavadinimas;
            entity.Savikaina = detale.Savikaina;
            entity.SerijosNumeris = detale.SerijosNumeris;
            entity.TelefonoModelisId = detale.TelefonoModelisId;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetaleExists(detale.Id))
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

        // POST: api/Detales
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> PostDetale(Detale detale)
        {
            _context.Detales.Add(detale);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDetale", new { id = detale.Id }, detale);
            return Ok(detale.Id);
        }

        // DELETE: api/Detales/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Detale>> DeleteDetale(int id)
        {
            var detale = await _context.Detales.FindAsync(id);
            if (detale == null)
            {
                return NotFound();
            }

            _context.Detales.Remove(detale);
            await _context.SaveChangesAsync();

            return detale;
        }

        private bool DetaleExists(int id)
        {
            return _context.Detales.Any(e => e.Id == id);
        }
    }
}
