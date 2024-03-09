using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabazovyProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly DataContext _context;

        public TemplatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Template>>> GetAllTemplates()
        {
            var templates = await _context.Templates.ToListAsync();
            return Ok(templates);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplateByID(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
                return NotFound("Template Not Found!");

            return Ok(template);
        }

        [HttpPost]
        public async Task<ActionResult<List<Template>>> AddTemplate([FromBody] Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return Ok(await _context.Templates.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Template>>> UpdateTemplate(Template updatedTemplate)
        {
            var dbTemplate = await _context.Templates.FindAsync(updatedTemplate.ID);
            if (dbTemplate == null)
                return NotFound("Template Not Found!");

            dbTemplate.Author_id = updatedTemplate.Author_id;
            dbTemplate.Typ_id = updatedTemplate.Typ_id;
            dbTemplate.Name = updatedTemplate.Name;
            dbTemplate.Priced = updatedTemplate.Priced;
            dbTemplate.Price = updatedTemplate.Price;

            await _context.SaveChangesAsync();

            return Ok(await _context.Templates.ToListAsync());
        }

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
