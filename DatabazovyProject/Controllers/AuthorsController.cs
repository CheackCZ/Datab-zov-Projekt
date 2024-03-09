using DatabazovyProject.Data;
using DatabazovyProjekt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabazovyProject.Controllers
{
    /// <summary>
    /// Controller for managing authors.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="context">The data context.</param>
        public AuthorsController(DataContext context) { _context = context; }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }

        /// <summary>
        /// Retrieves an author by ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorByID(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound("Author Not Found!");

            return Ok(author);
        }

        /// <summary>
        /// Adds a new author.
        /// </summary>
        /// <param name="author">The author to add.</param>
        [HttpPost]
        public async Task<ActionResult<List<Author>>> AddAuthor([FromBody]Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        /// <param name="updatedAuthor">The updated author information.</param>
        [HttpPut]
        public async Task<ActionResult<List<Author>>> UpdateAuthor(Author updatedAuthor)
        {
            var dbAuthor = await _context.Authors.FindAsync(updatedAuthor.ID);
            if (dbAuthor == null)
                return NotFound("Author Not Found!");

            dbAuthor.Name = updatedAuthor.Name;
            dbAuthor.LastName = updatedAuthor.LastName;
            dbAuthor.Email = updatedAuthor.Email;
            dbAuthor.Portfolio = updatedAuthor.Portfolio;

            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }

        /// <summary>
        /// Deletes an author.
        /// </summary>
        /// <param name="id">The ID of the author to delete.</param>
        [HttpDelete]
        public async Task<ActionResult<List<Author>>> DeleteAuthor(int id)
        {
            var dbAuthor = await _context.Authors.FindAsync(id);
            if (dbAuthor == null)
                return NotFound("Author Not Found!");

            _context.Authors.Remove(dbAuthor);
            await _context.SaveChangesAsync();

            return Ok(await _context.Authors.ToListAsync());
        }
    }
}
