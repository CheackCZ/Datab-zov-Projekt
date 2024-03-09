using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing types.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public TypesController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all types.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> GetAllTypes()
        {
            var types = await _context.Types.ToListAsync();
            return Ok(types);
        }

        /// <summary>
        /// Retrieves a type by ID.
        /// </summary>
        /// <param name="id">The ID of the type to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<DatabazovyProjekt.Type>> GetTypById(int id)
        {
            var typ = await _context.Types.FindAsync(id);
            if (typ == null)
                return NotFound("Typ Not Found!");

            return Ok(typ);
        }

        /// <summary>
        /// Adds a new type.
        /// </summary>
        /// <param name="typ">The type to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<DatabazovyProjekt.Type>>> AddTyp([FromBody] DatabazovyProjekt.Type typ)
        {
            _context.Types.Add(typ);
            await _context.SaveChangesAsync();

            return Ok(await _context.Types.ToListAsync());
        }

        /// <summary>
        /// Updates an existing type.
        /// </summary>
        /// <param name="updatedTyp">The updated type information.</param>
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

        /// <summary>
        /// Deletes a type.
        /// </summary>
        /// <param name="id">The ID of the type to delete.</param>
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
