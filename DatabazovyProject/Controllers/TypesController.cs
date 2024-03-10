using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Type = DatabazovyProjekt.Type;

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
            //ID specification condition
            if (typ.ID == 0)
                return BadRequest("You have to specify the type's ID!");

            //Checking for ID usage
            var types = _context.Types.ToList();
            foreach (Type type in types)
            {
                if (type.ID == typ.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateType(typ);
            if (result != null)
                return result;

            _context.Types.Add(typ);
            await _context.SaveChangesAsync();

            return Ok($"Type ({typ.ID}) added successfully:\n" + typ);
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
                return NotFound("Type Not Found!");

            var result = ValidateType(updatedTyp);
            if (result != null)
                return result;


            dbTyp.Nazev = updatedTyp.Nazev;

            await _context.SaveChangesAsync();
            return Ok($"Type ({dbTyp.ID}) updated successfully:\n" + dbTyp);
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

            return Ok($"Type ({dbTyp.ID}) deleted successfully:\n" + dbTyp);
        }

        /// <summary>
        /// Validates the provided type object to ensure it meets certain criteria.
        /// </summary>
        /// <param name="type">The type object to validate.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating success or failure of the validation.
        /// Returns <c>null</c> if the type object is valid; otherwise, returns a <see cref="BadRequestResult"/>
        /// with an appropriate error message.
        /// </returns>
        private ActionResult ValidateType(Type type)
        {
            var types = _context.Types.ToList();

            if (type.Nazev.Length > 20)
            {
                return BadRequest("Name can have maximum length of 20 characters!");
            }
            return null;
        }
    }
}
