using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class PirkimoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PirkimoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TelefonoModelis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoModelisViewModel>>> OpenPhonesList()
        {
            var entities = await _context.TelefonoModeliai.ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<TelefonoModelisViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/TelefonoModelis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TelefonoModelisViewModel>> OpenExactPhone(int id)
        {
            var telefonoModelis = await _context.TelefonoModeliai.FindAsync(id);

            if (telefonoModelis == null)
            {
                return NotFound();
            }

            var entity = _mapper.Map<TelefonoModelisViewModel>(telefonoModelis);

            return entity;
        }

        // PUT: api/TelefonoModelis/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> EditPreke(TelefonoModelis telefonoModelis)
        {
            




            var entity = await _context.TelefonoModeliai.FirstOrDefaultAsync(e => e.Id == telefonoModelis.Id);

            if (entity is null)
                return BadRequest();

            entity.Pavadinimas = telefonoModelis.Pavadinimas;
            entity.Gamintojas = telefonoModelis.Gamintojas;
            entity.IsleidimoData = telefonoModelis.IsleidimoData;
            entity.Kaina = telefonoModelis.Kaina;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoModelisExists(telefonoModelis.Id))
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

        // POST: api/TelefonoModelis
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TelefonoModelis>> AddPreke(TelefonoModelis telefonoModelis)
        {
            _context.TelefonoModeliai.Add(telefonoModelis);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTelefonoModelis", new { id = telefonoModelis.Id }, telefonoModelis);
            return Ok(telefonoModelis.Id);
        }

        // DELETE: api/TelefonoModelis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoModelis>> PasirinktiDelete(int id)
        {
            var telefonoModelis = await _context.TelefonoModeliai.FindAsync(id);
            if (telefonoModelis == null)
            {
                return NotFound();
            }

            _context.TelefonoModeliai.Remove(telefonoModelis);
            await _context.SaveChangesAsync();

            return telefonoModelis;
        }

        private bool TelefonoModelisExists(int id)
        {
            return _context.TelefonoModeliai.Any(e => e.Id == id);
        }
    }
}
