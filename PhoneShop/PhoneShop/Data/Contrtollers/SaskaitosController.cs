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
    public class SaskaitosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SaskaitosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Saskaitos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Saskaita>>> OpenOrderList()
        {
            var entities = await _context.Saskaitos.Include(e => e.Uzsakymas).ThenInclude(e => e.TelefonoModelis).ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<SaskaitaViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/Saskaita/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Saskaita>> OpenOrder(string id)
        {
            var saskaita = await _context.Saskaitos.Include(e => e.Uzsakymas).ThenInclude(e => e.TelefonoModelis).Where(e => e.Uzsakymas.KlientoId == id).OrderByDescending(e => e.Uzsakymas.Data).FirstOrDefaultAsync();//.FindAsync(id);
            if (saskaita == null)
            {
                return Ok(new Saskaita() { Id = -1 });
            }

            var viewModel = _mapper.Map<SaskaitaViewModel>(saskaita);
            viewModel.KlientoId = id;

            return Ok(viewModel);
        }

        // PUT: api/Saskaitos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutDetale(Saskaita saskaita)
        {
            var entity = await _context.Saskaitos.FirstOrDefaultAsync(e => e.Id == saskaita.Id);

            if (entity is null)
                return BadRequest();

            entity.Suma = saskaita.Suma;
            entity.ApmokejimoData = saskaita.ApmokejimoData;
            entity.ApmokejimoTerminas = saskaita.ApmokejimoTerminas;
            entity.Busena = saskaita.Busena;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaskaitaExists(saskaita.Id))
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

        // POST: api/Saskaitos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> PostDetale(Saskaita saskaita)
        {
            _context.Saskaitos.Add(saskaita);
            await _context.SaveChangesAsync();

            return Ok(saskaita.Id);
        }

        // DELETE: api/Saskaitos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Saskaita>> DeleteSaskaita(int id)
        {
            var saskaita = await _context.Saskaitos.FindAsync(id);
            if (saskaita == null)
            {
                return NotFound();
            }

            _context.Saskaitos.Remove(saskaita);
            await _context.SaveChangesAsync();

            return saskaita;
        }

        private bool SaskaitaExists(int id)
        {
            return _context.Saskaitos.Any(e => e.Id == id);
        }
    }
}
