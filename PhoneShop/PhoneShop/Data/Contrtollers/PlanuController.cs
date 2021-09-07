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
    public class PlanuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlanuController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Planai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planas>>> getPlanus()
        {
            var entities = await _context.Planai.ToListAsync();

            var viewModels = _mapper.Map<IEnumerable<PlanasViewModel>>(entities);

            return Ok(viewModels);
        }

        // GET: api/Planai/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planas>> ChoosePlan(int id)
        {
            var planas = await _context.Planai.FirstOrDefaultAsync(e => e.Id == id);//.FindAsync(id);

            if (planas == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PlanasViewModel>(planas);

            return Ok(viewModel);
        }

        // PUT: api/Planai/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutPlanas(Planas planas)
        {
            ////if (id != planas.Id)
            ////{
            ////    return BadRequest();
            ////}

            var entity = await _context.Planai.FirstOrDefaultAsync(e => e.Id == planas.Id);

            if (entity is null)
                return BadRequest();

            entity.Pavadinimas = planas.Pavadinimas;
            entity.MenMokestis = planas.MenMokestis;
            entity.GaliojimoLaikas = planas.GaliojimoLaikas;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanasExists(planas.Id))
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

        // POST: api/Planai
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> addPlana(Planas planas)
        {
            _context.Planai.Add(planas);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPlanas", new { id = planas.Id }, planas);
            return Ok(planas.Id);
        }

        // DELETE: api/Planai/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Planas>> pasirinktiDelete(int id)
        {
            var planas = await _context.Planai.FindAsync(id);
            if (planas == null)
            {
                return NotFound();
            }

            _context.Planai.Remove(planas);
            await _context.SaveChangesAsync();

            return planas;
        }

        private bool PlanasExists(int id)
        {
            return _context.Planai.Any(e => e.Id == id);
        }
    }
}
