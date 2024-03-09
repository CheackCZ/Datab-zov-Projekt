using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing templates.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this> class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public TemplatesController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all templates.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Template>>> GetAllTemplates()
        {
            var templates = await _context.Templates.ToListAsync();
            return Ok(templates);
        }

        /// <summary>
        /// Retrieves a template by ID.
        /// </summary>
        /// <param name="id">The ID of the template to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplateByID(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
                return NotFound("Template Not Found!");

            return Ok(template);
        }

        /// <summary>
        /// Adds a new template.
        /// </summary>
        /// <param name="template">The template to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Template>>> AddTemplate([FromBody] Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return Ok(await _context.Templates.ToListAsync());
        }

        /// <summary>
        /// Updates an existing template.
        /// </summary>
        /// <param name="updatedTemplate">The updated template information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Template>>> UpdateTemplate(Template updatedTemplate)
        {
            var dbTemplate = await _context.Templates.FindAsync(updatedTemplate.ID);
            if (dbTemplate == null)
                return NotFound("Template Not Found!");

            dbTemplate.Author_id = updatedTemplate.Author_id;
            dbTemplate.Type_id = updatedTemplate.Type_id;
            dbTemplate.Name = updatedTemplate.Name;
            dbTemplate.Priced = updatedTemplate.Priced;
            dbTemplate.Price = updatedTemplate.Price;

            await _context.SaveChangesAsync();

            return Ok(await _context.Templates.ToListAsync());
        }

        /// <summary>
        /// Deletes a template.
        /// </summary>
        /// <param name="id">The ID of the template to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Template>>> DeleteTemplate(int id)
        {
            var dbTemplate = await _context.Templates.FindAsync(id);
            if (dbTemplate == null)
                return NotFound("Template Not Found!");

            _context.Templates.Remove(dbTemplate);
            await _context.SaveChangesAsync();

            return Ok(await _context.Templates.ToListAsync());
        }
    }
}
