using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly DataContext _context;

        public TypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> GetAllTypes()
        {
            var types = await _context.Types.ToListAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DatabazovyProjekt.Type>> GetTypById(int id)
        {
            var typ = await _context.Types.FindAsync(id);
            if (typ == null)
                return NotFound("Typ Not Found!");

            return Ok(typ);
        }

        [HttpPost]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> AddTyp([FromBody] DatabazovyProjekt.Type typ)
        {
            _context.Types.Add(typ);
            await _context.SaveChangesAsync();

            return Ok(await _context.Types.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> UpdateTyp(DatabazovyProjekt.Type updatedTyp)
        {
            var dbTyp = await _context.Types.FindAsync(updatedTyp.ID);
            if (dbTyp == null)
                return NotFound("Typ Not Found!");

            dbTyp.Nazev = updatedTyp.Nazev;

            await _context.SaveChangesAsync();
            return Ok(await _context.Types.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> DeleteTyp(int id)
        {
            var dbTyp = await _context.Types.FindAsync(id);
            if (dbTyp == null)
                return NotFound("Typ Not Found!");

            _context.Types.Remove(dbTyp);
            await _context.SaveChangesAsync();

            return Ok(await _context.Types.ToListAsync());
        }

    }
}
