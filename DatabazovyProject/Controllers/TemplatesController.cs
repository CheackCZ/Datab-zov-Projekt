using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            //ID specification condition
            if (template.ID == 0)
                return BadRequest("You have to specify the template's ID!");

            //Checking for ID usage
            var templates = _context.Templates.ToList();
            foreach (Template temp in templates)
            {
                if (template.ID == temp.ID)
                    return BadRequest("This ID is already in use!\n -> Use differrent ID.");
            }

            var result = ValidateTemplate(template);
            if (result != null)
            {
                return result;
            }

            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return Ok($"Template ({template.ID}) added successfully:\n" + template);
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

            var result = ValidateTemplate(updatedTemplate);
            if (result != null)
            {
                return result;
            }

            dbTemplate.Author_id = updatedTemplate.Author_id;
            dbTemplate.Typ_id = updatedTemplate.Typ_id;
            dbTemplate.Name = updatedTemplate.Name;
            dbTemplate.Priced = updatedTemplate.Priced;
            dbTemplate.Price = updatedTemplate.Price;

            await _context.SaveChangesAsync();

            return Ok($"Template ({dbTemplate.ID}) added successfully:\n" + updatedTemplate);
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

            return Ok($"Template ({dbTemplate.ID}) deleted successfully:\n" + dbTemplate);
        }

        /// <summary>
        /// Validates a template entity.
        /// </summary>
        /// <param name="template">The template entity to be validated.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the validation:
        /// - <see cref="BadRequestObjectResult"/> with a message if the template name length exceeds 30 characters,
        /// - <see cref="BadRequestObjectResult"/> with a message if the template is priced but the price is null, or if the template is not priced but the price is not null,
        /// - <see cref="BadRequestObjectResult"/> with a message if the provided type ID or author ID does not exist in the database,
        /// - <see langword="null"/> if the template entity is valid.
        /// </returns>
        private ActionResult ValidateTemplate(Template template)
        {
            //Checks for the Length of Name atribute.
            if (template.Name.Length > 30)
            {
                return BadRequest("Template's name can have maximum of 30 characters!");
            }

            //Checks if the price is defined or not.
            if (((!template.Priced) && (template.Price != null)) || ((template.Priced) && (template.Price == null)))
            {
                return BadRequest("If the template is priced, then the price can not be null! \nIf the template is not priced, then the price has to be null!");
            }

            //Price Regex
            string pricePattern = @"^\d*\.?\d+$";
            if (template.Price != null)
            {
                if (!Regex.IsMatch(template.Price.ToString(), pricePattern))
                {
                    return BadRequest("Price added is in wrong format!");
                }
            }


            //Checks the FK exists
            var typeIds = _context.Types.Select(type => type.ID).ToList();
            var authorIds = _context.Authors.Select(author => author.ID).ToList();

            if (template.Typ_id != null && !typeIds.Contains(template.Typ_id))
            {
                return BadRequest("This ID doesn't match any of Types ID!");
            }
            if (template.Author_id != null && !authorIds.Contains(template.Author_id))
            {
                return BadRequest("This ID doesn't match any of Authors ID!");
            }
            return null;
        }
    }
}
