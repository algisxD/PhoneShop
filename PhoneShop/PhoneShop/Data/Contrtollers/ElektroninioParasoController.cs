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
    public class ElektroninioParasoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ElektroninioParasoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/EParasai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EParasas>>> GetParasai()
        {
            var entities = await _context.EParasai.ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<EParasasViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/EParasai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EParasas>> GetEParasas(int id)
        {
            var parasas = await _context.EParasai.FirstOrDefaultAsync(e => e.Id == id);//.FindAsync(id);

            if (parasas == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<EParasasViewModel>(parasas);

            return Ok(viewModel);
        }

        // PUT: api/EParasai/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutDetale(EParasas parasas)
        {
            ////if (id != detale.Id)
            ////{
            ////    return BadRequest();
            ////}

            var entity = await _context.EParasai.FirstOrDefaultAsync(e => e.Id == parasas.Id);

            if (entity is null)
                return BadRequest();

            entity.Pin = parasas.Pin;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParasasExists(parasas.Id))
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

        // POST: api/EParasai
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> pridetiParasa(EParasas parasas)
        {
            _context.EParasai.Add(parasas);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDetale", new { id = detale.Id }, detale);
            return Ok(parasas.Id);
        }

        // DELETE: api/EParasai/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EParasas>> DeleteDetale(int id)
        {
            var parasas = await _context.EParasai.FindAsync(id);
            if (parasas == null)
            {
                return NotFound();
            }

            _context.EParasai.Remove(parasas);
            await _context.SaveChangesAsync();

            return parasas;
        }

        private bool ParasasExists(int id)
        {
            return _context.EParasai.Any(e => e.Id == id);
        }
    }
}
